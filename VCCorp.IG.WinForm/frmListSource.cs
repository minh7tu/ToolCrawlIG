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
        ChromiumWebBrowser _browser;
        string _pathCache = @"C:\CEFSharp_Cache";
        List<SiDemandSourceDTO> _listSourceIdNull = new List<SiDemandSourceDTO>();
        List<SiDemandSourceDTO> _listSource = new List<SiDemandSourceDTO>();
        List<SiDemandSourcePostDTO> _listPost = new List<SiDemandSourcePostDTO>();
        List<KafkaComment> _listComment = new List<KafkaComment>();
        SiDemandSourcePostBUS _busPost = new SiDemandSourcePostBUS();
        static int count ;
        static int _countCmt = 0;
        static int _countCmtDetail = 0;
        static int _countPost = 0;
        //int _flag;//đánh cờ để chạy

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
        private void btnPost_Click(object sender, EventArgs e)
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

        //Bóc lấy bài post
        private void btnCrawlerPost_Click(object sender, EventArgs e)
        {

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
                                
                                rtxtDisplayResult.AppendText(_countPost.ToString()+"\t"+ dto.Link+"\n");
                                //_listPost.Add(dto);
                                //_busPost.Insert(dto);

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

                            //bus.Update(item.Id.ToString(), "2", "done", crawlcurrentdate , crawlerDate); // lưu vào db
                        }
                    }
                    else
                    {
                        //bus.Update(item.Id.ToString(), "-1", "", "","");//lưu vào db
                    }
                }
                catch (Exception ex)
                {
                    //bus.Update(item.Id.ToString(), "-1", "", "","");//lưu vào db
                }


            }
        }

        //Lấy dữ liệu trang post tiếp
        private void Endursor(string UserId, string nextPage)
        {
            SiDemandSourceBUS bus = new SiDemandSourceBUS();
            SiDemandSourcePostDTO dto = new SiDemandSourcePostDTO();

            foreach (var item in _listSource)
            {
                string urlpage = "https://www.instagram.com/graphql/query/?query_hash=472f257a40c653c64c666ce877d59d2b&variables={%22id%22:%22" + UserId + "%22,%22first%22:50,%22after%22:%22" + nextPage + "%22}";
                _browser.Load(urlpage);
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
                                //_busPost.Insert(dto);

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
                        //bus.Update(id.ToString(), "-1", "", "","");//lưu vào db
                    }
                }
                catch (Exception ex)
                {
                    //bus.Update(id.ToString(), "-1", "", "","");//lưu vào db
                }
                return;
            }
        }

        private void btnLoginIG_Click(object sender, EventArgs e)
        {
            InitBrowser();
        }

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

        private void btnLocation_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đang phát triển", "Thông báo");
        }

        private void btnOriginAudio_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đang phát triển", "Thông báo");
        }

        private void btnHashtag_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Đang phát triển", "Thông báo");
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
                source = GetSourceFromBrowser();

                source = Regex.Replace(source, "(<html)(.*?)(pre-wrap;\">)", "", RegexOptions.IgnoreCase); // xóa cặp thẻ
                source = Regex.Replace(source, "</pre></body></html>", "", RegexOptions.IgnoreCase);
                var ObjRoot = JsonConvert.DeserializeObject<Core.DTO.JsonToObjectIG.SourceId.Root>(source);

                if (ObjRoot != null)
                {
                    dto.SourceId = ObjRoot.graphql.user.id;
                    if (string.IsNullOrEmpty(dto.SourceId))
                    {
                        bus.Update(dto.Id.ToString(), dto.SourceId);
                    }
                    else
                    {
                        rtxtDisplayResult.AppendText(dto.Id.ToString());
                        //dto.SourceId = "-1";
                        //bus.Update(dto.Id.ToString(), dto.SourceId);
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
        }
        
        //Bóc từng bài rồi lấy comment
        private void btnCrawlerComment_Click(object sender, EventArgs e)
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
                //bus.Update(item.Id.ToString(), "1", "");
                _browser.Load(item.LinkCrawler);
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
                        
                        rtxtDisplayResult.AppendText(_countCmtDetail.ToString()+"\t"+cmt.CommentText + "\n");
                                          
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
                    //bus.Update(item.Id.ToString(), "2", "");
                }
                else
                {
                    //bus.Update(item.Id.ToString(), "-1", "");
                }    
            }
            lblSum.Text = countCmt.ToString();
        }

        //Bóc comment tiếp
        private void EndursorComment(string shortCode, string nextPage)
        {
            SiDemandSourcePostBUS bus = new SiDemandSourcePostBUS();
            //SiDemandSourcePostDTO dto = new SiDemandSourcePostDTO();
            KafkaComment cmt = new KafkaComment();


            _listPost = bus.GetListSourcePost();

            foreach (var item in _listPost)
            {
                //bus.Update(item.Id.ToString(), "1", "");
                string urlpage = "https://www.instagram.com/graphql/query/?query_hash=33ba35852cb50da46f5b5e889df7d159&variables=%7B%22shortcode%22:%22" + shortCode + "%22,%22first%22:100,%22after%22:%22" + nextPage + "%22%7D";
                _browser.Load(urlpage);
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
                        //Tìm phân trang nếu có
                        _countCmtDetail += 1;

                        rtxtDisplayResult.AppendText(_countCmtDetail.ToString() + "\t" + cmt.CommentText + "\n");

                    }                     
                }
                else
                { 
                        //bus.Update(item.Id.ToString(), "-1", "");
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
    }
}  
