using Nop.Core.Domain.Z_Consultant;
using Nop.Core.Domain.Customers;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.ConsultantAdmin.Customers
{
    public class CustomerPostsModel:BaseNopEntityModel
    {
        public int Id { get; set; }
        public string Username { get; set; }


        public string Email { get; set; }

        public string Mobile { get; set; }

        public string CustomerRole { get; set; }
        public string LastIpAddress { get; set; }
        public bool Active { get; set; }
        public DateTime LastActivityDateUtc { get; set; }

        public string SystemName { get; set; }

        public int PostId { get; set; }
        public string Filter { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsClosed { get; set; }
        public bool IsAnswered { get; set; }
        public int? Rate { get; set; }
        public bool IsDispayed { get; set; }
        public bool IsReserved { get; set; }
        public bool IsSetToSubCategory { get; set; }
        public int? SubCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public string PostOwner { get; set; }
        public string Url { get; set; }
        public CustomerPostsModel()
        {
            PostId = 0;
        }
    }
}
