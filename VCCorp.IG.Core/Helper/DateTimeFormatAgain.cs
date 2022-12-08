using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCCorp.IG.Core.Helper
{
    public class DateTimeFormatAgain
    {
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
