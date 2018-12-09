using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Consultant.Api.Models.User
{
    public class UserModel : BaseNopEntityModel
    {
        public string Username { get; set; }
        public string UserRole { get; set; }
    }
}
