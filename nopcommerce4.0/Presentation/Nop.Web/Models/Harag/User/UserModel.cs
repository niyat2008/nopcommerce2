using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Harag.User
{
    public class UserModel : BaseNopEntityModel
    {
        public string Username { get; set; }
        public string Role { get; set; } 
        public string Phone { get; set; }

        /// <summary>
        /// Following CreationTime
        /// </summary>
        public DateTime CreationTime { get; set; }
    }

    public class UserOutputModel : BaseNopEntityModel
    {
        public List<UserModel> Users { get; set; }
        public List<Post.PostModel> Posts { get; set; }
    }

}
