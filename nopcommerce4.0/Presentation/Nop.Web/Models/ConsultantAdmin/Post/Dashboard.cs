using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.ConsultantAdmin.Post
{
    public class Dashboard:BaseNopEntityModel
    {
        public int PostsCount { get; set; }
        public int MembersCount { get; set; }
        public int ConsultantCount { get; set; }
        public int OnlineMembersCount { get; set; }
        public int OnlineConsultantsCount { get; set; }
       
    }
}
