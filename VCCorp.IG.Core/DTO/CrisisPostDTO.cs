using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCCorp.IG.Core.DTO
{
    //db crisis_post
    public class CrisisPostDTO
    {
        public int Id { get; set; }
        public string PostId { get; set; }
        public string Link { get; set; }
        public string Email { get; set; }
        public int IsStatus { get; set; }
        public int IsDelete { get; set; }
        public DateTime CreateTime { get; set; }
        public int Code { get; set; }
        public int IsGetComment { get; set; }
        public int Type { get; set; }
        public int IsSendMail { get; set; }
        public string BrandName { get; set; }
        public string Platform { get; set; }
        public string DescriptionErr { get; set; }
        public string EmailOperator { get; set; }
        public string PhoneNumber { get; set; }
        public string ShortenLink { get; set; }
        public int IsStandart { get; set; }
    }
}
