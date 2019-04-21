using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Helpers
{
    public class TimeString
    {
        //static TimeString()
        //{

        //}

        public static TimeString Instance
        {
            get
            {
                return new TimeString();
            }
        }

        public string GetDateDescrition(DateTime dateCreated)
        {
                if (dateCreated == null)
                return "";

            var diff = DateTime.Now - dateCreated;

            long s = (long)diff.TotalMilliseconds / 1000;


            long m = 0;
            long h = 0;
            long d = 0;
            long mo = 0;
            long y = 0;

            if (s < 60)
            {
                return "ثوانِ";
            }

            m = s / 60;

            if (m < 60)
            {
                return " " + m + "دقيقه";
            }

            h = m / 60;

            if (h < 24)
            {
                return " " + h + "ساعه";
            }

            d = h / 24;

            if (d < 30)
            {
                return " " + d + "يوم";
            }

            mo = d / 30;

            if (mo < 12)
            {
                return " " + mo + "شهر";
            }

            y = mo / 12;

            return " " + y + "سنه";
        }

    }
}
