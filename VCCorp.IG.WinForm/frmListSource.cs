using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VCCorp.IG.Core.BUS;
using VCCorp.IG.Core.DTO;
using CefSharp;
using CefSharp.WinForms;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using VCCorp.IG.Core.DTO.JsonToObjectIG;
using System.Threading;
using VCCorp.IG.Core.DTO.Kafka;


namespace VCCorp.IG.WinForm
{
    public partial class frmListSource : Form
    {
        #region Fiel
        ChromiumWebBrowser _browser;
        string _pathCache = @"C:\CEFSharp_Cache";
        List<SiDemandSourceDTO> _listSourceIdNull = new List<SiDemandSourceDTO>();
        List<SiCrawlDataExcelDTO> _listPostSCDE = new List<SiCrawlDataExcelDTO>();
        List<SiCrawlDataExcelDTO> _listCommentSCDE = new List<SiCrawlDataExcelDTO>();
        List<SiDemandSourceDTO> _listSource = new List<SiDemandSourceDTO>();
        List<SiDemandSourcePostDTO> _listPost = new List<SiDemandSourcePostDTO>();
        List<KafkaComment> _listComment = new List<KafkaComment>();
        SiDemandSourcePostBUS _busPost = new SiDemandSourcePostBUS();
        static int count;
        static int _countCmt = 0;
        static int _countCmtDetail = 0;
        static int _countPost = 0;
        int _flag;
        //int _flag;//đánh cờ để chạy
        #endregion

        public frmListSource()
        {
            InitializeComponent();
            if (Directory.Exists(_pathCache))
            {
                btnLoginIG.Enabled = false;
            }
            InitBrowser();

        }

        //Lấy danh sách source trong bảng si_demand_source
        private void GetListSiDemandSource()
        {
            SiDemandSourceBUS source = new SiDemandSourceBUS();
            //SiDemandSourceDTO dto = new SiDemandSourceDTO();

            int i = 1;

            try
            {
                _listSource = source?.GetList(Convert.ToInt32(txtOptions.Text));
            }
            catch
            {
                rtxtDisplayResult.Text = "Không được bỏ trống trạng thái";
                return;
            }

            rtxtDisplayResult.Clear();

            if (_listSource.Count != 0)
            {
                foreach (var item in _listSource)
                {
                    //Note:sẽ bổ sung thêm switch case các trường hợp nếu có bản ghi 
                    switch (Convert.ToInt32(txtOptions.Text))
                    {
                        case 0:
                            rtxtDisplayResult.Text = "Kết quả chờ bóc: " + _listSource.Count + "\n";
                            break;
                        case 1:
                            rtxtDisplayResult.Text = "Kết quả đang bóc: " + _listSource.Count + "\n";
                            break;
                        case -1:
                            rtxtDisplayResult.Text = "Kết quả lỗi: " + _listSource.Count + "\n";
                            break;
                        case 2:
                            rtxtDisplayResult.Text = "Kết quả bóc done: " + _listSource.Count + "\n" + "-------------------" + "\n";
                            rtxtDisplayResult.AppendText("STT: " + i + Environment.NewLine);
                            i += 1;
                            rtxtDisplayResult.AppendText("Id: " + item.Id.ToString() + Environment.NewLine);
                            rtxtDisplayResult.AppendText("Platform: " + item.Platform.ToString() + Environment.NewLine);
                            rtxtDisplayResult.AppendText("Link Profile: " + item.Link.ToString() + Environment.NewLine);
                            rtxtDisplayResult.AppendText(Environment.NewLine + "------------------------" + Environment.NewLine);
                            break;
                        default:
                            MessageBox.Show("Điền số:\n1: Đang bóc\n2: Bóc thành công\n0: Chờ bóc \n-1: Lỗi", "Thông báo");
                            break;
                    }
                    //rtxtDisplayResult.AppendText("STT: " + i + Environment.NewLine);
                    //i += 1;
                    //rtxtDisplayResult.AppendText("Id: " + item.Id.ToString() + Environment.NewLine);
                    //rtxtDisplayResult.AppendText("Platform: " + item.Platform.ToString() + Environment.NewLine);
                    //rtxtDisplayResult.AppendText("Link Profile: " + item.Link.ToString() + Environment.NewLine);
                    //rtxtDisplayResult.AppendText(Environment.NewLine + "------------------------" + Environment.NewLine);
                }
            }
            else
            {
                //a = "1";
                //if(string.Compare(txtOptions.Text,a.ToString())==0)
                //{
                //    rtxtDisplayResult.Text = "Không có kết quả bóc chờ";
                //    return;
                //}    
                switch (Convert.ToInt32(txtOptions.Text))
                {
                    case 0:
                        rtxtDisplayResult.Text = "Không có kết quả chờ bóc";
                        break;
                    case 1:
                        rtxtDisplayResult.Text = "Không có kết quả đang bóc";
                        break;
                    case -1:
                        rtxtDisplayResult.Text = "Không có kết quả lỗi";
                        break;
                    case 2:
                        rtxtDisplayResult.Text = "Không có kết quả bóc done";
                        break;
                    default:
                        MessageBox.Show("Điền số:\n1: Đang bóc\n2: Bóc thành công\n0: Chờ bóc \n-1: Lỗi", "Thông báo");
                        break;
                }

            }
        }

        private void btnCrawlerPost_Click(object sender, EventArgs e)
        {
            CrawlerSDSPost();
        }

        //Login vào Ig
        private void btnLoginIG_Click(object sender, EventArgs e)
        {
            InitBrowser();
        }

        //Khởi tạo cefsharp login lưu cache IG
        private void InitBrowser()
        {
            if (!CefSharp.Cef.IsInitialized)
            {
                if (!Directory.Exists(_pathCache))
                {
                    Directory.CreateDirectory(_pathCache);
                }

                CefSharp.WinForms.CefSettings settings = new CefSharp.WinForms.CefSettings();
                settings.CachePath = _pathCache;
                settings.LogSeverity = LogSeverity.Disable;

                CefSharp.Cef.Initialize(settings);
            }

            _browser = new ChromiumWebBrowser("https://www.instagram.com/");
            this.pnlCefsharp.Controls.Add(_browser);
        }

        //Cập nhập source id null
        private void btnGetSourceId_Click(object sender, EventArgs e)
        {
            SiDemandSourceDTO dto = new SiDemandSourceDTO();
            SiDemandSourceBUS bus = new SiDemandSourceBUS();

            GetSourceIdNull();

            string source;

            foreach (var item in _listSourceIdNull)
            {
                dto.Id = item.Id;
                _browser.Load(item.Link);
                txtResutlUrl.Text = item.Link;
                source = GetSourceFromBrowser();

                source = Regex.Replace(source, "(<html)(.*?)(pre-wrap;\">)", "", RegexOptions.IgnoreCase); // xóa cặp thẻ
                source = Regex.Replace(source, "</pre></body></html>", "", RegexOptions.IgnoreCase);
                var ObjRoot = JsonConvert.DeserializeObject<Core.DTO.JsonToObjectIG.SourceId.Root>(source);

                if (ObjRoot != null)
                {
                    dto.SourceId = ObjRoot.graphql.user.id;
                    if (string.IsNullOrEmpty(dto.SourceId))
                    {
                        //bus.Update(dto.Id.ToString(), dto.SourceId);//Cập nhập lại source id khi nó bị null trong bảng si_demand_source khi thành công
                    }
                    else
                    {
                        rtxtDisplayResult.AppendText(dto.Id.ToString());
                        //dto.SourceId = "-1";
                        //bus.Update(dto.Id.ToString(), dto.SourceId);//cập nhập lại source id null trong bảng si_demand_source khi lỗi
                    }

                }

            }
        }

        //Lấy source từ trình duyệt
        private string GetSourceFromBrowser()
        {
            var task1 = _browser.GetSourceAsync();
            task1.Wait();
            string source = task1.Result;
            return source;
        }

        //Lấy danh sách source id null trong Db si_demand_source
        private void GetSourceIdNull()
        {
            SiDemandSourceBUS source = new SiDemandSourceBUS();

            _listSourceIdNull = source.GetListNullIdSource();

            if (_listSourceIdNull.Count < 1)
            {
                btnGetSourceId.Enabled = false;
            }

        }

        private void frmListSource_Load(object sender, EventArgs e)
        {
            GetSourceIdNull();
            lblStatus.Text = "xin chao";
        }

        private void btnCrawlerComment_Click(object sender, EventArgs e)
        {
            CrawlerSDSComment();
        }
        
        private void btnFresh_Click(object sender, EventArgs e)
        {
            rtxtDisplayResult.Clear();
        }

        private void btnSCDEComment_Click(object sender, EventArgs e)
        {
            CrawlerSCDEComment();
        }

        private void btnSCDEPost_Click(object sender, EventArgs e)
        {
            CrawlerSCDEPost();
        }

        //Bóc post của bảng si_crawl_data_excel
        private void CrawlerSCDEPost()
        {
            SiCrawlDataExcelBUS bus = new SiCrawlDataExcelBUS();

            _listPostSCDE = bus.GetListPost();//Lấy danh sách status = 0 trong bảng si_crawl_data_excel

            foreach (var item in _listPostSCDE)
            {
                _browser.Load(item.LinkCrawl);
                txtResutlUrl.Text = item.LinkCrawl;
                Thread.Sleep(6000);
                txtResutlUrl.Text = item.LinkCrawl;
                //bus.Update(item.Id, "", 1);//Cập nhập trạng thái trên bảng si_crawl_data_excel đang bóc
                string source = GetSourceFromBrowser();

                source = Regex.Replace(source, "(<html)(.*?)(pre-wrap;\">)", "", RegexOptions.IgnoreCase); // xóa cặp thẻ
                source = Regex.Replace(source, "</pre></body></html>", "", RegexOptions.IgnoreCase);

                var profileRoot = JsonConvert.DeserializeObject<SourceA1.Root>(source);

                if (profileRoot != null && profileRoot.items != null)
                {
                    foreach (var data in profileRoot.items)
                    {
                        SiDemandSourcePostDTO dto = new SiDemandSourcePostDTO();

                        dto.SiDemandSourceId = item.Id;
                        dto.PostId = data.id;
                        dto.ShortCode = data.code;
                        dto.Link = "https://www.instagram.com/p/" + dto.ShortCode;
                        dto.TotalComment = data.comment_count;
                        dto.TotalLike = data.like_count;
                        dto.Content = data.caption.text;
                        dto.CreateTime = VCCorp.IG.Core.Helper.DateTimeFormatAgain.UnixTimeStampToDateTime(data.caption.created_at);
                        dto.UpdateTime = DateTime.Now;
                        dto.UserId = data.user.pk.ToString();
                        dto.NameUser = data.user.username;
                        dto.Fullname = data.user.full_name;
                        dto.ProfilePicUrl = data.user.profile_pic_url;
                        dto.ImagePost = "";

                        rtxtDisplayResult.AppendText(dto.Content + "\n");
                        //Đưa vào db si_excel_history

                        //bắn lên kafka
                        KafaPostDTO kafka = new KafaPostDTO();
                        kafka.Id = dto.PostId;
                        kafka.Message = dto.Content;
                        kafka.ShortCode = dto.ShortCode;
                        kafka.Link = dto.Link;
                        kafka.TotalComment = dto.TotalComment;
                        kafka.TotalLike = dto.TotalLike;
                        kafka.TotalShare = 0;
                        kafka.TotalReaction = 0;
                        kafka.ImagePost = dto.ImagePost;
                        kafka.Platform = dto.Platform;
                        kafka.CreateTime = dto.CreateTime;
                        kafka.UpdateTime = DateTime.Now;
                        kafka.TmpTime = data.caption.created_at;
                        kafka.UserId = dto.UserId;
                        kafka.Username = "";
                        kafka.ImageUser = "";

                    }
                    // bus.Update(item.Id, "", 3);//Cập nhập trạng thái trên bảng si_crawl_data_excel đã bóc xong chờ bóc comment
                }
                else
                {
                    //bus.Update(item.Id, "", -1);//Cập nhập trạng thái trên bảng si_crawl_data_excel lỗi
                }

            }
        }

        //Bóc comment của bảng si_crawl_data_excel
        private void CrawlerSCDEComment()
        {
            SiCrawlDataExcelBUS bus = new SiCrawlDataExcelBUS();
            _listCommentSCDE = bus.GetListComment();//Lấy danh sách

            foreach (var item in _listCommentSCDE)
            {
                string linkcrawl = "https://www.instagram.com/graphql/query/?query_hash=33ba35852cb50da46f5b5e889df7d159&variables=%7B%22shortcode%22:%22" + item.ShortCode + "%22,%22first%22:100,%22after%22:%22%22%7D";
                _browser.Load(linkcrawl);
                Thread.Sleep(6000);
                txtResutlUrl.Text = linkcrawl;

                string source = GetSourceFromBrowser();
                //bus.Update(item.Id, "", 1);//Cập nhập trạng thái trên bảng si_crawl_data_excel đang bóc
                source = Regex.Replace(source, "(<html)(.*?)(pre-wrap;\">)", " ", RegexOptions.IgnoreCase); // xóa cặp thẻ
                source = Regex.Replace(source, "</pre></body></html>", "", RegexOptions.IgnoreCase);

                var objRoot = JsonConvert.DeserializeObject<CommentOfPost.Root>(source);

                if (objRoot != null && objRoot.data != null)
                {
                    foreach (var data in objRoot.data.shortcode_media.edge_media_to_comment.edges)
                    {
                        KafkaComment cmt = new KafkaComment();

                        //kafka - lấy dữ liệu từ bảng si_demand_source_post
                        cmt.PostId = item.PostId;
                        cmt.Url = item.Link;
                        //Kafka
                        cmt.CommentId = data.node.id;
                        cmt.CommentText = data.node.text;
                        cmt.OwnerId = data.node.owner.id;
                        cmt.OwnerUser = data.node.owner.username;
                        cmt.OwnerProfilePicUrl = data.node.owner.profile_pic_url;
                        cmt.CreateTime = VCCorp.IG.Core.Helper.DateTimeFormatAgain.UnixTimeStampToDateTime(data.node.created_at);

                        //Đưa vào list và bắn lên kafka
                        _listComment.Add(cmt);

                        _countCmtDetail += 1;

                        rtxtDisplayResult.AppendText(_countCmtDetail.ToString() + "\t" + cmt.CommentText + "\n");

                    }
                    //bus.Update(item.Id, "", 2);//Cập nhập trạng thái trên bảng si_crawl_data_excel bóc thành công
                }
                else
                {
                    //bus.Update(item.Id, "", -2);//Cập nhập trạng thái trên bảng si_crawl_data_excel không bóc được comment
                }
            }
        }

        //Bóc post từ bảng si_demand_source
        private void CrawlerSDSPost()
        {
            GetListSiDemandSource();

            SiDemandSourceBUS bus = new SiDemandSourceBUS();

            SiDemandSourcePostDTO dto = new SiDemandSourcePostDTO();

            foreach (var item in _listSource)
            {
                _browser.Load(item.Link);
                Thread.Sleep(6000);
                txtResutlUrl.Text = item.Link;
                //var id = item.Id;
                //dto.SiDemandSourceId = id;
                //dto.Platform = item.Platform;

                //bus.Update(item.Id.ToString(), "1", "in process", "", "");//Cập nhập trạng thái trên bảng si_demand_source - đang bóc

                string source = GetSourceFromBrowser();

                source = Regex.Replace(source, "(<html)(.*?)(pre-wrap;\">)", "", RegexOptions.IgnoreCase); // xóa cặp thẻ
                source = Regex.Replace(source, "</pre></body></html>", "", RegexOptions.IgnoreCase);

                try
                {
                    var profileRoot = JsonConvert.DeserializeObject<Profile.Root>(source);

                    if (profileRoot != null)
                    {
                        if (profileRoot.data != null)
                        {
                            count = profileRoot.data.user.edge_owner_to_timeline_media.edges.Count;//Test count
                            lblSum.Text = count.ToString();//testcount
                            foreach (var data in profileRoot.data.user.edge_owner_to_timeline_media.edges)
                            {

                                var id = item.Id;
                                dto.SiDemandSourceId = id;
                                dto.PostId = data.node.id;
                                dto.Platform = item.Platform;

                                foreach (var dataText in data.node.edge_media_to_caption.edges)
                                {
                                    string text = Regex.Replace(dataText.node.text, @"[^\w\.@-]", " ");
                                    dto.Content = text;
                                }

                                dto.Link = "https://www.instagram.com/p/" + data.node.shortcode;
                                dto.TotalComment = data.node.edge_media_to_comment.count;
                                dto.TotalLike = data.node.edge_media_preview_like.count;
                                dto.CreateTime = VCCorp.IG.Core.Helper.DateTimeFormatAgain.UnixTimeStampToDateTime(data.node.taken_at_timestamp);
                                dto.UpdateTime = DateTime.Now;
                                dto.Status = 0;
                                dto.Title = "";
                                dto.TotalShare = 0;
                                dto.UserCrawler = "thuyetnd";
                                dto.ServerNameCrawl = "";

                                //bắn lên kafka

                                //Lưu tạm vào list và db
                                _countPost += 1;

                                rtxtDisplayResult.AppendText(_countPost.ToString() + "\t" + dto.Link + "\n");
                                //_listPost.Add(dto);
                                //_busPost.Insert(dto);//Thêm bản ghi vào bảng si_demand_source_post

                            }


                            //Tìm phân trang 
                            string userId = item.SourceId;
                            Boolean hasNextpage = profileRoot.data.user.edge_owner_to_timeline_media.page_info.has_next_page;
                            string endCursor = profileRoot.data.user.edge_owner_to_timeline_media.page_info.end_cursor;

                            if (hasNextpage = true && !string.IsNullOrEmpty(endCursor))
                            {
                                Endursor(userId, endCursor);
                            }

                            string crawlcurrentdate = item.FrequencyCrawlCurrentDate.ToString();
                            crawlcurrentdate += 1;

                            string crawlerDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                            //bus.Update(item.Id.ToString(), "2", "done", crawlcurrentdate , crawlerDate); // Cập nhập trạng thái trong bảng si_demand_source đã bóc hoàn thành
                        }
                    }
                    else
                    {
                        //bus.Update(item.Id.ToString(), "-1", "", "","");//cập nhập trạng thái là bóc lỗi trong bảng si_demand_source
                    }
                }
                catch (Exception)
                {
                    //bus.Update(item.Id.ToString(), "-1", "", "","");//cập nhập trạng thái là bóc lỗi trong bảng si_demand_source
                }


            }
        }
        
        //Bóc trang tiếp của post hiện tại bảng si_demand_)source
        private void Endursor(string UserId, string nextPage)
        {
            SiDemandSourceBUS bus = new SiDemandSourceBUS();
            SiDemandSourcePostDTO dto = new SiDemandSourcePostDTO();

            foreach (var item in _listSource)
            {
                string urlpage = "https://www.instagram.com/graphql/query/?query_hash=472f257a40c653c64c666ce877d59d2b&variables={%22id%22:%22" + UserId + "%22,%22first%22:50,%22after%22:%22" + nextPage + "%22}";
                _browser.Load(urlpage);
                txtResutlUrl.Text = urlpage;
                Thread.Sleep(6000);

                var id = item.Id;
                dto.SiDemandSourceId = id;
                dto.Platform = item.Platform;

                //bus.Update(id.ToString(), "1", "in process", "", "");//Cập nhập trạng thái trên bảng si_demand_source - đang bóc

                string source = GetSourceFromBrowser();

                source = Regex.Replace(source, "(<html)(.*?)(pre-wrap;\">)", "", RegexOptions.IgnoreCase); // xóa cặp thẻ
                source = Regex.Replace(source, "</pre></body></html>", "", RegexOptions.IgnoreCase);

                try
                {
                    var profileRoot = JsonConvert.DeserializeObject<Profile.Root>(source);


                    if (profileRoot != null)
                    {
                        if (profileRoot.data != null)
                        {
                            count += profileRoot.data.user.edge_owner_to_timeline_media.edges.Count;//Test
                            lblSum.Text = count.ToString();//testcount
                            foreach (var data in profileRoot.data.user.edge_owner_to_timeline_media.edges)
                            {
                                dto.PostId = data.node.id;
                                dto.Content = data.node.text;
                                dto.Link = "https://www.instagram.com/p/" + data.node.shortcode;
                                dto.TotalComment = data.node.edge_media_to_comment.count;
                                dto.TotalLike = data.node.edge_media_preview_like.count;
                                dto.CreateTime = VCCorp.IG.Core.Helper.DateTimeFormatAgain.UnixTimeStampToDateTime(data.node.taken_at_timestamp); ;
                                dto.UpdateTime = DateTime.Now;
                                dto.Status = 0;
                                dto.Title = "";
                                dto.TotalShare = 0;
                                dto.UserCrawler = "thuyetnd";
                                dto.ServerNameCrawl = "";

                                //bắn lên kafka

                                //Lưu tạm vào list , lưu db
                                //_listPost.Add(dto);
                                _countPost += 1;

                                rtxtDisplayResult.AppendText(_countPost.ToString() + "\t" + dto.Link + "\n");
                                //_busPost.Insert(dto);//Thêm bản ghi vào bảng si_demand_source_post

                            }
                            string userId = item.SourceId;
                            Boolean hasNextpage = profileRoot.data.user.edge_owner_to_timeline_media.page_info.has_next_page;
                            string endCursor = profileRoot.data.user.edge_owner_to_timeline_media.page_info.end_cursor;

                            if (hasNextpage = true && !string.IsNullOrEmpty(endCursor))
                            {
                                Endursor(userId, endCursor);
                            }

                        }
                    }
                    else
                    {
                        //bus.Update(id.ToString(), "-1", "", "","");//Cập nhập trạng thái là bóc lỗi trong bảng si_demand_source
                    }
                }
                catch (Exception)
                {
                    //bus.Update(id.ToString(), "-1", "", "","");//Cập nhập trạng thái là bóc lỗi trong bảng si_demand_source
                }
                return;
            }
        }

        //Bóc comment từ bảng si_demand_source_post
        private void CrawlerSDSComment()
        {
            SiDemandSourcePostBUS bus = new SiDemandSourcePostBUS();
            //SiDemandSourcePostDTO dto = new SiDemandSourcePostDTO();
            KafkaComment cmt = new KafkaComment();
            int countCmt = 0;

            _listPost = bus.GetListSourcePost();

            foreach (var item in _listPost)
            {
                _countCmt += 1;
                rtxtDisplayResult.AppendText("Link: " + _countCmt.ToString() + "-----------------\n\n");
                //bus.Update(item.Id.ToString(), "1", "");//Cập nhập trạng thái trong bảng si_demand_source_post là đang bóc
                _browser.Load(item.LinkCrawler);
                txtResutlUrl.Text = item.LinkCrawler;
                Thread.Sleep(6000);

                string source = GetSourceFromBrowser();

                source = Regex.Replace(source, "(<html)(.*?)(pre-wrap;\">)", " ", RegexOptions.IgnoreCase); // xóa cặp thẻ
                source = Regex.Replace(source, "</pre></body></html>", "", RegexOptions.IgnoreCase);

                var objRoot = JsonConvert.DeserializeObject<CommentOfPost.Root>(source);

                if (objRoot != null && objRoot.data != null)
                {
                    foreach (var data in objRoot.data.shortcode_media.edge_media_to_comment.edges)
                    {

                        //kafka - lấy dữ liệu từ bảng si_demand_source_post
                        cmt.PostId = item.PostId;
                        cmt.Url = item.Link;
                        //Kafka
                        cmt.CommentId = data.node.id;
                        cmt.CommentText = data.node.text;
                        cmt.OwnerId = data.node.owner.id;
                        cmt.OwnerUser = data.node.owner.username;
                        cmt.OwnerProfilePicUrl = data.node.owner.profile_pic_url;
                        cmt.CreateTime = VCCorp.IG.Core.Helper.DateTimeFormatAgain.UnixTimeStampToDateTime(data.node.created_at);

                        //Đưa vào list và bắn lên kafka
                        _listComment.Add(cmt);

                        _countCmtDetail += 1;

                        rtxtDisplayResult.AppendText(_countCmtDetail.ToString() + "\t" + cmt.CommentText + "\n");

                    }
                    //Tìm phân trang nếu có
                    string userId = item.ShortCode;
                    Boolean has_next_page = objRoot.data.shortcode_media.edge_media_to_comment.page_info.has_next_page;
                    string nextPage = objRoot.data.shortcode_media.edge_media_to_comment.page_info.end_cursor;
                    if (has_next_page = true && !string.IsNullOrEmpty(nextPage))
                    {
                        EndursorComment(userId, nextPage);
                    }
                    else
                    {

                    }
                    //bus.Update(item.Id.ToString(), "2", "");//Cập nhập trạng thái trong bảng si_demand_source_post hoàn thành
                }
                else
                {
                    //bus.Update(item.Id.ToString(), "-1", "");//Cập nhập trạng thái trong bảng si_demand_source_post lỗi
                }
            }
            lblSum.Text = countCmt.ToString();
        }

        //Bóc comment của bảng si_demand_source_post (phân trang)
        private void EndursorComment(string shortCode, string nextPage)
        {
            SiDemandSourcePostBUS bus = new SiDemandSourcePostBUS();
            //SiDemandSourcePostDTO dto = new SiDemandSourcePostDTO();



            _listPost = bus.GetListSourcePost();

            foreach (var item in _listPost)
            {
                string urlpage = "https://www.instagram.com/graphql/query/?query_hash=33ba35852cb50da46f5b5e889df7d159&variables=%7B%22shortcode%22:%22" + shortCode + "%22,%22first%22:100,%22after%22:%22" + nextPage + "%22%7D";
                _browser.Load(urlpage);
                txtResutlUrl.Text = urlpage;
                Thread.Sleep(6000);

                string source = GetSourceFromBrowser();

                source = Regex.Replace(source, "(<html)(.*?)(pre-wrap;\">)", " ", RegexOptions.IgnoreCase); // xóa cặp thẻ
                source = Regex.Replace(source, "</pre></body></html>", "", RegexOptions.IgnoreCase);


                var objRoot = JsonConvert.DeserializeObject<CommentOfPost.Root>(source);

                if (objRoot != null && objRoot.data != null)
                {
                    foreach (var data in objRoot.data.shortcode_media.edge_media_to_comment.edges)
                    {
                        KafkaComment cmt = new KafkaComment();

                        //kafka - lấy dữ liệu từ bảng si_demand_source_post
                        cmt.PostId = item.PostId;
                        cmt.Url = item.Link;
                        //Kafka
                        cmt.CommentId = data.node.id;
                        cmt.CommentText = data.node.text;
                        cmt.OwnerId = data.node.owner.id;
                        cmt.OwnerUser = data.node.owner.username;
                        cmt.OwnerProfilePicUrl = data.node.owner.profile_pic_url;
                        cmt.CreateTime = VCCorp.IG.Core.Helper.DateTimeFormatAgain.UnixTimeStampToDateTime(data.node.created_at);

                        //Đưa vào list và bắn lên kafka
                        _listComment.Add(cmt);
                        //Tìm phân trang nếu có
                        _countCmtDetail += 1;

                        rtxtDisplayResult.AppendText(_countCmtDetail.ToString() + "\t" + cmt.CommentText + "\n");

                    }
                }
                else
                {
                    //bus.Update(item.Id.ToString(), "-1", "");//Cập nhập trạng thái si_demand_source_post là bóc lỗi
                }
                string userId = item.ShortCode;
                Boolean has_next_page = objRoot.data.shortcode_media.edge_media_to_comment.page_info.has_next_page;
                string nextPage1 = objRoot.data.shortcode_media.edge_media_to_comment.page_info.end_cursor;

                if (has_next_page = true && !string.IsNullOrEmpty(nextPage1))
                {
                    EndursorComment(userId, nextPage1);
                }
                else
                {

                }
                _countCmtDetail = 0;
                return;
            }

        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            
        }

        private void timeStart_Tick(object sender, EventArgs e)
        {
            
        }
    }
}