using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Consultant.Api.Models.Post
{
    public class PostWithFilesModel : BaseNopEntityModel
    {
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsClosed { get; set; }
        public int? Rate { get; set; }
        public bool IsAnswered { get; set; }
        public int SubCategoryId { get; set; }
        public bool IsDispayed { get; set; }
        public bool IsReserved { get; set; }
        public int CategoryId { get; set; }
        public bool IsSetToSubCategory { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string PostOwner { get; set; }

        public IList<string> Files { get; set; }

        public PostWithFilesModel()
        {
            Files = new List<string>();
        }
    }
}
