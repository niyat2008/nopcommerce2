using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Services.Customers;
using Nop.Services.Z_Harag.Helpers;
using Nop.Services.Z_Harag.BlackList;
using Nop.Services.Z_Harag.Rate;
using Nop.Services.Z_Harag.Post; 
using Nop.Web.Models.Harag.Post;
using Nop.Web.Models.Harag.Profile;
using Nop.Web.Models.Harag.Rate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core.Domain.Z_Harag;
using Nop.Web.Models.Harag.User;
using Nop.Services.Z_Harag.Follow;
using Nop.Services.Z_Harag.Payment;
using Nop.Services.Z_HaragAdmin.Setting;
using Nop.Services.Z_Harag.Notification;

namespace Nop.Web.Controllers.Harag
{
    public class UserController : BasePublicController
    {
        private SettingsModel Settings;
        private readonly Core.IWorkContext _workContext;
        private readonly  ICustomerService _customerContext;
        private readonly IRateSrevice _rateRepository;
        private readonly INotificationService _notificationService;
        private readonly IPaymentService _paymentRepository;
        private readonly ISettingService _settingRepository;

        private readonly IFollowService _followRepository; 
        private readonly  IBlackListService _blackListService;
        private readonly  IPostService _postService;

        public PagingParams PagingParams { get; set; }

        public UserController(Core.IWorkContext workContext,IRateSrevice _rateRepository, IFollowService _followRepository, IPaymentService _paymentRepository,
             IBlackListService _blackListService, IPostService _postService,ICustomerService _customerContext, ISettingService _settingRepository, INotificationService _notificationService)
        {
            this._notificationService = _notificationService;
            this._paymentRepository = _paymentRepository;
            this._settingRepository = _settingRepository;
            this._followRepository = _followRepository;
            this._workContext = workContext;
            this._rateRepository = _rateRepository;
            this._customerContext = _customerContext;
            this._postService = _postService;
            this._blackListService = _blackListService;

            Settings = _settingRepository.GetSettings();
            PagingParams = new PagingParams();
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
            ViewBag.SameUser = true; 
            ViewBag.CanRateOtherUsers = false;

            var posts = _postService.GetCurrentUserPosts(result.Id, PagingParams).Select(p => new PostModel
            {
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                Text = p.Text,
                Id = p.Id,
                Title = p.Title,
                City = p.City.ArName,
                DateCreated = p.DateCreated,
                Photo = p.Z_Harag_Photo.Select(ppp => ppp.Url).FirstOrDefault(),
                //Rate = p.Rate,
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
        public IActionResult GetHaragAdminLink()
        {
            UserModel model = new UserModel();

            if (_workContext.CurrentCustomer.IsRegistered())
            {
                if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                    return PartialView("~/Themes/Pavilion/Views/Harag/User/_AdminLink.cshtml", 1);
            }
            return PartialView("~/Themes/Pavilion/Views/Harag/User/_AdminLink.cshtml", 0);
        }
        /// <summary>
        /// Helper get the abilitty to add rate 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private bool CanRateOtherUsers()
        { 
            var payments = _paymentRepository.GetUserPayments(_workContext.CurrentCustomer.Id);
            if(payments.Count >= this.Settings.RateCommissionNumber && payments.Sum(m => m.SiteAmount) > Settings.RateCommissionSum)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [HttpGet]
        public IActionResult RateUserView(string username)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Redirect("/Login");
            var user = _customerContext.GetCustomerByUsername(username);

 
            if (user == null)
            {
                return NotFound();
            }

            var model = new RateModel
            {
                Username = user.Username,
                UserId = user.Id
            };

           

            if (user.Username == _workContext.CurrentCustomer.Username)
            { 
                var note = "لا يمكنك تقييم نفسك";
                return View("~/Themes/Pavilion/Views/Harag/Rate/RateNote.cshtml", note); 
            }
            else if (!CanRateOtherUsers())
            {
                var note = "لابد من الوصول الي شرط التقييم لامكانيه تقييم عضو اخر";
                return View("~/Themes/Pavilion/Views/Harag/Rate/RateNote.cshtml", note);
            }

            ViewBag.Added = false;

            return View("~/Themes/Pavilion/Views/Harag/Rate/RateUser.cshtml", model); 
        }

        [HttpPost]
        public IActionResult RateUserAjax(RateModel model)
        { 

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Redirect("/Login");

            if(!ModelState.IsValid)
                return View("~/Themes/Pavilion/Views/Harag/Rate/RateUser.cshtml", model);
            var user = _customerContext.GetCustomerByUsername(model.Username);

            if (user == null)
                return NotFound();

            var rate = new Z_Harag_Rate {
                AdviceDeal = model.AdviceDeal,
                IsBuyDone = model.IsBuyDone,
                RateComment = model.RateComment,
                CustomerId = _workContext.CurrentCustomer.Id,
                UserId = user.Id,
                BuyTime = model.WhenBuyDone
            };

            var posts = _rateRepository.AddUserRate(rate);
            var notification = _notificationService.PushRateNotification(_workContext.CurrentCustomer.Id,  model.AdviceDeal);

            ViewBag.Added = true;

            return View("~/Themes/Pavilion/Views/Harag/Rate/RateUser.cshtml", model);
        }


        [HttpGet]
        public IActionResult GetUserRates(string userId)
        {
             
            var userRates = new UserRateOutList();

            var user = _customerContext.GetCustomerByUsername(userId);

            if (user == null)
                return NotFound();

            var rates = _rateRepository.GetUserRates(user.Id);

            userRates.Rates = rates.Select(m => new RateModel
            {
                AdviceDeal = m.AdviceDeal,
                
                IsBuyDone = m.IsBuyDone,
                RateComment = m.RateComment,
                Username = m.Customer.Username
            }).ToList();

            userRates.ConfirmedUserDownRateCount = 0;
            userRates.ConfirmedUserUpRateCount = 0;
            userRates.UpRateCount = userRates.Rates.Where(m => m.AdviceDeal == true).ToList().Count; ;
            userRates.DownRateCount = userRates.Rates.Where(m => m.AdviceDeal == false).ToList().Count; 
            ViewBag.Added = true;

            return View("~/Themes/Pavilion/Views/Harag/Rate/AllUserRates.cshtml", userRates);
        }

        [HttpGet]
        public IActionResult UserProfile(string username)
        {
            if (username == null)
                return NotFound();

            var result = _customerContext.GetCustomerByUsername(username);

            if(result == null)
                return NotFound();
             
            ViewBag.SameUser = (result.Username == _workContext.CurrentCustomer.Username); 
            ViewBag.CanRateOtherUsers = this.CanRateOtherUsers();   

            var posts = _postService.GetCurrentUserPosts(result.Id, PagingParams)
                .Select(p => new PostModel
            {
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                Text = p.Text,
                Id = p.Id,
                Title = p.Title,
                City = p.City.ArName,
                DateCreated = p.DateCreated,
                Photo = p.Z_Harag_Photo.Select(ppp => ppp.Url).FirstOrDefault(),
                //Rate = p.Rate,
                DateUpdated = p.DateUpdated,
                IsDispayed = p.IsDispayed,
                PostOwner = p.Customer.Username
            }).ToList();

            var model = new ProfileModel
            {
                DownRating = _rateRepository.GetUserDownRates(result.Id).Count,
                UpRating = _rateRepository.GetUserUpRates(result.Id).Count,
                FollowerCount = _followRepository.GetFollowedUsers(result.Id).Count,
                LastSeen = result.LastActivityDateUtc,
                Posts = posts,
                userId = result.Id,
                UserName = result.Username
            };

            return View("~/Themes/Pavilion/Views/Harag/Profile/MainProfile.cshtml", model);
        }


        [HttpGet]
        public IActionResult GetUserSummaryInfo(string userId)
        {
            var id = 0;
            var user = _customerContext.GetCustomerByUsername(userId);

            if (user == null)
            { 
                    user = _customerContext.GetCustomerByMobile(userId);

                    if (user == null)
                    {
                        return Ok(new { Status = false , d = userId});
                    } 

            }

            UserModel model = new UserModel();
             
            model.Id = user.Id;
            model.Username = user.Username;
            model.Phone = user.Mobile;

            return Ok(new { Status = true, Data = model }); 
        }

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
