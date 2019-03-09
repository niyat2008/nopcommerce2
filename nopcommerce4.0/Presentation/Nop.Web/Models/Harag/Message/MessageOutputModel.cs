using Nop.Web.Controllers.Harag;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Harag.Message
{
    public class MessageOutputModel : BaseNopEntityModel
    {
        public int postId { get; set; }
        public int FromUserId { get; set; }
        public string FromUser { get; set; }
        public DateTime DateTime { get; set; }
        public string Message  { get; set; }

        public string DateDescription
        {
            get
            {
                return GetDateDescrition(this.DateTime);
            }
            set { }
        }

        public MessageType Type { get; internal set; }
        public string ToUser { get; internal set; }
        public int? ToUserId { get; internal set; }

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
                return "قبل " + m + "دقيقه";
            }

            h = m / 60;

            if (h < 24)
            {
                return "قبل " + h + "ساعه";
            }

            d = h / 24;

            if (d < 30)
            {
                return "قبل " + d + "يوم";
            }

            mo = d / 30;

            if (mo < 12)
            {
                return "قبل " + mo + "شهر";
            }

            y = mo / 12;

            return "قبل " + y + "سنه";
        }
    }
}
