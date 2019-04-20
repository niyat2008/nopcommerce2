using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Harag.Post
{
    public class PostWithFilesModel : BaseNopEntityModel
    {
        public string Text { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsClosed { get; set; }
        public bool Following { get; set; }
        public int? Rate { get; set; }
        public string PostOwnerFullName { get; set; }
        public bool IsAnswered { get; set; }
        public int SubCategoryId { get; set; }
        public bool IsDispayed { get; set; }
        public double Len { get; set; }
        public double Lat { get; set; }
        public bool IsReserved { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsCommentingClosed { get; set; }
        public int CategoryId { get; set; } 
        public string CategoryName { get; set; }
        public string CityName { get; set; }
        public int CityId { get; set; } 
        public int userId { get; set; } 
        public string PostOwner { get; set; }
        public string Contact { get; set; }
        public UserRating UserRating { get; set; }
        public IList<string> Files { get; set; }
        public List<PostModel> RelatedPosts { get;  set; }
        public List<PostModel> SameCityPosts { get; set; }
        public bool IsOrder { get; set; }
        public PostWithFilesModel()
        {
            Files = new List<string>();
        }


        public string DateDescription
        {
            get
            {
                return GetDateDescrition(this.DateCreated);
            }
            set { }
        }

        public string LastUpdateDateDescription
        {
            get
            {
                return GetDateDescrition(this.DateUpdated);
            }
            set { }
        }
      
        public bool IsPostOwner { get; set; }
        public bool IsUserFeatured { get;  set; }

        private string GetDateDescrition(DateTime dateCreated)
        {
            if (dateCreated == null)
                return "";

            var diff = DateTime.Now - dateCreated;

            int s = (int)diff.TotalMilliseconds/1000;

           
            int m = 0;
            int h = 0;
            int d = 0;
            int mo = 0;
            int y = 0; 

            if ( s < 60)
            {
                return "الان";
            }
             
            m = s / 60;
 
            if(m < 60)
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
