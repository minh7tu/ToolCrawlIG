using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCCorp.IG.Core.Helper
{
    public class Scheduing
    {
        public Scheduing()
        {

        }

        public DateTime CalculateDelayTime(string frequency, int timeCrawledAtDay)
        {
            /*
             * 5/1: 5 time at day
             * 2/7: 2 time at week
             * 1/30: once at month
             */
            string[] arrString = frequency.Split('/');
            byte[] arrTime = { byte.Parse(arrString[0]), byte.Parse(arrString[1]) };
            byte delta = 0;

            if (timeCrawledAtDay >= (arrTime[0] / 1.5))
            {
                delta = 1;
            }

            return DateTime.Now.AddHours(((arrTime[1] * 24) / arrTime[0]) + delta);
        }
    }
}
