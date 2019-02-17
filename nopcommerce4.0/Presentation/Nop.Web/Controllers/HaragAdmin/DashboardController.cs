using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Nop.Web.Controllers.HaragAdmin
{
    public class DashboardController : Controller
    {
        public IActionResult Home()
        {
            return View("~/Themes/Pavilion/Views/HaragAdmin/Home/Home.cshtml");
        }
    }
}