using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCCorp.IG.Core.DTO
{
    // si_demand_source
    public class SiDemandSourceDTO
    {
        public int Id { get; set; }
        public string Platform { get; set; }
        public string SourceId { get; set; }
        public int SourceIdInvalided { get; set; }
        public string Link { get; set; }
        public int Type { get; set; }
        public int Priority { get; set; }
        public string Frequency { get; set; }
        public int FrequencyCrawlCurrentDate { get; set; }
        public string FrequencyCrawlStatusCurrentDate { get; set; }
        public string Demand { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime UpdateTimeCrawl { get; set; }
        public int Status { get; set; }
        public int LockTmp { get; set; }
        public int IsEnded { get; set; }
        public string SourceName { get; set; }
        public DateTime LockTmpTime { get; set; }
        public string MessageError { get; set; }
        public string UserCrawler { get; set; }
        public int TicketId { get; set; }
        public string ServerNameCrawl { get; set; }
    }
}
