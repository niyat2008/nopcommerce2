using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Nop.Core.Domain.Customers;
using Nop.Services.Z_Harag.Helpers;

namespace Nop.Web.Controllers.HaragAdmin
{
    public class DashboardController : Controller
    {


        #region Fields
      
       
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;
        #endregion
        #region Ctor
        public DashboardController( Core.IWorkContext workContext, IHostingEnvironment env)
        {
           

            this._workContext = workContext;
            this._env = env;
        }
        #endregion
        #region Actions

        public IActionResult Home()
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            return View("~/Themes/Pavilion/Views/HaragAdmin/Home/Home.cshtml");
        }

        #endregion
    }
}