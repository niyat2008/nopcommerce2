using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Harag.Post
{
    public class PostModel : BaseNopEntityModel
    {
        public string Text { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime FeaturedDate { get; set; }
        public bool IsClosed { get; set; }
        public bool IsAnswered { get; set; }
        public string PostOwnerFullName { get; set; }
        public int? Rate { get; set; }
        public bool IsDispayed { get; set; }
        public bool IsReserved { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsSetToSubCategory { get; set; }
        public int? SubCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public string PostOwner { get; set; }
        public string City { get; set; }
        public string Photo { get; set; }
        public double Len { get; set; }
        public double Lat { get; set; }
        public int CommentsCount { get; set; }
        public bool IsCommon { get; set; }
         
        /// <summary>
        /// Following Data
        /// </summary>
        public DateTime CreationTime { get; set; }
         
        public string DateDescription
        {
            get
            {
                return GetDateDescrition(this.DateUpdated);
            }
            set { }
        }

        private string GetDateDescrition(DateTime dateCreated)
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
