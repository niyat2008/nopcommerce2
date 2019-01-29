using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Controllers.ConsultantِAdmin
{
    //BasePublicController
    public class DashboardConsultantController : BasePublicController
    {
        public DashboardConsultantController()
        {
               
        }


        public virtual IActionResult Home()
        {
            

            return View("~/Themes/Pavilion/Views/ConsultantAdmin/DashboardConsultant/Home.cshtml");
        }


    }
}
