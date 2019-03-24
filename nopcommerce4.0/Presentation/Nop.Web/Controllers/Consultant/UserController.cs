using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Services.Customers;
using Nop.Services.Z_Consultant.Helpers;
using Nop.Web.Models.Consultant.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Controllers.Consultant
{
    public class UserController : BasePublicController
    {
        private readonly Core.IWorkContext _workContext;
        public UserController(Core.IWorkContext workContext)
        {
            this._workContext = workContext;
        }


        [HttpGet]
        public IActionResult GetUserInfo()
        {
            UserModel model = new UserModel();

            if (_workContext.CurrentCustomer.IsRegistered())
            {
                if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                    model.UserRole = RolesType.Administrators;
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                    model.UserRole = RolesType.Consultant;
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                    model.UserRole = RolesType.Registered;

                model.Id = _workContext.CurrentCustomer.Id;
                model.Username = _workContext.CurrentCustomer.GetFullName();
            }
            else
                model.UserRole = null;



            return PartialView("~/Themes/Pavilion/Views/Consultant/User/_UserInfo.cshtml", model);
        }


        [HttpGet]
        public IActionResult GetAdminLink()
        {
            UserModel model = new UserModel();

            if (_workContext.CurrentCustomer.IsRegistered())
            {
                if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) || _workContext.CurrentCustomer.IsInCustomerRole(RolesType.ConsultationAdmin, true))
                    return PartialView("~/Themes/Pavilion/Views/Consultant/User/_AdminLink.cshtml", 1);
            }
            return PartialView("~/Themes/Pavilion/Views/Consultant/User/_AdminLink.cshtml", 0);
        }


        
        [HttpGet]
        public IActionResult GetUserRoles()
        {
            if (_workContext.CurrentCustomer.IsRegistered())
            {
                if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                    ViewBag.UserRole = RolesType.Administrators;
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                    ViewBag.UserRole = RolesType.Consultant;
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                    ViewBag.UserRole = RolesType.Registered;
            }
            else
                ViewBag.UserRole = "vistor";

            return PartialView("~/Themes/Pavilion/Views/Consultant/User/_UserRolesAndThreeButtons.cshtml");
        }



    }
}
