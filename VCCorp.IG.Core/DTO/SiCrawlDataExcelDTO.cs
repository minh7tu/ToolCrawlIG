using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCCorp.IG.Core.DTO
{
    /// <summary>
    /// si_crawl_data_excel
    /// </summary>
    public class SiCrawlDataExcelDTO
    {
        public int Id { get; set; }
        public string PostId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }
        public string Link { get; set; }

        public string LinkCrawl { get; set; }//ohter
        public string ShortCode { get; set; }//ohther
        public string ProfileId { get; set; }//oither
        public string Platform { get; set; }
        public int Topic { get; set; }
        public string TopicName { get; set; }
        public int Code { get; set; }
        public string SourceName { get; set; }
        public string Sentiment { get; set; }
        public string DescriptionErr { get; set; }
        public int IsStandart { get; set; }
        public int CrawlerPerDate { get; set; }
        public int IsEnded { get; set; }
        public int Priority { get; set; }
        public string Frequency { get; set; }
        public int TicketId { get; set; }
    }
}
