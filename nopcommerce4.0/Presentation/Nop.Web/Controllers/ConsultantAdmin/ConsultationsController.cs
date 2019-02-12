using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Z_ConsultantAdmin;
using Nop.Core.Domain.Customers;
using Nop.Services.Z_Consultant.Helpers;
using Nop.Web.Models.ConsultantAdmin.Post;
using System.Linq;
using System.Web;
using Nop.Web.Extensions.ConsultantAdmin;
using Nop.Web.Models.ConsultantAdmin.Comment;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Globalization;
using System.Text;
using Nop.Services.Customers;
using Nop.Services.Z_ConsultantAdmin.Categories;
using Nop.Services.Z_ConsultantAdmin.Post;

namespace Nop.Web.Controllers.Consultant_Admin
{
    public class ConsultationsController : BasePublicController
    {
        #region Fields
        private readonly IPostService _postService;
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;
        private readonly ICustomerService _customer;
       
        #endregion


        #region Ctor
        public ConsultationsController(IPostService postService, Core.IWorkContext workContext,IHostingEnvironment env, ICustomerService customer)
        {
            this._postService = postService;
            this._workContext = workContext;
            this._env = env;
            this._customer = customer;
        }
        #endregion


        #region Methods

        public ActionResult GetAllCustomers()
        {
            var customers = _customer.GetAllCustomers();
            return Json(new { data = customers });
        }



       
        



        //Closed Displayed
        [HttpGet]
        public ActionResult GetClosedDisplayedPosts(string filterType)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            var filteringType = new PostModel
            {
                Filter = filterType
            };
            ViewBag.url = "/Consultations/Admin/Posts/ClosedDisplayed";
            return View("~/Themes/Pavilion/Views/ConsultantAdmin/Posts/GetClosedDisplayedPosts.cshtml", filteringType);
        }
        //Get  Post By Id
        public ActionResult GetPostById(int postId, string type)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();
            if (postId == 0 || postId == null)
                return NotFound();

            var postInDb = _postService.GetPostById(postId, type);
            var post = new PostOutputModel
            {
                Posts = postInDb.Select(m => new PostModel
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsClosed,
                    IsAnswered = m.IsAnswered,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    SubCategoryId = m.SubCategoryId,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList()
                   
                
            };
            return Json(new {data= post.Posts});

        }
        //Closed Displayed In Json
        [HttpPost]
        public ActionResult GetAllClosedDisplayedPostsInJson()
      {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            //Server Side Parameters
            var start =Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            //int startRec = Request.Form.GetValues("start").First;
            //int start = Convert.ToInt32(Request.Form.GetValues("start")[0]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];

            
            var posts = _postService.GetClosedDisplayedPosts(start, length, searchValue, sortColumnName, sortDirection);


            var outputModel = new PostOutputModel
            {
                Posts = posts.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsClosed,
                    IsAnswered = m.IsAnswered,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    SubCategoryId = m.SubCategoryId,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name

                }).ToList()
            };
            
            return Json(new { data=outputModel.Posts });
           
        }
        //Closed Displayed With Category Id In Json
        [HttpGet]
        public ActionResult GetAllClosedDisplayedPostsWithCategoryIdInJson(int categoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();
            if (categoryId == null || categoryId == 0)
                return NotFound();

            var posts = _postService.GetClosedDisplayedPostsWithCategoryId(categoryId);
            if (posts == null)
                return NotFound();

            var outputModel = new PostOutputModel
            {
                Posts = posts.Select(m => new PostModel()
                {
                    Id=m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsClosed,
                    IsAnswered = m.IsAnswered,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    SubCategoryId = m.SubCategoryId,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name

                }).ToList()
            };

            return Json(new { data = outputModel.Posts });

        }
        //Closed Displayed With SubCategory Id In Json
        [HttpGet]
        public ActionResult GetAllClosedDisplayedPostsWithSubCategoryIdInJson(int subCategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();
            if (subCategoryId == null || subCategoryId == 0)
                return NotFound();

            var posts = _postService.GetClosedDisplayedPostsWithSubCategoryId(subCategoryId);

            if (posts == null)
                return NotFound();

            var outputModel = new PostOutputModel
            {
                Posts = posts.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsClosed,
                    IsAnswered = m.IsAnswered,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    SubCategoryId = m.SubCategoryId,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name

                }).ToList()
            };

            return Json(new { data = outputModel.Posts });

        }
       
        //Remove  Post
        [HttpDelete]
        public ActionResult RemovePost(int postId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            if (postId == null || postId == 0)
                return Json(new { result = false });

          _postService.RemovePost(postId);
            var photos = _postService.GetPhotos(postId);
            foreach(var photo in photos)
            {
                var photoPath = Path.Combine(_env.WebRootPath, "/ConsultantApi/Uploads/Images/" + photo.Url);
                if(System.IO.File.Exists(photoPath))
                   System.IO.File.Delete(photoPath);
            }
            return Json(new { result=true});
        }
        //Get  Post Details
        public ActionResult GetPostDetails(int postId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            if (postId == null || postId == 0)
                return NotFound();

            var postDetailsInDb = _postService.GetPostDetails(postId);

            List<string> files = new List<string>();

            foreach (var model in postDetailsInDb.Photos)
            {

                string pathReplaced = Path.Combine(_env.WebRootPath, "/ConsultantApi/Uploads/Images/" + model.Url);

                files.Add(pathReplaced);
            }
           // var modelToReturn = postDetailsInDb.ToPostWithFilesModel();


            var modelToReturn = new Models.ConsultantAdmin.Post.PostWithFilesModel
            {
                    Id = postDetailsInDb.Id,
                    Text = postDetailsInDb.Text,
                    Title = postDetailsInDb.Title,
                   DateCreated = postDetailsInDb.DateCreated,
                   DateUpdated = postDetailsInDb.DateUpdated,
                   IsClosed = postDetailsInDb.IsClosed,
                   IsAnswered = postDetailsInDb.IsAnswered,
                   Rate = postDetailsInDb.Rate,
                    IsDispayed = postDetailsInDb.IsDispayed,
                    IsReserved = postDetailsInDb.IsReserved,
                   IsSetToSubCategory = postDetailsInDb.IsSetToSubCategory,
                    SubCategoryId = postDetailsInDb.SubCategoryId,
                    CategoryId = postDetailsInDb.CategoryId,
                    PostOwner = postDetailsInDb.Customer.Username,
                    CategoryName = postDetailsInDb.Category.Name,
                    SubCategoryName = postDetailsInDb.SubCategory?.Name,
                    Files = files
        };
           
            return View("~/Themes/Pavilion/Views/ConsultantAdmin/Posts/GetClosedDisplayedPostDetails.cshtml", modelToReturn);
        }
        //Get Post Comments
        public ActionResult GetPostComments(int postId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            if (postId == null || postId == 0)
                return NotFound();
            var commentsInDb = _postService.GetPostComments(postId);
            var comments = new CommentOutputModel
            {
                Comments=commentsInDb.Select(m=>new CommentModel
                {
                    PostId=m.PostId,
                    Text=m.Text,
                    CommentedBy=m.CommentedBy,
                    CommentOwner=m.Customer?.Username,
                    DateCreated=m.DateCreated,
                    DateUpdated=m.DateUpdated==null? m.DateCreated: m.DateUpdated

                }).ToList()
            };

            return Json(new { data = comments.Comments });
        }
        //Get Posts By Date
        public ActionResult GetPostsByDate(DateTime firstDate,DateTime secondDate,string type)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            if (firstDate == null || secondDate == null)
                return NotFound();

            //----------------------
            //var f = firstDate.ToString("dd/MM/yyyy");
            //CultureInfo arSA = new CultureInfo("ar-SA");
            //arSA.DateTimeFormat.Calendar = new HijriCalendar();
            //var dateValue = DateTime.ParseExact(f, "dd/MM/yyyy", arSA);
            //StringBuilder builder = new StringBuilder();
            //builder.Append(dateValue.Day);
            //builder.Append("/");
            //builder.Append(dateValue.Month);
            //builder.Append("/");
            //builder.Append(dateValue.Year);
           

            

            //----------------------

            var postsInDb = _postService.GetPostsByDate(firstDate, secondDate,type);
            var posts = new PostOutputModel
            {
                Posts=postsInDb.Select(m=>new PostModel
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsClosed,
                    IsAnswered = m.IsAnswered,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    SubCategoryId = m.SubCategoryId,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList()
            };
            return Json(new { data = posts.Posts });
        }
        //Update Post 
       
        //public ActionResult UpdatePost(PostModelForPost post)
        //{
        //    if (!_workContext.CurrentCustomer.IsRegistered())
        //        return Unauthorized();

        //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //        return Forbid();

        //    return View("", post);
        //}
        //Update Post In Ajax
        [HttpPost]
        public ActionResult SetPostToCommon(int id)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            

             _postService.SetPostToCommon(id);

            return Json(new { result = true });
        }






        public ActionResult Test()
        {
            return View("~/Themes/Pavilion/Views/ConsultantAdmin/Posts/Test.cshtml");
        }



        //Closed Not Displayed
        public ActionResult GetClosedNotDisplayedPosts(string filterType)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();
            var filteringType = new PostModel
            {
                Filter = filterType
            };

            ViewBag.url = "/Consultations/Admin/Posts/ClosedNotDisplayed";

            return View("~/Themes/Pavilion/Views/ConsultantAdmin/Posts/GetClosedNotDisplayedPosts.cshtml",filteringType);
        }
        //Closed Not Displayed In Json
        [HttpPost]
        public ActionResult GetAllClosedNotDisplayedPostsInJson()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            //Server Side Parameters
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            //int startRec = Request.Form.GetValues("start").First;
            //int start = Convert.ToInt32(Request.Form.GetValues("start")[0]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];


            var posts = _postService.GetClosedNotDisplayedPosts(start, length, searchValue, sortColumnName, sortDirection);
            var postOutputModel = new PostOutputModel
            {
                Posts = posts.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsClosed,
                    IsAnswered = m.IsAnswered,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    SubCategoryId = m.SubCategoryId,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList()
            };
             return Json(new { data = postOutputModel.Posts });
        }
        //Closed Not Displayed With Category Id In Json
        public ActionResult GetAllClosedNotDisplayedPostsWithCategoryIdInJson(int categoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();
            
            if (categoryId == null || categoryId == 0)
                return NotFound();

            var posts = _postService.GetClosedNotDisplayedPostsWithCategoryId(categoryId);

            if (posts == null)
                return NotFound();

            var postOutputModel = new PostOutputModel
            {
                Posts = posts.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsClosed,
                    IsAnswered = m.IsAnswered,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    SubCategoryId = m.SubCategoryId,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList()
            };
            return Json(new { data = postOutputModel.Posts });
        }
        //Closed Not Displayed With SubCategory Id In Json
        public ActionResult GetAllClosedNotDisplayedPostsWithSubCategoryIdInJson(int subCategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            

            if (subCategoryId == null || subCategoryId == 0)
                return NotFound();

            var posts = _postService.GetClosedNotDisplayedPostsWithSubCategoryId(subCategoryId);

            if (posts == null)
                return NotFound();

            var postOutputModel = new PostOutputModel
            {
                Posts = posts.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsClosed,
                    IsAnswered = m.IsAnswered,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    SubCategoryId = m.SubCategoryId,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList()
            };

            return Json(new { data = postOutputModel.Posts });
        }
        //  Post Display
        public ActionResult PostDisplay(int postId)
        {
            if (postId == null || postId == 0)
                return Json(new { result = false });
            _postService.PostDisplay(postId);
            return Json(new { result = true });
        }










        //Not Replied
        
        public ActionResult GetNotRepliedPosts(string filterType)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();
            var filteringType = new PostModel
            {
                Filter = filterType
            };

            ViewBag.url = "/Consultations/Admin/Posts/GetAllNotRepliedPosts";

            return View("~/Themes/Pavilion/Views/ConsultantAdmin/Posts/GetNotRepliedPosts.cshtml",filteringType);
        }
        //Not Replied In Json
        [HttpPost]
        public ActionResult GetAllNotRepliedPostsInJson(int categoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            //Server Side Parameters
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            //int startRec = Request.Form.GetValues("start").First;
            //int start = Convert.ToInt32(Request.Form.GetValues("start")[0]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];

            var posts = _postService.GetNotRepliedPosts(start, length, searchValue, sortColumnName, sortDirection);
            var outputModel = new PostOutputModel
            {
                Posts = posts.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsClosed,
                    IsAnswered = m.IsAnswered,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    SubCategoryId = m.SubCategoryId,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList()
            };
            return Json(new { data = outputModel.Posts });
        }
        //Not Replied With Category Id In Json
        public ActionResult GetAllNotRepliedPostsWithCategoryIdInJson(int categoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();
           
            if (categoryId == null || categoryId == 0)
                return NotFound();

            var posts = _postService.GetNotRepliedPostsWithCategoryId(categoryId);

            if (posts == null)
                return NotFound();

            var outputModel = new PostOutputModel
            {
                Posts = posts.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsClosed,
                    IsAnswered = m.IsAnswered,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    SubCategoryId = m.SubCategoryId,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList()
            };
            return Json(new { data = outputModel.Posts });
        }
        //Not Replied With SubCategory Id In Json
        public ActionResult GetAllNotRepliedPostsWithSubCategoryIdInJson(int subCategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

           

            if (subCategoryId == null || subCategoryId == 0)
                return NotFound();

            var posts = _postService.GetNotRepliedPostsWithSubCategoryId(subCategoryId);

            if (posts == null)
                return NotFound();

            var outputModel = new PostOutputModel
            {
                Posts = posts.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsClosed,
                    IsAnswered = m.IsAnswered,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    SubCategoryId = m.SubCategoryId,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList()
            };

            return Json(new { data = outputModel.Posts });
        }





        //Reserved Posts
        public ActionResult GetReservedPosts(string filterType)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            ViewBag.url = "/Consultations/Admin/Posts/GetAllReservedPosts";

            var filteringType = new PostModel
            {
                Filter = filterType
            };
            return View("~/Themes/Pavilion/Views/ConsultantAdmin/Posts/GetReservedPosts.cshtml",filteringType);
        }
        //Reserved Posts With Category Id In Json
        [HttpPost]
        public ActionResult GetAllReservedPostsInJson()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();
            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            //Server Side Parameters
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            //int startRec = Request.Form.GetValues("start").First;
            //int start = Convert.ToInt32(Request.Form.GetValues("start")[0]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];

            var posts = _postService.GetIsReservedPosts(start, length, searchValue, sortColumnName, sortDirection);
            var outputModel = new PostOutputModel
            {
                Posts = posts.Select(m => new PostModel
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsClosed,
                    IsAnswered = m.IsAnswered,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    SubCategoryId = m.SubCategoryId,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList()
            };
            return Json(new { data = outputModel.Posts });
        }
        //Reserved Posts With Category Id In Json
        public ActionResult GetAllReservedPostsWithCategoryIdInJson(int categoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();
            
            if (categoryId == null || categoryId == 0)
                return NotFound();

            var posts = _postService.GetIsReservedPostsWithCategoryId(categoryId);

            if (posts == null)
                return NotFound();

            var outputModel = new PostOutputModel
            {
                Posts = posts.Select(m => new PostModel
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsClosed,
                    IsAnswered = m.IsAnswered,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    SubCategoryId = m.SubCategoryId,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList()
            };

            return Json(new { data = outputModel.Posts });
        }
        //Reserved Posts With SubCategory Id In Json
        public ActionResult GetAllReservedPostsWithSubCategoryIdInJson(int subCategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

           

            if (subCategoryId == null || subCategoryId == 0)
                return NotFound();

            var posts = _postService.GetIsReservedPostsWithSubCategoryId(subCategoryId);

            if (posts == null)
                return NotFound();

            var outputModel = new PostOutputModel
            {
                Posts = posts.Select(m => new PostModel
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsClosed,
                    IsAnswered = m.IsAnswered,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    SubCategoryId = m.SubCategoryId,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList()
            };

            return Json(new { data = outputModel.Posts });
        }
        //Cancel Reserving Post
        public ActionResult PostCancelReserving(int postId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();
            if (postId == null || postId == 0)
                return Json(new { result = false });
            _postService.PostCancelReserving(postId);
            return Json(new { result = true });
        }









        //Open Posts
        public ActionResult GetOpenPosts(string filterType)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            var filteringType = new PostModel
            {
                Filter = filterType
            };

            ViewBag.url = "/Consultations/Admin/Posts/GetAllOpenPosts";

            return View("~/Themes/Pavilion/Views/ConsultantAdmin/Posts/GetOpenPosts.cshtml",filteringType);
        }
        //Open Posts In Json
        [HttpPost]
        public ActionResult GetAllOpenPostsInJson()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();
            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            //Server Side Parameters
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            //int startRec = Request.Form.GetValues("start").First;
            //int start = Convert.ToInt32(Request.Form.GetValues("start")[0]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];


            var posts = _postService.GetOpenPosts(start, length, searchValue, sortColumnName, sortDirection);
            var outputModel = new PostOutputModel
            {
                Posts = posts.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsClosed,
                    IsAnswered = m.IsAnswered,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    SubCategoryId = m.SubCategoryId,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList()
            };
            return Json(new { data=outputModel.Posts });

        }
       //Open Posts With Category Id In Json
        public ActionResult GetAllOpenPostsWithCategoryIdInJson(int categoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();
            
            if (categoryId == null || categoryId == 0)
                return NotFound();

            var posts = _postService.GetOpenPostsWithCategoryId(categoryId);

            if (posts == null)
                return NotFound();

            var outputModel = new PostOutputModel
            {
                Posts = posts.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsClosed,
                    IsAnswered = m.IsAnswered,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    SubCategoryId = m.SubCategoryId,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList()
            };

            return Json(new { outputModel.Posts });
        }
        //Open Posts With SubCategory Id In Json
        public ActionResult GetAllOpenPostsWithSubCategoryIdInJson(int subCategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

           

            if (subCategoryId == null || subCategoryId == 0)
                return NotFound();

            var posts = _postService.GetOpenPostsWithSubCategoryId(subCategoryId);

            if (posts == null)
                return NotFound();


            var outputModel = new PostOutputModel
            {
                Posts = posts.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsClosed = m.IsClosed,
                    IsAnswered = m.IsAnswered,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    SubCategoryId = m.SubCategoryId,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList()
            };

            return Json(new { outputModel.Posts });
        }

        #endregion
    }
}