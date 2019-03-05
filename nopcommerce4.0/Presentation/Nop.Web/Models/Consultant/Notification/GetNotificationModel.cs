using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Consultant.Notification
{
    public class GetNotificationModel: BaseNopEntityModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public int OwnerId { get; set; }
        public int? UserId { get; set; }
        public int Type { get; set; }
        public DateTime Time { get; set; }
        public bool IsRead { get; set; }

        public string Owner { get; set; }
        public string User { get; set; }

        public string TimeDescription
        {
            get { return GetTime(this.Time); }
        }


        private string GetTime(DateTime time)
        {
            var difference = DateTime.Now - time;
            int timeInMili = (int)difference.TotalMilliseconds / 1000;

            int s = timeInMili, m=s/60 , h=m/60 , d=h/24, mo=d/30, y=mo/24;
           

            if (s < 60)
                return "حالا";
            if (m < 60)
                return " منذ" + m +" دقيقة";
            if (h < 24)
                return " منذ" + h +" ساعة";
            if (d < 30)
                return "منذ " + d +"يوم";
            if (mo < 12)
                return " منذ" + mo +"شهر";

            return " منذ" + y +"سنة";
        }
    }
}
