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
using VCCorp.IG.Core.Helper;

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
        List<KafkaCommentDTO> _listComment = new List<KafkaCommentDTO>();
        SiDemandSourcePostBUS _busPost = new SiDemandSourcePostBUS();
        static int count;
        static int _countCmt = 0;
        static int _countCmtDetail = 0;
        static int _countPost = 0;
        int _flag;
        int _load;
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
            //_load = 1;
            _flag = 1;
            GetListSiDemandSource();//Lấy danh sách source trong bảng si_demand_source
            GetListSiDemandSourcePost();//Lấy danh sách các bài post trong si_demand_source_post
            GetListSCDEPost();
            GetListSCDEComment();
            GetSourceIdNull();//Lấy danh sách source id null  trong bảng si_demand_source

            //txtOptionsAuto.Text = "3";

            //switch (Convert.ToInt32(txtOptionsAuto.Text))
            //{
            //    case 1:
            //        txtStatusTooltip.Text = "Đã login tài khoản IG. Hệ thống sẽ tiến hành chạy tự động trong vòng vài giây...-Si_Demand_Source";
            //        //timeStart.Interval = 1000 * 10;
            //        timerStartSDS.Enabled = true;
            //        break;
            //    case 2:
            //        break;
            //    case 3:
            //        txtStatusTooltip.Text = "Đã login tài khoản IG. Hệ thống sẽ tiến hành chạy tự động trong vòng vài giây...-Si_Crawl_Data_Excel";
            //        Thread.Sleep(6000);
            //        timeStart.Enabled = true;
            //        break;
            //}

            //txtStatusTooltip.Text = "Đã login tài khoản IG. Hệ thống sẽ tiến hành chạy tự động trong vòng vài giây...";
            ////timeStart.Interval = 1000 * 10;
            //timeStart.Enabled = true;          
        }

        //Lấy danh sách source trong bảng si_demand_source
        private void GetListSiDemandSource()
        {
            SiDemandSourceBUS source = new SiDemandSourceBUS();
            //SiDemandSourceDTO dto = new SiDemandSourceDTO();

            int i = 1;
            //txtOptions.Text = Convert.ToString("0");

            try
            {
                _listSource = source?.GetList();
                
            }
            catch
            {
                rtxtDisplayResult.Text = "Không được bỏ trống trạng thái";
                return;
            }

            //rtxtDisplayResult.Clear();

            //if (_listSource.Count != 0)
            //{
            //    foreach (var item in _listSource)
            //    {
            //        //Note:sẽ bổ sung thêm switch case các trường hợp nếu có bản ghi 
            //        switch (Convert.ToInt32(txtOptions.Text))
            //        {
            //            case 0:
            //                rtxtDisplayResult.Text = "Kết quả chờ bóc: " + _listSource.Count + "\n";
            //                break;
            //            case 1:
            //                rtxtDisplayResult.Text = "Kết quả đang bóc: " + _listSource.Count + "\n";
            //                break;
            //            case -1:
            //                rtxtDisplayResult.Text = "Kết quả lỗi: " + _listSource.Count + "\n";
            //                break;
            //            case 2:
            //                rtxtDisplayResult.Text = "Kết quả bóc done: " + _listSource.Count + "\n" + "-------------------" + "\n";
            //                rtxtDisplayResult.AppendText("STT: " + i + Environment.NewLine);
            //                i += 1;
            //                rtxtDisplayResult.AppendText("Id: " + item.Id.ToString() + Environment.NewLine);
            //                rtxtDisplayResult.AppendText("Platform: " + item.Platform.ToString() + Environment.NewLine);
            //                rtxtDisplayResult.AppendText("Link Profile: " + item.Link.ToString() + Environment.NewLine);
            //                rtxtDisplayResult.AppendText(Environment.NewLine + "------------------------" + Environment.NewLine);
            //                break;
            //            default:
            //                MessageBox.Show("Điền số:\n1: Đang bóc\n2: Bóc thành công\n0: Chờ bóc \n-1: Lỗi", "Thông báo");
            //                break;
            //        }
                   
            //    }
            //}
            //else
            //{
                
            //    switch (Convert.ToInt32(txtOptions.Text))
            //    {
            //        case 0:
            //            rtxtDisplayResult.Text = "Không có kết quả chờ bóc";
            //            break;
            //        case 1:
            //            rtxtDisplayResult.Text = "Không có kết quả đang bóc";
            //            break;
            //        case -1:
            //            rtxtDisplayResult.Text = "Không có kết quả lỗi";
            //            break;
            //        case 2:
            //            rtxtDisplayResult.Text = "Không có kết quả bóc done";
            //            break;
            //        default:
            //            MessageBox.Show("Điền số:\n1: Đang bóc\n2: Bóc thành công\n0: Chờ bóc \n-1: Lỗi", "Thông báo");
            //            break;
            //    }

            //}
        }

        //Lấy danh sách các bài post trong bảng si_demand_source_post
        private void GetListSiDemandSourcePost()
        {
            SiDemandSourcePostBUS bus = new SiDemandSourcePostBUS();
            _listPost = bus.GetListSourcePost();
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
                    _load = 0; //Đánh dấu trạng thái chưa login vào IG
                    
                }
                else
                {
                   
                    _load = 1;//Đánh dấu trạng thái đã login
                    
                }    

                CefSharp.WinForms.CefSettings settings = new CefSharp.WinForms.CefSettings();
                settings.CachePath = _pathCache;
                settings.LogSeverity = LogSeverity.Disable;

                CefSharp.Cef.Initialize(settings);
            }

            
            _browser = new ChromiumWebBrowser("https://www.instagram.com/");
            
            this.pnlCefsharp.Controls.Add(_browser);

            //_load = 0; //Đánh dấu trạng thái đang login vào IG
        }

        //Cập nhập source id null trong bảng si_demand_source
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
                        bus.Update(dto.Id.ToString(), dto.SourceId);//Cập nhập lại source id khi nó bị null trong bảng si_demand_source khi thành công
                    }
                    else
                    {
                        rtxtDisplayResult.AppendText(dto.Id.ToString());
                        //dto.SourceId = "-1";
                        bus.Update(dto.Id.ToString(), dto.SourceId);//cập nhập lại source id null trong bảng si_demand_source khi lỗi
                    }

                }

            }
        }

        //Load source từ trình duyệt
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

        private void btnCrawlerComment_Click(object sender, EventArgs e)
        {
            CrawlerSDSComment();
        }
        
        //Chạy tự động theo các lựa chọn
        private void btnFresh_Click(object sender, EventArgs e)
        {
            try
            {
                switch (Convert.ToInt32(txtOptionsAuto.Text))
                {
                    case 1:
                        txtStatusTooltip.Text = "Đã login tài khoản IG. Hệ thống sẽ tiến hành chạy tự động trong vòng vài giây...-Si_Demand_Source";
                        timerStartSDS.Interval = 1000 * 10;
                        timerStartSDS.Enabled = true;
                        timerStartSDS_Tick(sender, e);
                        break;
                    case 2:
                        txtStatusTooltip.Text = "Đã login tài khoản IG. Hệ thống sẽ tiến hành chạy tự động trong vòng vài giây...-Si_Demand_Source_Post";
                        timerStartSDSP.Interval = 1000 * 10;
                        timerStartSDSP.Enabled = true;
                        timerStartSDSP_Tick(sender, e);
                        break;
                    case 3:
                        txtStatusTooltip.Text = "Đã login tài khoản IG. Hệ thống sẽ tiến hành chạy tự động trong vòng vài giây...-Si_Crawl_Data_Excel";
                        timeStart.Interval = 1000 * 10;
                        timeStart.Enabled = true;
                        timeStart_Tick(sender, e);
                        break;
                    default:
                        MessageBox.Show("Nhập \n1: Bóc tự động bảng Si_Demand_Source\n2: Bóc tự động bảng Si_Demand_Source_Post\n3: Bóc tự động bảng Si_Crawl_Data_Excel", "Cảnh báo");
                        break;
            }
            }
            catch
            {
                MessageBox.Show("Nhập \n1: Bóc tự động bảng Si_Demand_Source\n2: Bóc tự động bảng Si_Demand_Source_Post\n3: Bóc tự động bảng Si_Crawl_Data_Excel", "Cảnh báo");
            }
           

        }

        private void btnSCDEComment_Click(object sender, EventArgs e)
        {
            CrawlerSCDEComment();
        }

        private void btnSCDEPost_Click(object sender, EventArgs e)
        {
            CrawlerSCDEPost();
        }

        //Lấy danh sách link trong bảng si_crawl_data_excel bóc post
        private void GetListSCDEPost()
        {
            SiCrawlDataExcelBUS bus = new SiCrawlDataExcelBUS();

            _listPostSCDE = bus.GetListPost();//Lấy danh sách status = 0 trong bảng si_crawl_data_excel
        }
        //Lấy danh sách link trong bảng si_crawl_data_excel bóc comment
        private void GetListSCDEComment()
        {
            SiCrawlDataExcelBUS bus = new SiCrawlDataExcelBUS();
            _listCommentSCDE = bus.GetListComment();//Lấy danh sách
        }

        //Bóc post của bảng si_crawl_data_excel 
        private async void CrawlerSCDEPost()
        {
            int dem = 1;
            SiCrawlDataExcelBUS bus = new SiCrawlDataExcelBUS();

            //_listPostSCDE = bus.GetListPost();//Lấy danh sách status = 0 trong bảng si_crawl_data_excel

            foreach (var item in _listPostSCDE)
            {
                
                _browser.Load(item.LinkCrawl);
                
                Thread.Sleep(6000);
               
                //txtResutlUrl.Text = item.LinkCrawl;
                bus.Update(item.Id, "", 1);//Cập nhập trạng thái trên bảng si_crawl_data_excel đang bóc
                string source = GetSourceFromBrowser();

                source = Regex.Replace(source, "(<html)(.*?)(pre-wrap;\">)", "", RegexOptions.IgnoreCase); // xóa cặp thẻ
                source = Regex.Replace(source, "</pre></body></html>", "", RegexOptions.IgnoreCase);

                var profileRoot = JsonConvert.DeserializeObject<SourceA1.Root>(source);

                if (profileRoot != null && profileRoot.items != null)
                {
                    dem += 1;

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

                        
                        //rtxtDisplayResult.AppendText(dto.Content + "\n");
                        //Đưa vào db si_excel_history

                        //bắn lên kafka
                        KafkaPostDTO kafka = new KafkaPostDTO();
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

                        await SaveKafka(kafka);
                    }
                    bus.Update(item.Id, "", 3);//Cập nhập trạng thái trên bảng si_crawl_data_excel đã bóc xong chờ bóc comment
                }
                else
                {
                    bus.Update(item.Id, "", -1);//Cập nhập trạng thái trên bảng si_crawl_data_excel lỗi
                }
                txtStatusTooltip.Text = "Bóc post bảng si_crawl_data_excel: " + dem ;
                Thread.Sleep(10000);
            }

            
        }

        //Bóc comment của bảng si_crawl_data_excel 
        private async void CrawlerSCDEComment()
        {
            SiCrawlDataExcelBUS bus = new SiCrawlDataExcelBUS();
            //_listCommentSCDE = bus.GetListComment();//Lấy danh sách
            int dem = 1;

            foreach (var item in _listCommentSCDE)
            {
                string linkcrawl = "https://www.instagram.com/graphql/query/?query_hash=33ba35852cb50da46f5b5e889df7d159&variables=%7B%22shortcode%22:%22" + item.ShortCode + "%22,%22first%22:100,%22after%22:%22%22%7D";
                _browser.Load(linkcrawl);
                Thread.Sleep(6000);
                //txtResutlUrl.Text = linkcrawl;

                string source = GetSourceFromBrowser();
                bus.Update(item.Id, "", 1);//Cập nhập trạng thái trên bảng si_crawl_data_excel đang bóc
                source = Regex.Replace(source, "(<html)(.*?)(pre-wrap;\">)", " ", RegexOptions.IgnoreCase); // xóa cặp thẻ
                source = Regex.Replace(source, "</pre></body></html>", "", RegexOptions.IgnoreCase);

                var objRoot = JsonConvert.DeserializeObject<CommentOfPost.Root>(source);

                if (objRoot != null && objRoot.data != null)
                {
                    dem += 1;
                    
                    foreach (var data in objRoot.data.shortcode_media.edge_media_to_comment.edges)
                    {
                        KafkaCommentDTO cmt = new KafkaCommentDTO();

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

                        await SaveKafka(cmt);
                        //Đưa vào list và bắn lên kafka
                        _listComment.Add(cmt);

                        _countCmtDetail += 1;

                        //rtxtDisplayResult.AppendText(_countCmtDetail.ToString() + "\t" + cmt.CommentText + "\n");

                    }
                    bus.Update(item.Id, "", 2);//Cập nhập trạng thái trên bảng si_crawl_data_excel bóc thành công
                }
                else
                {
                    bus.Update(item.Id, "", 2);//Cập nhập trạng thái trên bảng si_crawl_data_excel không bóc được comment
                }
                txtStatusTooltip.Text = "Bóc comment bảng si_crawl_data_excel: " + dem;
                Thread.Sleep(10000);
            }

            _flag = 10;
        }

        //Bóc post từ bảng si_demand_source 
        private async void CrawlerSDSPost()
        {
            //GetListSiDemandSource();

            //txtStatusTooltip.Text = "Đang thực hiện bóc Post từ bảng Si_Demand_Source";

            SiDemandSourceBUS bus = new SiDemandSourceBUS();
            VCCorp.IG.Core.Helper.Scheduing scheduing = new Core.Helper.Scheduing();

            SiDemandSourcePostDTO dto = new SiDemandSourcePostDTO();

            foreach (var item in _listSource)
            {
                _browser.Load(item.Link);
                Thread.Sleep(6000);
                //txtResutlUrl.Text = item.Link;
                //var id = item.Id;
                //dto.SiDemandSourceId = id;
                //dto.Platform = item.Platform;

                bus.Update(item.Id.ToString(), "1", "in process", (item.FrequencyCrawlCurrentDate + 1).ToString(), "");//Cập nhập trạng thái trên bảng si_demand_source - đang bóc

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
                                dto.ServerNameCrawl = Core.Helper.Utilies.GetLocalIP();

                                //bắn post lên kafka
                                KafkaPostDTO kafka = new KafkaPostDTO();
                                kafka.Id = dto.PostId;
                                kafka.Message = dto.Content;
                                kafka.ShortCode = data.node.shortcode;
                                kafka.Link = dto.Link;
                                kafka.TotalComment = dto.TotalComment;
                                kafka.TotalLike = dto.TotalLike;
                                kafka.TotalShare = 0;
                                kafka.TotalReaction = 0;
                                kafka.ImagePost = dto.ImagePost;
                                kafka.Platform = dto.Platform;
                                kafka.CreateTime = dto.CreateTime;
                                kafka.UpdateTime = DateTime.Now;
                                kafka.TmpTime = data.node.taken_at_timestamp;
                                kafka.UserId = dto.UserId;
                                kafka.Username = "";
                                kafka.ImageUser = "";

                                await SaveKafka(kafka);

                                //Lưu tạm vào list và db
                                _countPost += 1;

                                rtxtDisplayResult.AppendText(_countPost.ToString() + "\t" + dto.Link + "\n");
                                //_listPost.Add(dto);
                                _busPost.Insert(dto);//Thêm bản ghi vào bảng si_demand_source_post

                            }


                            //Tìm phân trang 
                            string userId = item.SourceId;
                            Boolean hasNextpage = profileRoot.data.user.edge_owner_to_timeline_media.page_info.has_next_page;
                            string endCursor = profileRoot.data.user.edge_owner_to_timeline_media.page_info.end_cursor;

                            if (hasNextpage = true && !string.IsNullOrEmpty(endCursor))
                            {
                                Endursor(userId, endCursor);
                            }

                            int crawlcurrentdate = item.FrequencyCrawlCurrentDate +1 ;
                            crawlcurrentdate += 1;

                            //string crawlerDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                            string crawlerDate = scheduing.CalculateDelayTime(item.Frequency, item.FrequencyCrawlCurrentDate).ToString("MM/dd/yyyy HH:mm:ss");

                            bus.Update(item.Id.ToString(), "2", "done", crawlcurrentdate.ToString()  , crawlerDate); // Cập nhập trạng thái trong bảng si_demand_source đã bóc hoàn thành
                        }
                    }
                    else
                    {                      
                        bus.Update(item.Id.ToString(), "-1", " ", " "," ");//cập nhập trạng thái là bóc lỗi trong bảng si_demand_source
                    }
                }
                catch (Exception)
                {
                    bus.Update(item.Id.ToString(), "-1", " ", " ", " ");//cập nhập trạng thái là bóc lỗi trong bảng si_demand_source
                }

               
            }
            // txtStatusTooltip.Text = "Đã xong quá trình tự động bóc post bảng si_demand_source";
            _flag = 10;
        }
        
        //Bóc trang tiếp của post hiện tại bảng si_demand_source
        private async void Endursor(string UserId, string nextPage)
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
                               
                                //bắn post lên kafka
                                KafkaPostDTO kafka = new KafkaPostDTO();
                                kafka.Id = dto.PostId;
                                kafka.Message = dto.Content;
                                kafka.ShortCode = data.node.shortcode;
                                kafka.Link = dto.Link;
                                kafka.TotalComment = dto.TotalComment;
                                kafka.TotalLike = dto.TotalLike;
                                kafka.TotalShare = 0;
                                kafka.TotalReaction = 0;
                                kafka.ImagePost = dto.ImagePost;
                                kafka.Platform = dto.Platform;
                                kafka.CreateTime = dto.CreateTime;
                                kafka.UpdateTime = DateTime.Now;
                                kafka.TmpTime = data.node.taken_at_timestamp;
                                kafka.UserId = dto.UserId;
                                kafka.Username = "";
                                kafka.ImageUser = "";

                                await SaveKafka(kafka);
                                //Lưu tạm vào list , lưu db
                                //_listPost.Add(dto);
                                _countPost += 1;

                                rtxtDisplayResult.AppendText(_countPost.ToString() + "\t" + dto.Link + "\n");
                                _busPost.Insert(dto);//Thêm bản ghi vào bảng si_demand_source_post

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
        private async void CrawlerSDSComment()
        {
            //txtStatusTooltip.Text = "Đang bóc comment từ bảng Si_Demand_Source_Post";

            SiDemandSourcePostBUS bus = new SiDemandSourcePostBUS();
            //SiDemandSourcePostDTO dto = new SiDemandSourcePostDTO();
            KafkaCommentDTO cmt = new KafkaCommentDTO();
            int countCmt = 0;

            //_listPost = bus.GetListSourcePost();

            foreach (var item in _listPost)
            {
                countCmt += 1;
                _countCmt += 1;
                txtStatusTooltip.Text = "Bóc comment bảng si_demand_source_post: " + countCmt;

                

                //Thread.Sleep(10000);
                //txtStatusTooltip.Text = "Bóc comment bảng si_demand_source_post: " + _countCmt;
                //Thread.Sleep(10000);
                //txtStatusTooltip.Text = "";
                //rtxtDisplayResult.AppendText("Link: " + _countCmt.ToString() + "-----------------\n\n");
                bus.Update(item.Id.ToString(), "1", "");//Cập nhập trạng thái trong bảng si_demand_source_post là đang bóc
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
                        await SaveKafka(cmt);
                        _countCmtDetail += 1;

                        //rtxtDisplayResult.AppendText(_countCmtDetail.ToString() + "\t" + cmt.CommentText + "\n");

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
                    bus.Update(item.Id.ToString(), "2", "");//Cập nhập trạng thái trong bảng si_demand_source_post hoàn thành
                }
                else
                {
                    bus.Update(item.Id.ToString(), "-1", "");//Cập nhập trạng thái trong bảng si_demand_source_post lỗi
                }
            }
            //lblSum.Text = countCmt.ToString();
            _flag = 10;
            //txtStatusTooltip.Text = "Hoàn tất bóc Comment bảng Si_Demand_Source_Post";
            //Thread.Sleep(10000);
        }

        //Bóc comment của bảng si_demand_source_post (phân trang)
        private async void EndursorComment(string shortCode, string nextPage)
        {
            SiDemandSourcePostBUS bus = new SiDemandSourcePostBUS();
            //SiDemandSourcePostDTO dto = new SiDemandSourcePostDTO();

            _listPost = bus.GetListSourcePost();

            foreach (var item in _listPost)
            {
                string urlpage = "https://www.instagram.com/graphql/query/?query_hash=33ba35852cb50da46f5b5e889df7d159&variables=%7B%22shortcode%22:%22" + shortCode + "%22,%22first%22:100,%22after%22:%22" + nextPage + "%22%7D";
                _browser.Load(urlpage);
                //txtResutlUrl.Text = urlpage;
                Thread.Sleep(6000);

                string source = GetSourceFromBrowser();

                source = Regex.Replace(source, "(<html)(.*?)(pre-wrap;\">)", " ", RegexOptions.IgnoreCase); // xóa cặp thẻ
                source = Regex.Replace(source, "</pre></body></html>", "", RegexOptions.IgnoreCase);

                try
                {
                    var objRoot = JsonConvert.DeserializeObject<CommentOfPost.Root>(source);

                    if (objRoot != null && objRoot.data != null)
                    {
                        foreach (var data in objRoot.data.shortcode_media.edge_media_to_comment.edges)
                        {
                            KafkaCommentDTO cmt = new KafkaCommentDTO();

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
                            await SaveKafka(cmt);
                            //rtxtDisplayResult.AppendText(_countCmtDetail.ToString() + "\t" + cmt.CommentText + "\n");

                        }
                    }
                    else
                    {
                        //bus.Update(item.Id.ToString(), "-1", "");//Cập nhập trạng thái si_demand_source_post là bóc lỗi
                    }

                    string userId = item.ShortCode;
                    Boolean has_next_page = objRoot?.data?.shortcode_media?.edge_media_to_comment?.page_info?.has_next_page ?? false;
                    string nextPage1 = objRoot?.data?.shortcode_media?.edge_media_to_comment?.page_info.end_cursor ?? "";

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
                catch
                {
                    return;
                }
                //string userId = item.ShortCode;
                //Boolean has_next_page = objRoot?.data.shortcode_media.edge_media_to_comment.page_info.has_next_page == null ? false : true;
                //string nextPage1 = objRoot?.data.shortcode_media.edge_media_to_comment.page_info.end_cursor;

                //if (has_next_page = true && !string.IsNullOrEmpty(nextPage1))
                //{
                //    EndursorComment(userId, nextPage1);
                //}
                //else
                //{

                //}
                //_countCmtDetail = 0;
                //return;
            }

        }
  
        private void timeStart_Tick(object sender, EventArgs e)
        {           
            SchedulingSiCrawlDataExcel();
        }

        //Lập lịch chạy tự động bảng Si_Crawl_Data_Excel
        private void SchedulingSiCrawlDataExcel()
        {           
            if (_load == 0)
            {
                
                txtStatusTooltip.Text = "Hệ thống đang chờ login vào IG";
                timeStart.Stop();
                return;//Do chưa thực hiện đăng nhập
            }

            if(_flag == 1)
            {
                if (_listPostSCDE.Count == 0 && _listCommentSCDE.Count == 0)
                {
                    Thread.Sleep(6000);
                    txtStatusTooltip.Text = "Hiện tại chưa có link để bóc post và comment của bảng si_crawl_data_excel - Dừng";
                    timeStart.Enabled = false;
                    timeStart.Stop();
                    _flag = 10;
                    //btnAutoSiDataExcel.Enabled = true;
                    return;
                }
                else if (_listPostSCDE.Count > 0 || _listCommentSCDE.Count > 0)
                {
                    if (_listPostSCDE.Count > 0)
                    {
                        txtStatusTooltip.Text = "Đang có " + _listPostSCDE.Count + " link trong bảng si_crawl_data_excel cần bóc lấy post";
                        Thread.Sleep(6000);
                        Thread th = new Thread(new ThreadStart(CrawlerSCDEPost));
                        th.Start();
                    }

                    if (_listCommentSCDE.Count > 0)
                    {
                        txtStatusTooltip.Text = "Đang có " + _listCommentSCDE.Count + " link trong bảng si_crawl_data_excel cần bóc lấy comment";
                        Thread.Sleep(6000);
                        Thread th2 = new Thread(new ThreadStart(CrawlerSCDEComment));
                        th2.Start();
                    }                  
                }
            }                                   

            if (_flag == 10)
            {
                txtStatusTooltip.Text = "Đã bóc xong 1 vòng chờ 2 phút để bóc tiếp - Si_Crawl_Data_Excel";
                Thread.Sleep(6000);
                timeStart.Interval = 1000 * 60 * 2;
                timeStart.Enabled = true;
                _flag = 1;
                return;
            }
        }

        //Lập lịch chạy bảng Si_Demand_Source
        private void SchedulingSiDemandSource()
        {
            if (_load == 0)
            {

                txtStatusTooltip.Text = "Hệ thống đang chờ login vào IG";
                timerStartSDS.Stop();
                return;//Do chưa thực hiện đăng nhập
            }

            if(_flag == 1)
            {
                if(_listSource.Count == 0 && _listSource != null)
                {
                    Thread.Sleep(6000);
                    txtStatusTooltip.Text = "Hiện tại chưa có link để bóc post của bảng si_demand_source - Dừng";
                    timerStartSDS.Enabled = false;
                    timerStartSDS.Stop();
                    //_flag = 10;
                    //btnAutoSiDataExcel.Enabled = true;
                    return;
                }  
                else
                {
                    txtStatusTooltip.Text = "Đang có " + _listSource.Count + " link trong bảng si_demand_source cần bóc lấy post";
                    Thread.Sleep(6000);
                    //Thread th = new Thread(new ThreadStart(CrawlerSDSPost));
                    //th.Start();
                    CrawlerSDSPost();
                }    
            }

            if (_flag == 10)
            {
                GetListSiDemandSource();
                txtStatusTooltip.Text = "Đã bóc xong 1 vòng chờ 2 phút để bóc tiếp - Si_Demand_Source";
                Thread.Sleep(6000);
                timerStartSDS.Interval = 1000 * 60 * 2;
                timerStartSDS.Enabled = true;
                _flag = 1;
                return;
            }

        }

        //Lập lịch chạy bảng Si_Demand_Source_Post
        private void SchedulingSiDemandSourcePost()
        {
            if (_load == 0)
            {

                txtStatusTooltip.Text = "Hệ thống đang chờ login vào IG";
                timerStartSDSP.Stop();
                return;//Do chưa thực hiện đăng nhập
            }

            if (_flag == 1)
            {
                if (_listPost.Count == 0 && _listPost != null)
                {
                    Thread.Sleep(6000);
                    txtStatusTooltip.Text = "Hiện tại chưa có link để bóc comment của bảng si_demand_source_post - Dừng";
                    timerStartSDSP.Enabled = false;
                    timerStartSDSP.Stop();
                    //_flag = 10;
                    //btnAutoSiDataExcel.Enabled = true;
                    return;
                }
                else
                {
                    txtStatusTooltip.Text = "Đang có " + _listPost.Count + " link trong bảng si_demand_source_post cần bóc lấy comment";
                    Thread.Sleep(6000);
                    Thread th = new Thread(new ThreadStart(CrawlerSDSComment));
                    th.Start();
                    //CrawlerSDSComment();
                }    
            }

            if (_flag == 10)
            {
                txtStatusTooltip.Text = "Đã bóc xong 1 vòng chờ 2 phút để bóc tiếp - Si_Demand_Source_Post";
                Thread.Sleep(6000);
                timerStartSDSP.Interval = 1000 * 60 * 2;
                timerStartSDSP.Enabled = true;
                _flag = 1;
                return;
            }

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timerStartSDSP_Tick(object sender, EventArgs e)
        {
            SchedulingSiDemandSourcePost();
        }

        private void timerStartSDS_Tick(object sender, EventArgs e)
        {
            SchedulingSiDemandSource();
        }

        //Bắn post lên kafka
        private async Task SaveKafka(KafkaPostDTO dto)
        {
            try
            {
                string msg = dto.ToJson();

                string putPost = await Kafka.PutOnKafkaPostINS(msg);
            }
            catch(Exception ex)
            {

            }
        }

        //Bắn comment lên kafka
        private async Task SaveKafka(KafkaCommentDTO dto)
        {
            try
            {
                string msg = dto.ToJson();

                string putPost = await Kafka.PutOnKafkaCmtINS(msg);
            }
            catch (Exception ex)
            {

            }
        }
    }
}