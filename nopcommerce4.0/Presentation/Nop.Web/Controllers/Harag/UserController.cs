using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Services.Customers;
using Nop.Services.Z_Consultant.Helpers;
using Nop.Services.Z_Harag.BlackList;
using Nop.Services.Z_Harag.Post;
using Nop.Web.Models.Consultant.User;
using Nop.Web.Models.Harag.Post;
using Nop.Web.Models.Harag.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Controllers.Harag
{
    public class UserController : BasePublicController
    {
        private readonly Core.IWorkContext _workContext;
        private readonly  ICustomerService _customerContext;
        private readonly  IBlackListService _blackListService;
        private readonly  IPostService _postService;

        public UserController(Core.IWorkContext workContext,
             IBlackListService _blackListService, IPostService _postService,ICustomerService _customerContext)
        {
            this._workContext = workContext;
            this._customerContext = _customerContext;
            this._postService = _postService;
            this._blackListService = _blackListService;
        }

        [HttpGet]
        public IActionResult CheckBlackList()
        {
            return View("~/Themes/Pavilion/Views/Harag/Blacklist/Blacklist.cshtml");
        }

        [HttpGet]
        public IActionResult CheckBlackListAjax(string phone)
        {
            var result = _blackListService.checkIfUserInBlackList(phone);

            var re = result ? "هذا الرقم موجود في القائمه السوداء" : "هذا الرقم غير موجود في القائمه السوداء";
            return Ok(re);
        }

        [HttpGet]
        public IActionResult Profile()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Redirect("Login");

            var result = _workContext.CurrentCustomer;

            var posts = _postService.GetCurrentUserPosts(result.Id).Select(p => new PostModel
            {
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                Text = p.Text,
                Id = p.Id,
                Title = p.Title,
                City = p.City.ArName,
                DateCreated = p.DateCreated,
                Photo = p.Z_Harag_Photo.Select(ppp => ppp.Url).FirstOrDefault(),
                Rate = p.Rate,
                DateUpdated = p.DateUpdated,
                IsDispayed = p.IsDispayed,
                PostOwner = p.Customer.Username
            }).ToList();

            var model = new ProfileModel
            {
                DownRating = 0,
                UpRating = 0,
                FollowerCount = 0,
                LastSeen = result.LastActivityDateUtc,
                Posts = posts,
                userId = result.Id,
                UserName = result.Username
            };

            return View("~/Themes/Pavilion/Views/Harag/Profile/MainProfile.cshtml", model);
        }

        [HttpGet]
        public IActionResult UserProfile(string username)
        {
            if (username == null)
                return NotFound();

            var result = _customerContext.GetCustomerByUsername(username); 

            if(result == null)
                return NotFound();

            var posts = _postService.GetCurrentUserPosts(result.Id).Select(p => new PostModel
            {
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                Text = p.Text,
                Id = p.Id,
                Title = p.Title,
                City = p.City.ArName,
                DateCreated = p.DateCreated,
                Photo = p.Z_Harag_Photo.Select(ppp => ppp.Url).FirstOrDefault(),
                Rate = p.Rate,
                DateUpdated = p.DateUpdated,
                IsDispayed = p.IsDispayed,
                PostOwner = p.Customer.Username
            }).ToList();

            var model = new ProfileModel
            {
                DownRating = 0,
                UpRating = 0,
                FollowerCount = 0,
                LastSeen = result.LastActivityDateUtc,
                Posts = posts,
                userId = result.Id,
                UserName = result.Username
            };

            return View("~/Themes/Pavilion/Views/Harag/Profile/MainProfile.cshtml", model);
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
