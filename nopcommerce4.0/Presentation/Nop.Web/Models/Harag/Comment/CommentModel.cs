using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Harag.Comment
{
    public class CommentModel : BaseNopEntityModel
    {
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CommentedBy { get; set; }
        public string CommentOwner { get; set; }
        public int PostId { get; set; }


        public string DateDescription
        {
            get
            {
                return GetDateDescrition(this.DateCreated);
            }
            set { }
        }

        private string GetDateDescrition(DateTime dateCreated)
        {
            if (dateCreated == null)
                return "";

            var diff = DateTime.Now - dateCreated;

            double s = diff.Milliseconds / 1000;


            double m = 0;
            double h = 0;
            double d = 0;
            double mo = 0;
            double y = 0;

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
