using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.HaragAdmin.User
{
    public class UserModel : BaseNopEntityModel
    {
        public string Username { get; set; }
        public string UserRole { get; set; }
    }
}
