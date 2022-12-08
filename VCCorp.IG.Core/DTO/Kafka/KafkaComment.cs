using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCCorp.IG.Core.DTO.Kafka
{
    public class KafkaComment
    {
        public string Id { get; set; }
        public string CommentId { get; set; }
        public string PostId { get; set; }
        public string Url { get; set; }
        public bool HasNextPage { get; set; }
        public int EndCursor { get; set; }
        public string CommentText { get; set; }
        public double CreateAt { get; set; }
        public string OwnerId { get; set; }
        public string ShortCode { get; set; }
        public string OwnerUser { get; set; }
        public string OwnerProfilePicUrl { get; set; }
        public DateTime TimePost { get; set; }
        public DateTime CmtAtDate { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
