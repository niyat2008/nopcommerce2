using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.HaragAdmin.Post
{
    public class PostModel : BaseNopEntityModel
    {
        public int Id { get; set; }


        public string Title { get; set; }
        public string Text { get; set; }
        public string PaymentMethod { get; set; }
        public string Contact { get; set; }


        public bool IsDispayed { get; set; }
        public bool IsReserved { get; set; }
        public Nullable<bool> IsCommon { get; set; }
        public bool IsClosed { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsAnswered { get; set; }

        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }

        public string City { get; set; }


        public string Customer { get; set; }
        public string Category { get; set; }

    }
}
