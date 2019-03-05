using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Nop.Web.Controllers.HaragAdmin
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {

            return View("~/Themes/Pavilion/Views/HaragAdmin/Notification/AllUsersMessage.cshtml");
        }
    }
}