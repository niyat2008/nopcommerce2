using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Services.Customers; 
using Nop.Services.Z_Harag.BlackList;
using Nop.Services.Z_Harag.Rate;
using Nop.Services.Z_Harag.Post; 
using Nop.Services.Z_Harag.Follow; 
using Nop.Web.Models.Harag.Post;
using Nop.Web.Models.Harag.Profile;
using Nop.Web.Models.Harag.Rate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Z_Harag.Category;
using Nop.Web.Models.Harag.User;

namespace Nop.Web.Controllers.Harag
{
    public class FollowController : BasePublicController
    {
        private readonly Core.IWorkContext _workContext;
        private readonly ICustomerService _customerContext;
        private readonly IRateSrevice _rateRepository;
        private readonly IFollowService _followRepository;
        private readonly ICategoryService _categoryRepository;
        private readonly IBlackListService _blackListService;
        private readonly IPostService _postService;

        public FollowController(Core.IWorkContext workContext,
            IRateSrevice _rateRepository, ICategoryService _categoryRepository, IFollowService _followRepository,
            IBlackListService _blackListService, IPostService _postService,ICustomerService _customerContext)
        {
            this._workContext = workContext;
            this._categoryRepository = _categoryRepository;
            this._rateRepository = _rateRepository;
            this._customerContext = _customerContext;
            this._postService = _postService;
            this._followRepository = _followRepository;
            this._blackListService = _blackListService;
        } 

        [HttpGet]
        public IActionResult AddPostToFollow(int postId)
        {
            if (_workContext.CurrentCustomer.IsRegistered())
            {
                var post = _postService.GetPost(postId, "");

                if (post == null)
                    return Ok(new { status = false });

                var postFollow = new Z_Harag_Follow
                {
                    FollowType = (int) FollowType.Post, 
                    PostId = post.Id,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                    UserId = _workContext.CurrentCustomer.Id
                };
                // check if user exists
                var exists = _followRepository.IsPostFollowed(post.Id, _workContext.CurrentCustomer.Id);

                if (exists)
                {
                    _followRepository.RemovePostFromFollow(post.Id, _workContext.CurrentCustomer.Id);
                    return Ok(new { status = true, added = false });

                }
                var follow = _followRepository.AddPostToFollow(postFollow);
                return Ok(new { status = true, added = true });  
            }
            return Ok(new { status = false }); 
        }

        [HttpGet]
        public IActionResult RemovePostFromFollow(int postId)
        {
            if (_workContext.CurrentCustomer.IsRegistered())
            {
                var post = _postService.GetPost(postId, "");
                 
                if(post == null)
                    return Ok(new { status = false });

                var follow = _followRepository.RemovePostFromFollow(post.Id, _workContext.CurrentCustomer.Id);

                return Ok(new { status = true });
            }
            return Ok(new { status = false });
        }

        [HttpGet]
        public IActionResult AddUserToFollow(int userId)
        {
            if (_workContext.CurrentCustomer.IsRegistered())
            {
                var user = _customerContext.GetCustomerById(userId);

                if (user == null)
                    return Ok(new { status = false });

                var postFollow = new Z_Harag_Follow
                {
                    FollowType = (int) FollowType.User,
                    FollowedId = user.Id,
                    CreatedTime = DateTime.Now,
                    UpdatedTime = DateTime.Now,
                    UserId = _workContext.CurrentCustomer.Id
                };

                // check if user exists
                var exists = _followRepository.IsUserFollowed(user.Id, _workContext.CurrentCustomer.Id);

                if (exists)
                {
                    _followRepository.RemoveUserFromFollow(user.Id, _workContext.CurrentCustomer.Id);
                    return Ok(new { status = true, added = false });

                }
                var follow = _followRepository.AddUserToFollow(postFollow);
                return Ok(new { status = true, added = true });
                 
            }
            return Ok(new { status = false  });
        }

        [HttpGet]
        public IActionResult RemoveUserFromFollow(int userId)
        {
            if (_workContext.CurrentCustomer.IsRegistered())
            {
                var user = _customerContext.GetCustomerById(userId);
                if (user == null)
                    return Ok(new { status = false });

                var follow = _followRepository.RemoveUserFromFollow(user.Id, _workContext.CurrentCustomer.Id);


                return Ok(new { status = true });
            }

            return Ok(new { status = false });
        }

        [HttpGet]
        public IActionResult PaymentBanks()
        {
            return View("~/Themes/Pavilion/Views/Harag/Payment/bankpayment.cshtml");
        }
        [HttpGet]
        public IActionResult PaymentSadad()
        { 
            return View("~/Themes/Pavilion/Views/Harag/Payment/sadadpayment.cshtml"); 
        }


        [HttpGet]
        public IActionResult GetFollowedUsersPosts()
        {
            if (_workContext.CurrentCustomer.IsRegistered())
            {
                var posts = _followRepository.GetFollowedPosts(_workContext.CurrentCustomer.Id);
                var users = _followRepository.GetFollowedUsers(_workContext.CurrentCustomer.Id);

                var modelList = new List<PostModel>();
                foreach (var follow in posts)
                {
                    var p = _postService.GetPost((int) follow.PostId, "");

                    modelList.Add(new PostModel
                    {
                        CategoryId = p.CategoryId,
                        CategoryName = p.Category.Name,
                        Text = p.Text,
                        Id = p.Id,
                        Title = p.Title,
                        City = p.City.ArName,
                        DateCreated = p.DateCreated,
                        Photo = p.Z_Harag_Photo.Select(ppp => ppp.Url).FirstOrDefault(), 
                        DateUpdated = p.DateUpdated,
                        IsDispayed = p.IsDispayed,
                        PostOwner = p.Customer.Username,
                        CreationTime = (DateTime)follow.CreatedTime
                    });
                }

                var modelUsersList = new List<UserModel>();

                foreach (var follow in users)
                {
                    var u = _customerContext.GetCustomerById((int)follow.FollowedId);

                    modelUsersList.Add(new UserModel
                    {
                         Phone = u.Mobile,
                         Id = u.Id,
                         Username = u.Username,
                         CreationTime = (DateTime)follow.CreatedTime
                    });
                }

                var generalModel = new UserOutputModel
                {
                    Posts = modelList,
                    Users = modelUsersList
                };

                return View("~/Themes/Pavilion/Views/Harag/Follow/Follow.cshtml", generalModel);
            }
            else
            {
                return Redirect("Login");
            }
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
