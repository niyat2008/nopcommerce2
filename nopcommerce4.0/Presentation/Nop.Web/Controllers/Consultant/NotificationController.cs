using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Z_Consultant.Notification;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Web.Models.Consultant.Notification;

namespace Nop.Web.Controllers.Consultant
{
    public class NotificationController : Controller
    {
        #region Fields
        private readonly INotificationService _notificationService;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor
        public NotificationController(INotificationService notificationService, IWorkContext workContext)
        {
            this._notificationService = notificationService;
            this._workContext = workContext;
        }
        #endregion

        #region Actions
        //Get Notifications
        public IActionResult GetNotifications()
        {
            if (!_workContext.CurrentCustomer.IsRegistered()) 
                return Redirect("/Login");

            int customerId = _workContext.CurrentCustomer.Id;

            var notifications = _notificationService.GetNotifications(customerId);

            var model = notifications.Select(m => new GetNotificationModel {
                Id = m.Id,
                PostId = m.PostId,
                Owner = m.Owner.Username,
                User = m.User.Username,
                Time = m.Time,
                Type=m.Type
                

            });

            _notificationService.UpdateNotification(customerId);
            return View("~/Themes/Pavilion/Views/Consultant/Notification/GetNotifications.cshtml",model);
        }


        //Get Un Read Notifications
        public IActionResult GetUnReadNotifications()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            int customerId = _workContext.CurrentCustomer.Id;

            int query = _notificationService.GetUnReadNotifications(customerId);

            return Json(new { query });
        }


        [HttpGet]
        public IActionResult DeleteConsultantNotifications()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Redirect("/Login");

             _notificationService.DeleteNotifications(_workContext.CurrentCustomer.Id);
            return Redirect("/Consultant/Notifications");
        }
      
        #endregion


    }
}