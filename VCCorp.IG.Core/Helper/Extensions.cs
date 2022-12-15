using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace VCCorp.IG.Core.Helper
{
    public static class Extensions
    {
        /// <summary>
        /// Chuyển 1 chuỗi ra Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            var serializer = new JavaScriptSerializer
            {
                MaxJsonLength = int.MaxValue,
            };
            return obj == null ? string.Empty : serializer.Serialize(obj);
        }
    }
}
