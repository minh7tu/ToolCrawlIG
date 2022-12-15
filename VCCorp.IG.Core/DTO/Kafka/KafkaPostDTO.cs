using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCCorp.IG.Core.DTO.Kafka
{
    public class KafkaPostDTO
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string ShortCode { get; set; }
        public string Link { get; set; }
        public int TotalComment { get; set; }
        public int TotalLike { get; set; }
        public int TotalShare { get; set; }
        public int TotalReaction { get; set; }
        public string ImagePost { get; set; }
        public string Platform { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int TmpTime { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string ImageUser { get; set; }
    }
}
