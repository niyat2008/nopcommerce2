using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Z_HaragAdmin.Customers;
using Microsoft.AspNetCore.Hosting;
using Nop.Core.Domain.Customers;
using Nop.Services.Z_Harag.Helpers;
using Nop.Services.Z_Harag.Notification;

namespace Nop.Web.Controllers.HaragAdmin
{
    public class NotificationController : Controller
    {
        #region Fields
        private readonly ICustomerService _customerService;
        private readonly INotificationService _notificationService;
        //private readonly IPostService _postService;
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;
        #endregion
        #region Ctor
        public NotificationController(ICustomerService customerService, INotificationService notificationService, Core.IWorkContext workContext, IHostingEnvironment env)
        {
            this._customerService = customerService;
            this._notificationService = notificationService;
            this._workContext = workContext;
            this._env = env;
        }
        #endregion
        #region Methods


        //Push Message To All Users
        public IActionResult PushMessage()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            return View("~/Themes/Pavilion/Views/HaragAdmin/Notification/AllUsersMessage.cshtml");
        }

        //Push Message To All Users Ajax
        [HttpPost]
        public IActionResult PushMessageAjax([FromBody]SiteToUserNotificationModel model)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            model.AdminId= _workContext.CurrentCustomer.Id;

            var added = _notificationService.PushSiteToAllUserNotification(model);

            return Json(new {added });
        }



        //Send Message To User
        [HttpPost]
        public IActionResult SendMessage([FromBody]SiteToUserNotificationModel model)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            model.AdminId = _workContext.CurrentCustomer.Id;

            var added = _notificationService.PushSiteToUserNotification(model);

            return Json(new {  added });
        }
        #endregion
    }
}