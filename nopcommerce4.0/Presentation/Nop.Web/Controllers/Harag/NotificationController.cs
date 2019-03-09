using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Services.Customers;
using Nop.Services.Z_Consultant.Helpers;
using Nop.Services.Z_Harag.BlackList;
using Nop.Services.Z_Harag.Rate;
using Nop.Services.Z_Harag.Post;
using Nop.Web.Models.Consultant.User;
using Nop.Web.Models.Harag.Post;
using Nop.Web.Models.Harag.Profile;
using Nop.Web.Models.Harag.Rate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Z_Harag.Notification;
using Nop.Web.Models.Harag.Notification;

namespace Nop.Web.Controllers.Harag
{
    public class NotificationController : BasePublicController
    {
        private readonly Core.IWorkContext _workContext;
        private readonly  ICustomerService _customerContext;
        private readonly  IRateSrevice _rateRepository
            ;
        private readonly  IBlackListService _blackListService;
        private readonly  INotificationService _notificationsService;
        private readonly  IPostService _postService;

        public NotificationController(Core.IWorkContext workContext,IRateSrevice _rateRepository,
            INotificationService _notificationsService,
             IBlackListService _blackListService, IPostService _postService,ICustomerService _customerContext)
        {
            this._workContext = workContext;
            this._notificationsService = _notificationsService;
            this._rateRepository = _rateRepository;
            this._customerContext = _customerContext;
            this._postService = _postService;
            this._blackListService = _blackListService;
        }
          
        [HttpGet]
        public IActionResult Notifications()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Redirect("/Login");

            var currentUser = _workContext.CurrentCustomer;

            var Notifications = _notificationsService.GetUserNotifications(currentUser.Id);
            _notificationsService.SetUserNotificationsSeen(currentUser.Id);

             
            var notificationsModels = Notifications.Select(m => new NotificationModel
            {
                CustomerId = (int)m.CustomerId,
                CustomerName = m.Customer == null ? "" : m?.Customer.GetFullName(),
                Type = (NotificationType)m.NotificationType,
                Note = m.NotificationContent,
                OwnerId = (int)m.OwnerId,
                OwnerName =  m.Owner == null ?"": m?.Owner.GetFullName(),
                PostId =  (m.PostId== null ?0:(int)m.PostId),
                PostTitle = (m.Z_Harag_Post == null ? "" : m?.Z_Harag_Post.Title),
                Time = (DateTime)m.NotificationTime,
                Username =( m.Customer== null?"":m?.Customer?.Username)
            }).ToList();

            var outputModel = new NotificationListModel { Notifications = notificationsModels };

            return View("~/Themes/Pavilion/Views/Harag/Notification/Notifications.cshtml", outputModel);
        }

        [HttpGet]
        public IActionResult NotificationsCount()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Ok(0);

            var count = _notificationsService.GetUnSeenNotificationCount(_workContext.CurrentCustomer.Id);
            return Ok(count);
        }

   
         
        //[HttpGet]
        //public IActionResult GetUserInfo()
        //{
        //    UserModel model = new UserModel();

        //    if (_workContext.CurrentCustomer.IsRegistered())
        //    {
        //        if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //            model.UserRole = RolesType.Administrators;
        //        else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
        //            model.UserRole = RolesType.Consultant;
        //        else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
        //            model.UserRole = RolesType.Registered;

        //        model.Id = _workContext.CurrentCustomer.Id;
        //        model.Username = _workContext.CurrentCustomer.Username;
        //    }
        //    else
        //        model.UserRole = null;



        //    return PartialView("~/Themes/Pavilion/Views/Consultant/User/_UserInfo.cshtml", model);
        //}


        //[HttpGet]
        //public IActionResult GetAdminLink()
        //{
        //    UserModel model = new UserModel();

        //    if (_workContext.CurrentCustomer.IsRegistered())
        //    {
        //        if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //            return PartialView("~/Themes/Pavilion/Views/Consultant/User/_AdminLink.cshtml", 1);
        //    }
        //    return PartialView("~/Themes/Pavilion/Views/Consultant/User/_AdminLink.cshtml", 0);
        //}



        //[HttpGet]
        //public IActionResult GetUserRoles()
        //{
        //    if (_workContext.CurrentCustomer.IsRegistered())
        //    {
        //        if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //            ViewBag.UserRole = RolesType.Administrators;
        //        else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
        //            ViewBag.UserRole = RolesType.Consultant;
        //        else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
        //            ViewBag.UserRole = RolesType.Registered;
        //    }
        //    else
        //        ViewBag.UserRole = "vistor";

        //    return PartialView("~/Themes/Pavilion/Views/Consultant/User/_UserRolesAndThreeButtons.cshtml");
        //}



    }
}
