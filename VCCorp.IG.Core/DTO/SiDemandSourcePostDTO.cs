using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCCorp.IG.Core.DTO
{
    public class SiDemandSourcePostDTO
    {
        public int Id { get; set; }
        public int SiDemandSourceId { get; set; }
        public string PostId { get; set; }
        public string Platform { get; set; }
        public string Link { get; set; }
        #region Other
        public string ShortCode { get; set; }
        public string LinkCrawler { get; set; }
        public string UserId { get; set; }
        public string NameUser { get; set; }
        public string Fullname { get; set; }
        public string ProfilePicUrl { get; set; }
        public string ImagePost { get; set; }
        #endregion
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CrawledTime { get; set; }
        public int Status { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int TotalComment { get; set; }
        public int TotalLike { get; set; }
        public int TotalShare { get; set; }
        public int LockTmp { get; set; }
        public DateTime LockTmpTime { get; set; }
        public string UserCrawler { get; set; }
        public string ServerNameCrawl { get; set; }

    }
}
