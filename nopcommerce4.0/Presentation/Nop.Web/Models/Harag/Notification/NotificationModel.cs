using Nop.Services.Z_Harag.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Harag.Notification
{
    public class NotificationModel
    {
        public int PostId { get; set; }
        public int OwnerId { get; set; }
        public int CustomerId { get; set; }
        public DateTime Time { get; set; }
        public NotificationType Type { get; set; }
        public string OwnerName { get; set; }
        public string Username { get; set; }
        public string PostTitle { get; set; }
        public string Note { get; set; }
        public string CustomerName { get; set; }

        public string DateDescription
        {
            get
            {
                return GetDateDescrition(this.Time);
            }
            set { }
        }

        private string GetDateDescrition(DateTime dateCreated)
        {
            if (dateCreated == null)
                return "";

            var diff = DateTime.Now - dateCreated;

            int s = (int)diff.TotalMilliseconds / 1000;


            int m = 0;
            int h = 0;
            int d = 0;
            int mo = 0;
            int y = 0;

            if (s < 60)
            {
                return "حالا";
            }

            m = s / 60;

            if (m < 60)
            {
                return "قبل " + m + "دقيقه ";
            }

            h = m / 60;

            if (h < 24)
            {
                return "قبل " + h + "ساعه ";
            }

            d = h / 24;

            if (d < 30)
            {
                return "قبل " + d + " يوم";
            }

            mo = d / 30;

            if (mo < 12)
            {
                return "قبل " + mo + " شهر";
            }

            y = mo / 12;

            return "قبل " + y + "سنه ";
        }
    }
}
