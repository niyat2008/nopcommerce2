﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Z_HaragAdmin.Post;
using Microsoft.AspNetCore.Hosting;
using Nop.Services.Customers;
using Nop.Core.Domain.Customers;
using Nop.Services.Z_Harag.Helpers;
using Nop.Web.Models.HaragAdmin.Post;
using System.IO;
using Nop.Services.Z_HaragAdmin.Comment;
using Nop.Web.Models.HaragAdmin.Comment;
using Nop.Web.Models.HaragAdmin.Messages;
using Nop.Web.Models.HaragAdmin.Reports;

namespace Nop.Web.Controllers.HaragAdmin
{
    public class PostController : Controller
    {
        #region Fields
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;
        private readonly ICustomerService _customer;

        #endregion

        #region Ctor
        public PostController(IPostService postService, ICommentService commentService, Core.IWorkContext workContext, IHostingEnvironment env, ICustomerService customer)
        {
            this._postService = postService;
            this._commentService = commentService;
            this._workContext = workContext;
            this._env = env;
            this._customer = customer;
        }
        #endregion

        #region Actions
        //Get All Posts
        public IActionResult GetHaragPosts()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();



            return View("~/Themes/Pavilion/Views/HaragAdmin/Post/GetAllPosts.cshtml");
        }
        //Get All Posts Ajax
        [HttpPost]
        public IActionResult GetHaragPostsAjax()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            //Server Side Parameters
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            //int startRec = Request.Form.GetValues("start").First;
            //int start = Convert.ToInt32(Request.Form.GetValues("start")[0]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];
            var posts = _postService.GetAllPosts(start, length, searchValue, sortColumnName, sortDirection);


            var outputModel = new PostOutputModel
            {
                Items = posts.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated.Date,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsCommentingClosed,
                    IsAnswered = m.IsAnswered,
                    City = m.City?.ArName,

                    IsDispayed = m.IsDispayed,
                    IsFeatured = (m.IsFeatured == null? false:(bool)  m.IsFeatured), 
                    Customer = m.Customer.Username,
                    Category = m.Category.Name,


                }).ToList()
            };

            return Json(new { data = outputModel.Items });
        }

       
        //Get All Posts
        public IActionResult GetHaragDeletedPosts()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();



            return View("~/Themes/Pavilion/Views/HaragAdmin/Post/GetAllDeletedPosts.cshtml");
        }
        //Get All Posts Ajax
        [HttpPost]
        public IActionResult GetHaragDeletedPostsAjax()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            //Server Side Parameters
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            //int startRec = Request.Form.GetValues("start").First;
            //int start = Convert.ToInt32(Request.Form.GetValues("start")[0]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];
            var posts = _postService.GetAllDeletedPosts(start, length, searchValue, sortColumnName, sortDirection);


            var outputModel = new PostOutputModel
            {
                Items = posts.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated.Date,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsCommentingClosed,
                    IsAnswered = m.IsAnswered,
                    City = m.City?.ArName,

                    IsDispayed = m.IsDispayed,
                    IsFeatured = (m.IsFeatured == null ? false : (bool)m.IsFeatured),
                    Customer = m.Customer.Username,
                    Category = m.Category.Name,
                    DeletionReason=m.DeleteMessage,
                    DeletionTime=m.DeleteTime

                }).ToList()
            };

            return Json(new { data = outputModel.Items });
        }

        //Get  Post Details
        public ActionResult GetPostDetails(int postId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            if (postId == null || postId == 0)
                return NotFound();

            var postDetailsInDb = _postService.PostDetails(postId);

            List<string> files = new List<string>();

            foreach (var model in postDetailsInDb.Z_Harag_Photo)
            {

                string pathReplaced = Path.Combine(_env.WebRootPath, "/HaragApi/Uploads/Images/" + model.Url);

                files.Add(pathReplaced);
            }
            // var modelToReturn = postDetailsInDb.ToPostWithFilesModel();


            var modelToReturn = new PostWithFilesModel
            {
                Id = postDetailsInDb.Id,
                Text = postDetailsInDb.Text,
                Title = postDetailsInDb.Title,
                DateCreated = postDetailsInDb.DateCreated,
                DateUpdated = postDetailsInDb.DateUpdated,
                IsClosed = postDetailsInDb.IsCommentingClosed,
                IsAnswered = postDetailsInDb.IsAnswered,
                City = postDetailsInDb.City?.ArName,
                IsDispayed = postDetailsInDb.IsDispayed,
                IsFeatured = (postDetailsInDb.IsFeatured == null?false:(bool)postDetailsInDb.IsFeatured),


                Customer = postDetailsInDb.Customer?.Username,
                Category = postDetailsInDb.Category?.Name,

                Photos = files
            };

            return View("~/Themes/Pavilion/Views/HaragAdmin/Post/PostDetails.cshtml", modelToReturn);
        }
        //Get Post Comments
        public ActionResult GetPostComments(int postId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            if (postId == null || postId == 0)
                return NotFound();
            var commentsInDb = _commentService.GetPostComments(postId);
            var comments = new CommentOutputModel
            {
                Items = commentsInDb.Select(m => new CommentModel
                {
                    Id = m.Id,
                    PostId = m.PostId,
                    Text = m.Text,
                    CommentedBy = m.CommentedBy,
                    CommentOwner = m.Customer?.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated == null ? m.DateCreated : m.DateUpdated

                }).ToList()
            };

            return Json(new { data = comments.Items });
        }
        //Get Post Messages
        public ActionResult GetPostMessages(int postId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            if (postId == null || postId == 0)
                return NotFound();
            var messagesInDb = _postService.GetPostMessage(postId);
            var messages = new MessageOutputModel
            {
                Items = messagesInDb.Select(m => new MessageModel
                {
                    Id = m.Id,
                    Message = m.Message,
                    Customer = m.Customer?.Username,
                    Post = m.Z_Harag_Post?.Title,
                    DateCreated = m.CreatedTime
                }).ToList()
            };

            return Json(new { data = messages.Items });
        }
        //Get Post Reports
        public ActionResult GetPostReports(int postId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            if (postId == null || postId == 0)
                return NotFound();
            var messagesInDb = _postService.GetPostReports(postId);
            var messages = new ReportOutputModel
            {
                Items = messagesInDb.Select(m => new ReportModel
                {
                    Id = m.Id,
                    ReportDescription = m.ReportDescription,
                    CustomerName = m.Customer?.Username,
                    PostTitle = m.Z_Harag_Post?.Title,
                    ReportTitle = m.ReportTitle
                }).ToList()
            };

            return Json(new { data = messages.Items });
        }
        //Get Comment Reports Ajax

        public ActionResult GetCommentReports(int commentId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            if (commentId == 0)
                return NotFound();

            return View("~/Themes/Pavilion/Views/HaragAdmin/Post/CommentReports.cshtml", commentId);

        }
        //Get Comment Reports Ajax

        public ActionResult GetCommentReportsAjax(int commentId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            if (commentId == null || commentId == 0)
                return NotFound();
            var reportInDb = _commentService.GetCommentReports(commentId);
            var messages = new ReportOutputModel
            {
                Items = reportInDb.Select(m => new ReportModel
                {
                    Id = m.Id,
                    Category = m.ReportCategory,
                    ReportDescription = m.ReportDescription,
                    CustomerName = m.Z_Harag_Customer?.Username,
                    Comment = m.Z_Harag_Comment?.Text,
                    CommentId=m.CommentId,
                    ReportTitle = m.ReportTitle
                }).ToList()
            };

            return Json(new { data = messages.Items, CommentId = commentId });
        }

        //Get Post By Id
        public ActionResult GetPostById(int postId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            if (postId == 0)
                return NotFound();

            var postInDb = _postService.GetPostById(postId);
            var outputModel = new PostOutputModel
            {
                Items = postInDb.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsCommentingClosed,
                    IsAnswered = m.IsAnswered,
                    City = m.City?.ArName,

                    IsDispayed = m.IsDispayed,
                    IsFeatured = (m.IsFeatured == null ? false : (bool)m.IsFeatured),


                    Customer = m.Customer.Username,
                    Category = m.Category.Name,


                }).ToList()
            };

            return Json(new { data = outputModel.Items });

           
        }

        //Get Posts By Category
        public IActionResult GetPostsByCategory(int categoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            if (categoryId == 0)
                return NotFound();

            return View("~/Themes/Pavilion/Views/HaragAdmin/Category/GetCategoryPosts.cshtml",categoryId);
        }


        //Get Posts By Category Ajax
        [HttpPost]
        public IActionResult GetPostsByCategoryAjax(int categoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            //Server Side Parameters
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            //int startRec = Request.Form.GetValues("start").First;
            //int start = Convert.ToInt32(Request.Form.GetValues("start")[0]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];
            var posts = _postService.GetPostsByCategory(categoryId,start, length, searchValue, sortColumnName, sortDirection);


            var outputModel = new PostOutputModel
            {
                Items = posts.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsCommentingClosed,
                    IsAnswered = m.IsAnswered,
                    City = m.City?.ArName,

                    IsDispayed = m.IsDispayed,
                    IsFeatured = (m.IsFeatured == null ? false : (bool)m.IsFeatured),


                    Customer = m.Customer.Username,
                    Category = m.Category.Name,


                }).ToList()
            };

            return Json(new { data = outputModel.Items });
        }

        //Get Posts By City
      
        public IActionResult GetPostsByCity(int cityId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            if (cityId == 0)
                return NotFound();

            return View("~/Themes/Pavilion/Views/HaragAdmin/Cities/GetCityPosts.cshtml", cityId);
        }

        //Get Posts By City
        [HttpPost]
        public IActionResult GetPostsByCityAjax(int cityId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            //Server Side Parameters
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            //int startRec = Request.Form.GetValues("start").First;
            //int start = Convert.ToInt32(Request.Form.GetValues("start")[0]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];
            var posts = _postService.GetPostsByCity(cityId, start, length, searchValue, sortColumnName, sortDirection);


            var outputModel = new PostOutputModel
            {
                Items = posts.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsCommentingClosed,
                    IsAnswered = m.IsAnswered,
                    City = m.City?.ArName,

                    IsDispayed = m.IsDispayed,
                    IsFeatured = (m.IsFeatured == null ? false : (bool)m.IsFeatured), 
                    Customer = m.Customer.Username,
                    Category = m.Category.Name,


                }).ToList()
            };

            return Json(new { data = outputModel.Items });
        }

        //Get Comment By Id
        public IActionResult GetComment(int commentId=0)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            if (commentId == 0)
                return NotFound();

            var commentInDb = _commentService.GetComment(commentId);

            var model = new CommentModel
            {
                Id = commentInDb.Id,
                PostId = commentInDb.PostId,
                Text = commentInDb.Text,
                CommentedBy = commentInDb.CommentedBy,
                CommentOwner = commentInDb.Customer?.Username,
                DateCreated = commentInDb.DateCreated.Date,
                DateUpdated = commentInDb.DateUpdated == null ? commentInDb.DateCreated.Date : commentInDb.DateUpdated.Date
            };

            return Json(new { data = model });
        }
        ////Get Post Messages
        //public IActionResult GetPostMessages(int postId)
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
        //        return Forbid();



        //    return View("~/Themes/Pavilion/Views/HaragAdmin/Post/GetAllPosts.cshtml",postId);
        //}
        ////Get Post Messages Ajax
        //public IActionResult GetPostMessagesAjax(int postId)
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();


        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
        //        return Forbid();

        //    var messagesInDb = _postService.GetPostMessage(postId);
        //    var messages = new MessageOutputModel
        //    {
        //        Items = messagesInDb.Select(m => new MessageModel
        //        {
        //            Id = m.Id,
        //            Message = m.Message,
        //            Customer = m.Customer?.Username,
        //            Post = m.Z_Harag_Post?.Title,
        //            DateCreated = m.CreatedTime
        //        }).ToList()
        //    };

        //    return Json(new { data = messages.Items });
        //}
        #endregion
    }
}