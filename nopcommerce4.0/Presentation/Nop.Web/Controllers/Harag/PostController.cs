using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Z_Harag.Category;
using Nop.Services.Z_Harag.Comment;
using Nop.Services.Z_Harag.Helpers;
using Nop.Services.Z_Harag.Post;
using Nop.Web.Models.Consultant.Post;
using Nop.Web.Models.Harag.Category;
using Nop.Web.Models.Harag.Post;
using Nop.Web.Models.Harag.Report;

namespace Nop.Web.Controllers.Harag
{
    public class PostController : BasePublicController
    {
        #region Fields

        private readonly ICategoryService _categoryService;
        private readonly IUrlHelper _urlHelper;
        private readonly IPostService _postService;
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;
        private readonly ICommentService _commentService; 

        #endregion


        #region ctor
        public PostController(
            ICategoryService categoryService,
            IUrlHelper urlHelper,
            IPostService postService,
            Core.IWorkContext workContext,
            IHostingEnvironment env,
            ICommentService commentService
            )
        {
            this._categoryService = categoryService;
            this._urlHelper = urlHelper;
            this._postService = postService;
            this._workContext = workContext;
            this._env = env;
            this._commentService = commentService; 
        }
        #endregion


        #region helpers

        private List<LinkInfo> GetLinksForCatPosts(PagedList<Z_Harag_Post> list, string routName, int CategoryId)
        {
            var links = new List<LinkInfo>();

            if (list.HasPreviousPage)
                links.Add(CreateLinkForCatPosts(routName, list.PreviousPageNumber,
                           list.PageSize, "previousPage", "GET", CategoryId));

            links.Add(CreateLinkForCatPosts(routName, list.PageNumber,
                           list.PageSize, "self", "GET", CategoryId));

            if (list.HasNextPage)
                links.Add(CreateLinkForCatPosts(routName, list.NextPageNumber,
                           list.PageSize, "nextPage", "GET", CategoryId));

            return links;
        }

        private LinkInfo CreateLinkForCatPosts(
            string routeName, int pageNumber, int pageSize,
            string rel, string method, int CategoryId)
        {
            return new LinkInfo
            {
                Href = _urlHelper.Link(routeName,
                            new { PageNumber = pageNumber, PageSize = pageSize, CategoryId = CategoryId }),
                Rel = rel,
                Method = method
            };
        }



        private List<LinkInfo> GetLinks(PagedList<Z_Harag_Post> list, string routName)
        {
            var links = new List<LinkInfo>();

            if (list.HasPreviousPage)
                links.Add(CreateLink(routName, list.PreviousPageNumber,
                           list.PageSize, "previousPage", "GET"));

            links.Add(CreateLink(routName, list.PageNumber,
                           list.PageSize, "self", "GET"));

            if (list.HasNextPage)
                links.Add(CreateLink(routName, list.NextPageNumber,
                           list.PageSize, "nextPage", "GET"));

            return links;
        }

        private LinkInfo CreateLink(
            string routeName, int pageNumber, int pageSize,
            string rel, string method)
        {
            return new LinkInfo
            {
                Href = _urlHelper.Link(routeName,
                            new { PageNumber = pageNumber, PageSize = pageSize }),
                Rel = rel,
                Method = method
            };
        }

        private List<LinkInfo> GetLinksForSubCatPosts(PagedList<Z_Harag_Post> list, string routName, int SubCategoryId)
        {
            var links = new List<LinkInfo>();

            if (list.HasPreviousPage)
                links.Add(CreateLinkForSubCatPosts(routName, list.PreviousPageNumber,
                           list.PageSize, "previousPage", "GET", SubCategoryId));

            links.Add(CreateLinkForSubCatPosts(routName, list.PageNumber,
                           list.PageSize, "self", "GET", SubCategoryId));

            if (list.HasNextPage)
                links.Add(CreateLinkForSubCatPosts(routName, list.NextPageNumber,
                           list.PageSize, "nextPage", "GET", SubCategoryId));

            return links;
        }


        private LinkInfo CreateLinkForSubCatPosts(
            string routeName, int pageNumber, int pageSize,
            string rel, string method, int SubCategoryId)
        {
            return new LinkInfo
            {
                Href = _urlHelper.Link(routeName,
                            new { PageNumber = pageNumber, PageSize = pageSize, SubCategoryId = SubCategoryId }),
                Rel = rel,
                Method = method
            };
        }


        private List<LinkInfo> GetLinks(PagedList<Z_Harag_Category> list, string routName)
        {
            var links = new List<LinkInfo>();

            if (list.HasPreviousPage)
                links.Add(CreateLink(routName, list.PreviousPageNumber,
                           list.PageSize, "previousPage", "GET"));

            links.Add(CreateLink(routName, list.PageNumber,
                           list.PageSize, "self", "GET"));

            if (list.HasNextPage)
                links.Add(CreateLink(routName, list.NextPageNumber,
                           list.PageSize, "nextPage", "GET"));

            return links;
        }

 


        private List<LinkInfo> GetLinksForSearch(PagedList<Z_Harag_Post> list, string routName, string Term)
        {
            var links = new List<LinkInfo>();

            if (list.HasPreviousPage)
                links.Add(CreateLinkForSearch(routName, list.PreviousPageNumber,
                           list.PageSize, "previousPage", "GET", Term));

            links.Add(CreateLinkForSearch(routName, list.PageNumber,
                           list.PageSize, "self", "GET", Term));

            if (list.HasNextPage)
                links.Add(CreateLinkForSearch(routName, list.NextPageNumber,
                           list.PageSize, "nextPage", "GET", Term));

            return links;
        }


        private LinkInfo CreateLinkForSearch(
            string routeName, int pageNumber, int pageSize,
            string rel, string method, string Term)
        {
            return new LinkInfo
            {
                Href = _urlHelper.Link(routeName,
                            new { PageNumber = pageNumber, PageSize = pageSize, Term = Term }),
                Rel = rel,
                Method = method
            };
        }

        #endregion

        [HttpGet]
        public IActionResult AgreementBeforeAddPost()
        {

            if (_workContext.CurrentCustomer.IsRegistered())
            {
                if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                    ViewBag.UserRole = RolesType.Administrators;
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                    ViewBag.UserRole = RolesType.Consultant;
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                    ViewBag.UserRole = RolesType.Registered;

                if (_postService.CanAddNewPost(_workContext.CurrentCustomer.Id))
                {
                    ViewBag.CanAddNewPost = true;
                }
                else
                {
                    ViewBag.CanAddNewPost = false;
                }

                return View("~/Themes/Pavilion/Views/Harag/Post/AgreementBeforeAddPost.cshtml");
            }
            else
            {
                return RedirectToRoute("Login", new { returnUrl = "Harag" });
            }
        }
        [HttpGet]
        public IActionResult HaragAddPost()
        {

            if (_workContext.CurrentCustomer.IsRegistered())
            {
                if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                    ViewBag.UserRole = RolesType.Administrators;
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                    ViewBag.UserRole = RolesType.Consultant;
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                    ViewBag.UserRole = RolesType.Registered;

                if (_postService.CanAddNewPost(_workContext.CurrentCustomer.Id))
                {
                    ViewBag.CanAddNewPost = true;
                }
                else
                {
                    ViewBag.CanAddNewPost = false;
                }

                return View("~/Themes/Pavilion/Views/Harag/Post/AddPost.cshtml", new PostForPostListModel());
            }
            else
            {
                return RedirectToRoute("Login", new { returnUrl = "Harag" });
            }
        }

        [HttpPost]
        public IActionResult AddNewPostAjax([FromBody]PostForPostListModel postForPostModel)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                return Forbid();

           

            var currentUserId = _workContext.CurrentCustomer.Id;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (postForPostModel.CategoryId <= 0)
                return BadRequest("Post must be belong to Category");

            List<string> errors = new List<string>();

            var post = _postService.AddNewPost(postForPostModel, currentUserId, postForPostModel.Files, errors);

            if (post == null)
            {
                StringBuilder err = new StringBuilder();

                foreach (var item in errors)
                {
                    err.AppendFormat(" {0} ", item);
                    err.AppendLine();
                }
                return BadRequest(err.ToString());
            }




            string userName = _workContext.CurrentCustomer.Username;

            //var postToReturn = post.ToPostModel();
            //postToReturn.PostOwner = userName;
            //return CreatedAtRoute("Consultant.Api.Post.GetPost", new { PostId = postToReturn.Id }, postToReturn);
            return Ok(new { postId = post.Id});
        }

        [HttpGet]
        public ActionResult UpdatePostLocation(int postId)
        {
            if (_workContext.CurrentCustomer.IsRegistered())
            {
                if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                    ViewBag.UserRole = RolesType.Administrators;
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                    ViewBag.UserRole = RolesType.Consultant;
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                    ViewBag.UserRole = RolesType.Registered;

                if (!_postService.IsExists(postId))
                {
                    return NotFound();
                }

                var post = _postService.GetPost(postId, "");

                var model = new PostForPostListModel
                {
                    Id = post.Id,
                    CategoryId = post.CategoryId,
                    CityId = (int)post.CityId,
                    Lat = (double)(post.Lattiude==null?0: post.Lattiude),
                    Len = (double)(post.Longtiude==null?0: post.Longtiude)
                 };
                
                return View("~/Themes/Pavilion/Views/Harag/Post/UpdatePostLocation.cshtml", model);
            }
            else
            {
                return RedirectToRoute("Login", new { returnUrl = "Harag" });
            }
        }


        [HttpPost]
        public ActionResult UpdatePostLocationAjax([FromBody] PostForPostListModel post)
        {
            if (_workContext.CurrentCustomer.IsRegistered())
            {
                if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                    ViewBag.UserRole = RolesType.Administrators;
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                    ViewBag.UserRole = RolesType.Consultant;
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                    ViewBag.UserRole = RolesType.Registered;

                var model = new Z_Harag_Post
                {
                    Id = post.Id,
                    CategoryId = post.CategoryId,
                    CityId = (int)post.CityId,
                    Lattiude = (Decimal)post.Lat,
                    Longtiude = (Decimal)post.Len,
                    NeighborhoodId = post.NeighborhoodId
                };

                return Ok(new { status = true, postId = model.Id});
            }
            return Ok(new { status = false }); 
        }

        [HttpGet]
        public ActionResult PostDetails()
        {
            return View("~/Themes/Pavilion/Views/Harag/Post/PostDetails.cshtml");

        }

        [HttpGet]
        public ActionResult GetHaragCategories()
        {
            var categories = _categoryService.GetCategories();


            var categoriesOutput = new CategoryOutputModel
            {
                Items = categories.Select(m => new CategoryModel
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList()
            };
            return View("~/Themes/Pavilion/Views/Harag/Post/ListOfCategory.cshtml", categoriesOutput); 
        }
        //#region methods


        [HttpGet]
        public IActionResult GetHaragPost(int PostId)
        {
            if (!_postService.IsExists(PostId))
                return NotFound();
            string UserRole = "Vistor";

             Models.Harag.Post.PostWithFilesModel model = new Models.Harag.Post.PostWithFilesModel();

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            var currentUserId = _workContext.CurrentCustomer.Id;
            ViewBag.UserName = _workContext.CurrentCustomer.Username;

            if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
            {
                var post = this._postService.GetPost(PostId, "");
                if (post != null)
                {
                    model = new Models.Harag.Post.PostWithFilesModel
                    {
                        CategoryId = post.CategoryId,
                        CategoryName = post.Category.Name,
                        Text = post.Text,
                        Title = post.Title,
                        DateCreated = post.DateCreated,
                        Files = post.Z_Harag_Photo.Select(p => p.Url).ToList(),
                        Rate = post.Rate,
                        Len = (double)(post.Longtiude??0),
                        Lat = (double)(post.Lattiude??0),
                        userId = post.CustomerId,
                        CityName = post.City.ArName,
                        Contact = post.Contact,
                        CityId = (int)post.CityId,
                        Id = post.Id,
                        DateUpdated = post.DateUpdated,
                        IsDispayed = post.IsDispayed,
                        PostOwner = post.Customer.Username
                    };
                }


            }
            else
            {
                return Unauthorized();
            }

            ViewBag.UserRole = UserRole;

            return View("~/Themes/Pavilion/Views/Harag/Post/PostDetails.cshtml", model);
        }

        [HttpGet]
        public IActionResult GetHaragNavbar()
        {
            var categories = _categoryService.GetCategories();
            var model = new CategoryWithSubCategoriesOutputModel();
            if (categories != null)
            {
                model.Items = categories.Select(m => new CategoryWithSubCategoriesModel
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList();
                return PartialView("~/Themes/Pavilion/Views/Harag/Shared/_Navbar.cshtml", model);
            }
            return PartialView("~/Themes/Pavilion/Views/Harag/Shared/_Navbar.cshtml", model);
        }

        [HttpGet]
        public IActionResult GetAllFeaturedPosts()
        {
            return null;
        }

        [HttpGet]
        public IActionResult GetAllHaragPostsAjax()
        {
            var posts = _postService.GetFeaturedPosts();

            var modelOutput = new Models.Harag.Post.PostOutputModel();
            
                modelOutput.Items = posts.Select(p => new Models.Harag.Post.PostModel
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

                return View("~/Themes/Pavilion/Views/Harag/Post/PostsAjax.cshtml", modelOutput);
    
        }

        [HttpGet]
        public IActionResult GetUserPostsByUserId(int userId)
        {

            var posts = _postService.GetCurrentUserPosts(userId);

            var modelOutput = new Models.Harag.Post.PostOutputModel();

            modelOutput.Items = posts.Select(p => new Models.Harag.Post.PostModel
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

            return View("~/Themes/Pavilion/Views/Harag/Post/PostsAjax.cshtml", modelOutput);

        }

        [HttpGet]
        public IActionResult GetHaragCityPosts(string city)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return RedirectToRoute("Login", new { returnUrl = "Harag" });
            var cityO = _postService.GetCity(city);

            if (cityO == null)
                return NotFound();

            var posts = _postService.SearchByCity(cityO.Id);

            var modelOutput = new Models.Harag.Post.PostOutputModel();

            modelOutput.Items = posts.Select(p => new Models.Harag.Post.PostModel
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

            return View("~/Themes/Pavilion/Views/Harag/Post/PostsForSearch.cshtml", modelOutput); 
        }
         
        [HttpGet]
        public IActionResult GetHaragCatPosts(int catId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return RedirectToRoute("Login", new { returnUrl = "Harag" });

            var posts = _postService.SearchByCategory(catId);

            var modelOutput = new Models.Harag.Post.PostOutputModel();

            modelOutput.Items = posts.Select(p => new Models.Harag.Post.PostModel
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

            return View("~/Themes/Pavilion/Views/Harag/Post/PostsForSearch.cshtml", modelOutput);

        }

        [HttpGet]
        public IActionResult GetHaragFavoritesPosts()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return RedirectToRoute("Login", new { returnUrl = "Harag" });

            var user = _workContext.CurrentCustomer;

            var posts = _postService.GetFavoritesPosts(user.Id);

            var modelOutput = new Models.Harag.Post.PostOutputModel();

            //modelOutput.Items = posts.Select(p => new Models.Harag.Post.PostModel
            //{
            //    CategoryId = p.CategoryId,
            //    CategoryName = p.Category.Name,
            //    Text = p.Text,
            //    Id = p.Id,
            //    Title = p.Title,
            //    City = p.City.ArName,
            //    DateCreated = p.DateCreated,
            //    Photo = p.Z_Harag_Photo.Select(ppp => ppp.Url).FirstOrDefault(),
            //    Rate = p.Rate,
            //    DateUpdated = p.DateUpdated,
            //    IsDispayed = p.IsDispayed,
            //    PostOwner = p.Customer.Username
            //}).ToList();

            return View("~/Themes/Pavilion/Views/Harag/Post/PostsForSearch.cshtml", modelOutput);

        }

        [HttpGet]
        public IActionResult GetHaragUserPosts(int userId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return RedirectToRoute("Login", new { returnUrl = "Harag" });


            var posts = _postService.GetCurrentUserPosts(_workContext.CurrentCustomer.Id);

            var modelOutput = new Models.Harag.Post.PostOutputModel();

            modelOutput.Items = posts.Select(p => new Models.Harag.Post.PostModel
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

            return View("~/Themes/Pavilion/Views/Harag/Post/PostsForSearch.cshtml", modelOutput);

        }

        [HttpGet]
        public IActionResult GetNextPostsAjax(int pageId, int type)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return RedirectToRoute("Login", new { returnUrl = "Harag" });

            var posts = _postService.GetFeaturedPosts();

            var modelOutput = new Models.Harag.Post.PostOutputModel();

            if (posts.Count <= 0)
            {
                modelOutput.Items = posts.Select(p => new Models.Harag.Post.PostModel
                {
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    Text = p.Text,
                    Title = p.Title,
                    DateCreated = p.DateCreated,
                    Photo = p.Z_Harag_Photo.Select(ppp => ppp.Url).FirstOrDefault(),
                    Rate = p.Rate,
                    DateUpdated = p.DateUpdated,
                    IsDispayed = p.IsDispayed,
                    PostOwner = p.Customer.Username
                }).ToList();

                return View("~/Themes/Pavilion/Views/Harag/Shared/_Navbar.cshtml", posts);
            }
            return null;
        }

        [HttpGet]
        public IActionResult GetAllSideBarTags()
        {
            return null;
        } 

        [HttpGet]
        public IActionResult GetAllTopBarTags()
        {
            return null;
        }

        [HttpGet]
        public IActionResult GetHaragCities()
        {
            var cities = _postService.GetCities();
            var model = cities.Select(m => new CityOutputModel
            {
                Id = m.Id,
                Name = m.ArName
            }).ToList();
            return PartialView("~/Themes/Pavilion/Views/Harag/Post/ListOfCities.cshtml", model);
        }

        [HttpGet]
        public IActionResult GetHaragNeighborhoods()
        {
            var cities = _postService.GetNeighborhoods();
            var model = cities.Select(m => new NeighbohoodOutputModel
            {
                Id = m.Id,
                Name = m.ArName
            }).ToList();
            return PartialView("~/Themes/Pavilion/Views/Harag/Post/ListOfNieghbohood.cshtml", model);
        }
        [HttpGet]
        public IActionResult ReportPost(int postId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered()) 
                return RedirectToRoute("Login", new { returnUrl = "Harag/ReportPost?postid="+postId }); 

            var post = _postService.IsExists(postId);
            if (!post)
                return NotFound();

            var model = new PostReport { PostId = postId };

            return  View("~/Themes/Pavilion/Views/Harag/Report/AddReport.cshtml", model); 
        }
        [HttpPost]
        public IActionResult ReportPostAjax(PostReport model)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            
            var report = new Z_Harag_Reports
            { 
                ReportDescription = model.ReportMessage,
                PostId = (byte)model.PostId, 
                IsIllegal = model.IsIllegal,
                ReporterUser = _workContext.CurrentCustomer.Id
            };

            var result = _postService.ReportPost(report);

            if (result != null)
            {
                ViewBag.PostId = model.PostId;
                return PartialView("~/Themes/Pavilion/Views/Harag/Report/_PostAddedSuccessfully.cshtml");
            }
            return View("~/Themes/Pavilion/Views/Harag/Report/AddReport.cshtml", model);
        }
         

        [HttpGet]
        public IActionResult AddPostToFavorite(int postId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return RedirectToRoute("Login", new { returnUrl = "Harag/ReportPost?postid=" + postId });

            var post = _postService.IsExists(postId);
            if (!post)
                return NotFound();
            var isAddedToFav = _postService.IsPostAddedToFavorite(postId, _workContext.CurrentCustomer.Id);

            if(isAddedToFav)
            {
                var result = _postService.RemovePostFromFavorite(postId, _workContext.CurrentCustomer.Id);
                return Ok(new { stat = result, ok = 0});
            }
            else
            {
                var res = _postService.AddPostToFavorite(postId, _workContext.CurrentCustomer.Id); 
                return Ok(new { stat = res, ok = 1 }); 
            }  
        }
            //    var model = _postService.GetClosedPosts(pagingParams);

            //    Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            //    var outputModel = new PostOutputModel
            //    {
            //        Paging = model.GetHeader(),
            //        Links = GetLinks(model, "Consultant.Post.GetAllClosedPosts"),
            //        Items = model.List.Select(m => new PostModel()
            //        {
            //            Id = m.Id,
            //            Text = m.Text,
            //            Title = m.Title,
            //            CategoryId = m.CategoryId,
            //            IsSetToSubCategory = m.IsSetToSubCategory,
            //            PostOwner = m.Customer.Username,
            //            SubCategoryId = m.SubCategoryId,
            //            DateCreated = m.DateCreated,
            //            DateUpdated = m.DateUpdated,
            //            IsAnswered = m.IsAnswered,
            //            IsClosed = m.IsClosed,
            //            IsDispayed = m.IsDispayed,
            //            IsReserved = m.IsReserved,
            //            Rate = m.Rate,
            //            CategoryName = m.Category.Name,
            //            SubCategoryName = m.SubCategory?.Name,
            //            Photo= GetPostPhoto(m.Photos?.FirstOrDefault()?.Url)
            //        }).ToList(),
            //    };
            //    ViewBag.UserRole = "Vistor";

            //    return PartialView("~/Themes/Pavilion/Views/Consultant/Post/Posts.cshtml", outputModel);
            //}


            //private string GetPostPhoto(string photoName)
            //{
            //    if (photoName != null)
            //    {
            //        string pathReplaced = Path.Combine(Request.Host.Host + ":" + Request.Host.Port, "\\Consultations\\Images\\" + photoName)
            //            .Replace("\\", "/");
            //        return pathReplaced;
            //    }
            //    else
            //        return null;
            //}


            //[HttpGet]
            //public virtual IActionResult GetAllClosedPostsAjax(PagingParams pagingParams)
            //{
            //    var model = _postService.GetClosedPosts(pagingParams);

            //    Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            //    var outputModel = new PostOutputModel
            //    {
            //        Paging = model.GetHeader(),
            //        Links = GetLinks(model, "Consultant.Post.GetAllClosedPosts"),
            //        Items = model.List.Select(m => new PostModel()
            //        {
            //            Id = m.Id,
            //            Text = m.Text,
            //            Title = m.Title,
            //            CategoryId = m.CategoryId,
            //            IsSetToSubCategory = m.IsSetToSubCategory,
            //            PostOwner = m.Customer.Username,
            //            SubCategoryId = m.SubCategoryId,
            //            DateCreated = m.DateCreated,
            //            DateUpdated = m.DateUpdated,
            //            IsAnswered = m.IsAnswered,
            //            IsClosed = m.IsClosed,
            //            IsDispayed = m.IsDispayed,
            //            IsReserved = m.IsReserved,
            //            Rate = m.Rate,
            //            CategoryName = m.Category.Name,
            //            SubCategoryName = m.SubCategory?.Name,
            //            Photo = GetPostPhoto(m.Photos?.FirstOrDefault()?.Url)
            //        }).ToList(),
            //    };
            //    ViewBag.UserRole = "Vistor";

            //    return PartialView("~/Themes/Pavilion/Views/Consultant/Post/PostsAjax.cshtml", outputModel);
            //}


            //[HttpGet]
            //public IActionResult GetAllClosedPostsByCategoryId(PagingParams pagingParams, int CategoryId)
            //{
            //    var model = _postService.GetClosedPostsByCategoryId(pagingParams, CategoryId);

            //    Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            //    var outputModel = new PostOutputModel
            //    {
            //        Paging = model.GetHeader(),
            //        Links = GetLinksForCatPosts(model, "Consultant.GetAllClosedPostsByCategoryId", CategoryId),
            //        Items = model.List.Select(m => new PostModel()
            //        {
            //            Id = m.Id,
            //            Text = m.Text,
            //            Title = m.Title,
            //            CategoryId = m.CategoryId,
            //            SubCategoryId = m.SubCategoryId,
            //            IsSetToSubCategory = m.IsSetToSubCategory,
            //            PostOwner = m.Customer.Username,
            //            DateCreated = m.DateCreated,
            //            DateUpdated = m.DateUpdated,
            //            IsAnswered = m.IsAnswered,
            //            IsClosed = m.IsClosed,
            //            Rate = m.Rate,
            //            IsDispayed = m.IsDispayed,
            //            IsReserved = m.IsReserved,
            //            CategoryName = m.Category.Name,
            //            SubCategoryName = m.SubCategory?.Name,
            //            Photo = GetPostPhoto(m.Photos?.FirstOrDefault()?.Url)
            //        }).ToList(),
            //    };
            //    ViewBag.UserRole = "Vistor";


            //    return View("~/Themes/Pavilion/Views/Consultant/Post/Posts.cshtml", outputModel);
            //}


            //[HttpGet]
            //public IActionResult GetAllClosedPostsBySubCategoryId(PagingParams pagingParams, int SubCategoryId)
            //{

            //    var model = _postService.GetClosedPostsBySubCategoryId(pagingParams, SubCategoryId);

            //    Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            //    var outputModel = new PostOutputModel
            //    {
            //        Paging = model.GetHeader(),
            //        Links = GetLinksForSubCatPosts(model, "Consultant.Consultations.GetAllClosedPostsBySubCategoryId", SubCategoryId),
            //        Items = model.List.Select(m => new PostModel()
            //        {
            //            Id = m.Id,
            //            Text = m.Text,
            //            Title = m.Title,
            //            CategoryId = m.CategoryId,
            //            SubCategoryId = m.SubCategoryId,
            //            IsSetToSubCategory = m.IsSetToSubCategory,
            //            PostOwner = m.Customer.Username,
            //            DateCreated = m.DateCreated,
            //            DateUpdated = m.DateUpdated,
            //            IsAnswered = m.IsAnswered,
            //            IsClosed = m.IsClosed,
            //            Rate = m.Rate,
            //            IsDispayed = m.IsDispayed,
            //            IsReserved = m.IsReserved,
            //            CategoryName = m.Category.Name,
            //            SubCategoryName = m.SubCategory?.Name,
            //            Photo = GetPostPhoto(m.Photos?.FirstOrDefault()?.Url)
            //        }).ToList(),
            //    };
            //    ViewBag.UserRole = "Vistor";

            //    return View("~/Themes/Pavilion/Views/Consultant/Post/Posts.cshtml", outputModel);
            //}





            //[HttpGet]
            //public IActionResult GetCustomerOpenPosts(PagingParams pagingParams)
            //{

            //    if (!_workContext.CurrentCustomer.IsRegistered())
            //        return RedirectToRoute("Login", new { returnUrl = "consultations" });


            //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
            //        return Forbid();

            //    ViewBag.UserRole = RolesType.Registered;
            //    var currentUserId = _workContext.CurrentCustomer.Id;


            //    var model = _postService.GetOpenPostsForCustomer(pagingParams, currentUserId);

            //    Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            //    var outputModel = new PostOutputModel
            //    {
            //        Paging = model.GetHeader(),
            //        Links = GetLinks(model, "Consultant.Post.GetCustomerOpenPosts"),
            //        Items = model.List.Select(m => new PostModel()
            //        {
            //            Id = m.Id,
            //            Text = m.Text,
            //            Title = m.Title,
            //            CategoryId = m.CategoryId,
            //            SubCategoryId = m.SubCategoryId,
            //            IsSetToSubCategory = m.IsSetToSubCategory,
            //            PostOwner = m.Customer.Username,
            //            DateCreated = m.DateCreated,
            //            DateUpdated = m.DateUpdated,
            //            IsAnswered = m.IsAnswered,
            //            IsClosed = m.IsClosed,
            //            Rate = m.Rate,
            //            IsDispayed = m.IsDispayed,
            //            IsReserved = m.IsReserved,
            //            CategoryName = m.Category.Name,
            //            SubCategoryName = m.SubCategory?.Name,
            //            Photo = GetPostPhoto(m.Photos?.FirstOrDefault()?.Url)
            //        }).ToList(),
            //    };
            //    return View("~/Themes/Pavilion/Views/Consultant/Post/CustomerOpenConsultations.cshtml", outputModel);
            //}



            //[HttpGet]
            //public IActionResult GetCustomerClosedPosts(PagingParams pagingParams)
            //{
            //    if (!_workContext.CurrentCustomer.IsRegistered())
            //        return Unauthorized();


            //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
            //        return Forbid();
            //    ViewBag.UserRole = RolesType.Registered;

            //    var currentUserId = _workContext.CurrentCustomer.Id;

            //    var model = _postService.GetClosedPostsForCustomer(pagingParams, currentUserId);

            //    Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            //    var outputModel = new PostOutputModel
            //    {
            //        Paging = model.GetHeader(),
            //        Links = GetLinks(model, "Consultant.Post.GetCustomerClosedPosts"),
            //        Items = model.List.Select(m => new PostModel()
            //        {
            //            Id = m.Id,
            //            Text = m.Text,
            //            Title = m.Title,
            //            CategoryId = m.CategoryId,
            //            SubCategoryId = m.SubCategoryId,
            //            IsSetToSubCategory = m.IsSetToSubCategory,
            //            PostOwner = m.Customer.Username,
            //            DateCreated = m.DateCreated,
            //            DateUpdated = m.DateUpdated,
            //            IsAnswered = m.IsAnswered,
            //            IsClosed = m.IsClosed,
            //            Rate = m.Rate,
            //            IsDispayed = m.IsDispayed,
            //            IsReserved = m.IsReserved,
            //            CategoryName = m.Category.Name,
            //            SubCategoryName = m.SubCategory?.Name,
            //            Photo = GetPostPhoto(m.Photos?.FirstOrDefault()?.Url)
            //        }).ToList(),
            //    };
            //    return View("~/Themes/Pavilion/Views/Consultant/Post/CustomerClosedConsultations.cshtml", outputModel);
            //}


            //[HttpGet]
            //public IActionResult GetCustomerCommonPosts(PagingParams pagingParams)
            //{
            //    if (!_workContext.CurrentCustomer.IsRegistered())
            //        return Unauthorized();


            //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
            //        return Forbid();
            //    ViewBag.UserRole = RolesType.Registered;

            //    var currentUserId = _workContext.CurrentCustomer.Id;

            //    var model = _postService.GetCommonPostsForCustomer(pagingParams, currentUserId);

            //    Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            //    var outputModel = new PostOutputModel
            //    {
            //        Paging = model.GetHeader(),
            //        Links = GetLinks(model, "Consultant.Post.GetCustomerClosedPosts"),
            //        Items = model.List.Select(m => new PostModel()
            //        {
            //            Id = m.Id,
            //            Text = m.Text,
            //            Title = m.Title,
            //            CategoryId = m.CategoryId,
            //            SubCategoryId = m.SubCategoryId,
            //            IsSetToSubCategory = m.IsSetToSubCategory,
            //            PostOwner = m.Customer.Username,
            //            DateCreated = m.DateCreated,
            //            DateUpdated = m.DateUpdated,
            //            IsAnswered = m.IsAnswered,
            //            IsClosed = m.IsClosed,
            //            Rate = m.Rate,
            //            IsCommon = m.IsCommon,
            //            IsDispayed = m.IsDispayed,
            //            IsReserved = m.IsReserved,
            //            CategoryName = m.Category.Name,
            //            SubCategoryName = m.SubCategory?.Name,
            //            Photo = GetPostPhoto(m.Photos?.FirstOrDefault()?.Url)
            //        }).ToList(),
            //    };
            //    return View("~/Themes/Pavilion/Views/Consultant/Post/CustomerClosedConsultations.cshtml", outputModel);
            //}
            //[HttpGet]

            //private IActionResult GetAllCitiesAjax()
            //{
            //    var cities = _postService.GetCities();

            //    return Json(cities);
            //}
            //[HttpGet]
            //private IActionResult GetAllCityNeighborhoodAjax(int cityId)
            //{
            //    var cityNeighborhood = _postService.GetCityNeighborhood(cityId);

            //    return Json(cityNeighborhood);
            //}
            //[HttpGet]
            //public virtual IActionResult GetConsultantNotAnsweredPosts(PagingParams pagingParams)
            //{

            //    if (!_workContext.CurrentCustomer.IsRegistered())
            //        return Unauthorized();

            //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
            //        return Forbid();


            //    ViewBag.UserRole = RolesType.Consultant;
            //    var model = _postService.GetOpenPostsNotAns(pagingParams);

            //    Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            //    var outputModel = new PostOutputModel
            //    {
            //        Paging = model.GetHeader(),
            //        Links = GetLinks(model, "Consultant.Post.GetConsultantNotAnsweredPosts"),
            //        Items = model.List.Select(m => new PostModel()
            //        {
            //            Id = m.Id,
            //            Text = m.Text,
            //            Title = m.Title,
            //            CategoryId = m.CategoryId,
            //            SubCategoryId = m.SubCategoryId,
            //            IsSetToSubCategory = m.IsSetToSubCategory,
            //            PostOwner = m.Customer.Username,
            //            DateCreated = m.DateCreated,
            //            DateUpdated = m.DateUpdated,
            //            IsAnswered = m.IsAnswered,
            //            IsClosed = m.IsClosed,
            //            Rate = m.Rate,
            //            IsDispayed = m.IsDispayed,
            //            IsReserved = m.IsReserved,
            //            CategoryName = m.Category.Name,
            //            SubCategoryName = m.SubCategory?.Name,
            //            Photo = GetPostPhoto(m.Photos?.FirstOrDefault()?.Url)
            //        }).ToList(),
            //    };
            //    return View("~/Themes/Pavilion/Views/Consultant/Post/ConsultantNotAnswerdConsultations.cshtml", outputModel);
            //}



            //[HttpGet]
            //public IActionResult GetConsultantReservedPosts(PagingParams pagingParams)
            //{

            //    if (!_workContext.CurrentCustomer.IsRegistered())
            //        return Unauthorized();

            //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
            //        return Forbid();
            //    ViewBag.UserRole = RolesType.Consultant;
            //    var currentUserId = _workContext.CurrentCustomer.Id;


            //    var model = _postService.GetReservedPostsForConsultant(pagingParams, currentUserId);

            //    Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            //    var outputModel = new PostOutputModel
            //    {
            //        Paging = model.GetHeader(),
            //        Links = GetLinks(model, "Consultant.Post.GetConsultantReservedPosts"),
            //        Items = model.List.Select(m => new PostModel()
            //        {
            //            Id = m.Id,
            //            Text = m.Text,
            //            Title = m.Title,
            //            CategoryId = m.CategoryId,
            //            SubCategoryId = m.SubCategoryId,
            //            IsSetToSubCategory = m.IsSetToSubCategory,
            //            PostOwner = m.Customer.Username,
            //            DateCreated = m.DateCreated,
            //            DateUpdated = m.DateUpdated,
            //            IsAnswered = m.IsAnswered,
            //            IsClosed = m.IsClosed,
            //            Rate = m.Rate,
            //            IsDispayed = m.IsDispayed,
            //            IsReserved = m.IsReserved,
            //            CategoryName = m.Category.Name,
            //            SubCategoryName = m.SubCategory?.Name,
            //            Photo = GetPostPhoto(m.Photos?.FirstOrDefault()?.Url)
            //        }).ToList(),
            //    };
            //    return PartialView("~/Themes/Pavilion/Views/Consultant/Post/ConsultantReservedConsultations.cshtml", outputModel);
            //}



            //[HttpGet]
            //public IActionResult GetConsultantOpenPosts(PagingParams pagingParams)
            //{

            //    if (!_workContext.CurrentCustomer.IsRegistered())
            //        return Unauthorized();

            //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
            //        return Forbid();
            //    ViewBag.UserRole = RolesType.Consultant;
            //    var currentUserId = _workContext.CurrentCustomer.Id;


            //    var model = _postService.GetOpenPostsForConsultant(pagingParams, currentUserId);

            //    Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            //    var outputModel = new PostOutputModel
            //    {
            //        Paging = model.GetHeader(),
            //        Links = GetLinks(model, "Consultant.Post.GetConsultantOpenPosts"),
            //        Items = model.List.Select(m => new PostModel()
            //        {
            //            Id = m.Id,
            //            Text = m.Text,
            //            Title = m.Title,
            //            CategoryId = m.CategoryId,
            //            SubCategoryId = m.SubCategoryId,
            //            IsSetToSubCategory = m.IsSetToSubCategory,
            //            PostOwner = m.Customer.Username,
            //            DateCreated = m.DateCreated,
            //            DateUpdated = m.DateUpdated,
            //            IsAnswered = m.IsAnswered,
            //            IsClosed = m.IsClosed,
            //            Rate = m.Rate,
            //            IsDispayed = m.IsDispayed,
            //            IsReserved = m.IsReserved,
            //            CategoryName = m.Category.Name,
            //            SubCategoryName = m.SubCategory?.Name,
            //            Photo = GetPostPhoto(m.Photos?.FirstOrDefault()?.Url)
            //        }).ToList(),
            //    };
            //    return PartialView("~/Themes/Pavilion/Views/Consultant/Post/ConsultantOpenConsultations.cshtml", outputModel);
            //}



            //[HttpGet]
            //public IActionResult GetConsultantClosedPosts(PagingParams pagingParams)
            //{

            //    if (!_workContext.CurrentCustomer.IsRegistered())
            //        return Unauthorized();

            //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
            //        return Forbid();
            //    ViewBag.UserRole = RolesType.Consultant;
            //    var currentUserId = _workContext.CurrentCustomer.Id;


            //    var model = _postService.GetClosedPostsForConsultant(pagingParams, currentUserId);

            //    Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            //    var outputModel = new PostOutputModel
            //    {
            //        Paging = model.GetHeader(),
            //        Links = GetLinks(model, "Consultant.Post.GetConsultantClosedPosts"),
            //        Items = model.List.Select(m => new PostModel()
            //        {
            //            Id = m.Id,
            //            Text = m.Text,
            //            Title = m.Title,
            //            CategoryId = m.CategoryId,
            //            SubCategoryId = m.SubCategoryId,
            //            IsSetToSubCategory = m.IsSetToSubCategory,
            //            PostOwner = m.Customer.Username,
            //            DateCreated = m.DateCreated,
            //            DateUpdated = m.DateUpdated,
            //            IsAnswered = m.IsAnswered,
            //            IsClosed = m.IsClosed,
            //            Rate = m.Rate,
            //            IsDispayed = m.IsDispayed,
            //            IsReserved = m.IsReserved,
            //            CategoryName = m.Category.Name,
            //            SubCategoryName = m.SubCategory?.Name,
            //            Photo = GetPostPhoto(m.Photos?.FirstOrDefault()?.Url)
            //        }).ToList(),
            //    };
            //    return PartialView("~/Themes/Pavilion/Views/Consultant/Post/ConsultantClosedConsultations.cshtml", outputModel);
            //}







            //[HttpGet]
            //public IActionResult GetPost(int PostId)
            //{
            //    if (!_postService.IsExist(PostId))
            //        return NotFound();
            //    string UserRole = "Vistor";

            //    PostWithFilesModel model  = new PostWithFilesModel();

            //    if (!_postService.IsClosed(PostId))
            //    {
            //        if (!_workContext.CurrentCustomer.IsRegistered())
            //            return Unauthorized();

            //        var currentUserId = _workContext.CurrentCustomer.Id;
            //        ViewBag.UserName = _workContext.CurrentCustomer.Username;

            //        if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
            //        {
            //            UserRole = RolesType.Consultant;
            //            if (_postService.IsReserved(PostId))
            //            {
            //                if (_postService.IsConsultantAuthToPost(PostId, currentUserId))
            //                {
            //                    model = GetPostWithPhotosPrivate(PostId);
            //                }
            //                else
            //                {
            //                    return Forbid();
            //                }
            //            }
            //            else
            //            {
            //                model = GetPostWithPhotosPrivate(PostId);
            //            }

            //        }
            //        else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
            //        {
            //            UserRole = RolesType.Registered;
            //            if (_postService.IsCustomerAuthToPost(PostId, currentUserId))
            //            {
            //                model = GetPostWithPhotosPrivate(PostId);
            //            }
            //            else
            //            {
            //                return Forbid();
            //            }
            //        }
            //        else
            //        {
            //            return Unauthorized();
            //        }
            //    }
            //    else
            //    {
            //        model = GetPostWithPhotos(PostId);
            //    }

            //    ViewBag.UserRole = UserRole;

            //    return View("~/Themes/Pavilion/Views/Consultant/Post/PostDetails.cshtml", model);
            //}




            //[HttpGet]
            //public IActionResult getFile(string path)
            //{
            //    return File(Path.Combine(_env.WebRootPath, "/ConsultantApi/Uploads/Images/" + path), contentType: "image/*");
            //}

            //private PostWithFilesModel GetPostWithPhotos(int id)
            //{
            //    Z_Consultant_Post model = null;
            //    model = _postService.GetPost(id);
            //    List<string> files = new List<string>();
            //    foreach (var photo in model.Photos)
            //    {
            //        string pathReplaced = Path.Combine(Request.Host.Host + ":" + Request.Host.Port, "\\Consultations\\Images\\" + photo.Url)
            //            .Replace("\\", "/");
            //        files.Add(pathReplaced);
            //    }
            //    var modelToReturn = model.ToPostWithFilesModel();
            //    modelToReturn.CategoryName = model.Category.Name;
            //    modelToReturn.SubCategoryName = model.SubCategory?.Name;
            //    modelToReturn.Files = files;
            //    return modelToReturn;
            //}





            //[HttpGet]
            //public IActionResult getFilePrivate(string path)
            //{
            //    return File(Path.Combine(_env.WebRootPath, "/ConsultantApi/Uploads/Images/" + path), contentType: "image/*");
            //}

            //private PostWithFilesModel GetPostWithPhotosPrivate(int id)
            //{
            //    Z_Consultant_Post model = null;
            //    model = _postService.GetPost(id);
            //    List<string> files = new List<string>();
            //    foreach (var photo in model.Photos)
            //    {
            //        string pathReplaced = Path.Combine(Request.Host.Host + ":" + Request.Host.Port, "\\Consultations\\Imgs\\" + photo.Url)
            //            .Replace("\\", "/");
            //        files.Add(pathReplaced);
            //    }
            //    var modelToReturn = model.ToPostWithFilesModel();
            //    modelToReturn.CategoryName = model.Category.Name;
            //    modelToReturn.SubCategoryName = model.SubCategory?.Name;
            //    modelToReturn.Files = files;
            //    return modelToReturn;
            //}




            //[HttpPost]
            //public IActionResult ClosePost([FromBody]ClosePostModel closePostModel)
            //{
            //    if (!_workContext.CurrentCustomer.IsRegistered())
            //        return Unauthorized();


            //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
            //        return Forbid();


            //    var currentUserId = _workContext.CurrentCustomer.Id;


            //    if (!_postService.IsCustomerAuthToPost(closePostModel.Id, currentUserId))
            //        return Forbid();


            //    if (!ModelState.IsValid)
            //        return BadRequest(ModelState);


            //    if (closePostModel.Id <= 0)
            //        return BadRequest("Rating and closing must be belong to post");



            //    if (!_postService.IsAnswered(closePostModel.Id))
            //        return BadRequest("The post must be answered to closing it");

            //    if (!_postService.IsRated(closePostModel.Id))
            //        return BadRequest("The post must be rated to closing it");

            //    var thereIsCahnges = _postService.ClosePost(closePostModel, currentUserId);

            //    if (thereIsCahnges)
            //        return Ok();
            //    else
            //        return BadRequest();
            //}



            //[HttpPost]
            //public IActionResult RatePost([FromBody]RatePostModel RatePostModel)
            //{
            //    if (!_workContext.CurrentCustomer.IsRegistered())
            //        return Unauthorized();


            //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
            //        return Forbid();


            //    var currentUserId = _workContext.CurrentCustomer.Id;


            //    if (!_postService.IsCustomerAuthToPost(RatePostModel.Id, currentUserId))
            //        return Forbid();


            //    if (!ModelState.IsValid)
            //        return BadRequest(ModelState);


            //    if (RatePostModel.Id <= 0)
            //        return BadRequest("Rating and closing must be belong to post");


            //    if (!_postService.IsAnswered(RatePostModel.Id))
            //        return BadRequest("The post must be answered to  rating it");

            //    var thereIsCahnges = _postService.RatePost(RatePostModel, currentUserId);

            //    if (thereIsCahnges)
            //        return Ok();
            //    else
            //        return BadRequest();
            //}


            //[HttpPost]
            //public IActionResult ReservePost([FromBody] ReservePostModel reservePost)
            //{

            //    if (!_workContext.CurrentCustomer.IsRegistered())
            //        return Unauthorized();

            //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
            //        return Forbid();
            //    if (!ModelState.IsValid)
            //        return BadRequest(ModelState);

            //    if (!_postService.IsExist(reservePost.PostId))
            //        return BadRequest("Post not found");
            //    if (_postService.IsReserved(reservePost.PostId))
            //        return Forbid("post already reserved");


            //    var curruntUserId = _workContext.CurrentCustomer.Id;
            //    _postService.ReservePost(reservePost.PostId, curruntUserId);
            //    return Ok();
            //}

            //[HttpPost]
            //public IActionResult UnReservePost([FromBody] ReservePostModel reservePost)
            //{
            //    if (!_workContext.CurrentCustomer.IsRegistered())
            //        return Unauthorized();

            //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
            //        return Forbid();

            //    if (!ModelState.IsValid)
            //        return BadRequest(ModelState);

            //    if (!_postService.IsExist(reservePost.PostId))
            //        return BadRequest("Post not found");
            //    if (!_postService.IsReserved(reservePost.PostId))
            //        return BadRequest("Post not Reserved");

            //    var curruntUserId = _workContext.CurrentCustomer.Id;
            //    if (!_postService.IsConsultantAuthToPost(reservePost.PostId, curruntUserId))
            //        return Forbid();

            //    _postService.UnReservePost(reservePost.PostId);
            //    return Ok();
            //}


            //[HttpGet]
            //public virtual IActionResult ChangeCatAndSub(PagingParams pagingParams)
            //{

            //    var model = _categoryService.GetCategories(pagingParams);

            //    Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            //    var outputModel = new CategoryOutputModel
            //    {
            //        Paging = model.GetHeader(),
            //        Links = GetLinks(model, "Consultant.Consultations.ChangeCatAndSub"),
            //        Items = model.List.Select(m => new CategoryModel()
            //        {
            //            Id = m.Id,
            //            Name = m.Name
            //        }).ToList(),
            //    };


            //    return PartialView("~/Themes/Pavilion/Views/Consultant/Post/ChangeCatAndSub.cshtml", outputModel);
            //}

            //[HttpGet]
            //public IActionResult GetCategory(int categoryId)
            //{


            //    var subCategories = _subCategoryService.GetSubCategoriesByCategory(categoryId);

            //    var sub = new SubCategoryOutputModel
            //    {
            //        Items = subCategories.Select(m => new SubCategoryModel {
            //            Id=m.Id,
            //            Name=m.Name
            //        }).ToList()
            //    };
            //   // return Json(new { data = sub.Items });
            //    return View("~/Themes/Pavilion/Views/Consultant/Post/ChangeCatAndSub.cshtml", sub.Items);
            //}



            //[HttpGet]
            //public IActionResult GetSubCategoriesByCategoryId(PagingParams pagingParams,int categoryId)
            //{
            //   var model = _subCategoryService.GetSubCategoriesByCategoryId(pagingParams, categoryId);


            //    Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            //    var outputModel = new SubCategoryOutputModel
            //    {
            //        Paging = model.GetHeader(),
            //        Links = GetLinks(model, "Consultant.Consultations.GetSubCategoriesByCategoryId"),
            //        Items = model.List.Select(m => new SubCategoryModel()
            //        {
            //            Id = m.Id,
            //            Name = m.Name
            //        }).ToList(),
            //    };
            //    return PartialView("~/Themes/Pavilion/Views/Consultant/Post/ListOfSubCategories.cshtml", outputModel);
            //}


            //[HttpPost]
            //public IActionResult SetPostToCategoryAndSubCategory([FromBody] SetPostToCategoryAndSubCategoryModel model)
            //{

            //    if (!_workContext.CurrentCustomer.IsRegistered())
            //        return Unauthorized();

            //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
            //        return Forbid();

            //    var currentUserId = _workContext.CurrentCustomer.Id;



            //    if (_postService.IsConsultantAuthToPost(model.PostId, currentUserId))
            //    {
            //        if (!ModelState.IsValid)
            //            return BadRequest(ModelState);

            //        if (!_postService.IsExist(model.PostId))
            //            return BadRequest("post not found");

            //        //if (_reop.IsSetedToSubCategory(model.PostId))
            //        //    return BadRequest("The post have been setted up to subcategory before that");

            //        if (!_postService.IsCategoryConatinTheSub(model))
            //            return BadRequest("The SubCategory is not belong to the category");

            //        _postService.SetPostToCategoryAndSubCategory(model);

            //        return Ok();
            //    }
            //    else
            //    {
            //        return Forbid();
            //    }

            //}




            //[HttpGet]
            //public IActionResult HasOpenedConsultationAnCanAdd(int userId)
            //{
            //    if (_postService.GetOpenPostsForCustomer(new PagingParams(), userId).List.Count > 0)
            //    {
            //        return Ok(false);
            //    }
            //    else
            //    {
            //        return Ok(true);
            //    }
            //}



            //[HttpGet]
            //public virtual IActionResult GetCategories(PagingParams pagingParams)
            //{
            //    var model = _categoryService.GetCategories(pagingParams);

            //    Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            //    var outputModel = new CategoryOutputModel
            //    {
            //        Paging = model.GetHeader(),
            //        Links = GetLinks(model, "Consultant.Post.GetCategories"),
            //        Items = model.List.Select(m => new CategoryModel()
            //        {
            //            Id = m.Id,
            //            Name = m.Name
            //        }).ToList(),
            //    };


            //    return PartialView("~/Themes/Pavilion/Views/Consultant/Post/ListOfCategory.cshtml", outputModel);
            //}



            //[HttpPost]
            //public IActionResult AddPostAjax([FromBody]PostForPostModel postForPostModel)
            //{

            //    if (!_workContext.CurrentCustomer.IsRegistered())
            //        return Unauthorized();


            //    if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
            //        return Forbid();


            //    var currentUserId = _workContext.CurrentCustomer.Id;

            //    if (!ModelState.IsValid)
            //        return BadRequest(ModelState);


            //    if (postForPostModel.CategoryId <= 0)
            //        return BadRequest("Post must be belong to Category");

            //    List<string> errors = new List<string>();

            //    var post = _postService.AddNewPost(postForPostModel, currentUserId, postForPostModel.Files, errors);

            //    if (post == null)
            //    {
            //        StringBuilder err = new StringBuilder();

            //        foreach (var item in errors)
            //        {
            //            err.AppendFormat(" {0} ", item);
            //            err.AppendLine();
            //        }
            //        return BadRequest(err.ToString());
            //    }




            //    string userName = _workContext.CurrentCustomer.Username;

            //    var postToReturn = post.ToPostModel();
            //    postToReturn.PostOwner = userName;
            //    //return CreatedAtRoute("Consultant.Api.Post.GetPost", new { PostId = postToReturn.Id }, postToReturn);
            //    return Ok();
            //}





            //public IActionResult Search(string Term, PagingParams pagingParams)
            //{
            //    if (string.IsNullOrEmpty(Term))
            //        return BadRequest(ModelState);

            //    SearchModel SearchModel = new SearchModel() { Term = Term };

            //    var model = _postService.SearchPosts(pagingParams, SearchModel);

            //    Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            //    var outputModel = new PostOutputModel
            //    {
            //        Paging = model.GetHeader(),
            //        Links = GetLinksForSearch(model, "Consultant.Post.Search", Term),
            //        Items = model.List.Select(m => new PostModel()
            //        {
            //            Id = m.Id,
            //            Text = m.Text,
            //            Title = m.Title,
            //            CategoryId = m.CategoryId,
            //            IsSetToSubCategory = m.IsSetToSubCategory,
            //            PostOwner = m.Customer.Username,
            //            SubCategoryId = m.SubCategoryId,
            //            DateCreated = m.DateCreated,
            //            DateUpdated = m.DateUpdated,
            //            IsAnswered = m.IsAnswered,
            //            IsClosed = m.IsClosed,
            //            IsDispayed = m.IsDispayed,
            //            IsReserved = m.IsReserved,
            //            Rate = m.Rate,
            //            CategoryName = m.Category.Name,
            //            SubCategoryName = m.SubCategory?.Name,
            //            Photo = GetPostPhoto(m.Photos?.FirstOrDefault()?.Url)
            //        }).ToList(),
            //    };
            //    ViewBag.UserRole = "Vistor";
            //    return View("~/Themes/Pavilion/Views/Consultant/Post/Posts.cshtml", outputModel);
            //}

            //#endregion 
        }
}
