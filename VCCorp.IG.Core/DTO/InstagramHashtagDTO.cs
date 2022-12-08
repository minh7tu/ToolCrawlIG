using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCCorp.IG.Core.DTO
{
    /// <summary>
    // instagram_hashtag
    public class InstagramHashtagDTO
    {
        public int Id { get; set; }
        public string Hashtag { get; set; }
        public int Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
