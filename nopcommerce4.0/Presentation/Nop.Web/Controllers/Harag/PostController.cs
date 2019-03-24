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
using Nop.Services.Z_Harag.Notification;
using Nop.Services.Z_Harag.Post;
using Nop.Web.Models.Consultant.Post;
using Nop.Web.Models.Harag.Category;
using Nop.Web.Models.Harag.Post;
using Nop.Services.Customers;
using Nop.Web.Models.Harag.Report;
using PostOutputModel = Nop.Web.Models.Harag.Post.PostOutputModel;
using Nop.Services.Z_Harag.Follow;
using Nop.Services.Z_HaragAdmin.Setting;
using Nop.Services.Z_Harag.Rate;

namespace Nop.Web.Controllers.Harag
{
    public class PostController : BasePublicController
    {
        #region Fields
        public SettingsModel Setting;
        private readonly ICategoryService _categoryService;
        private readonly INotificationService _notificationService;
        private readonly IUrlHelper _urlHelper;
        private readonly ISettingService _settingsService;
        private readonly IRateSrevice rateService;
        private readonly ICustomerService customerService;
        private readonly IPostService _postService;
        private readonly Core.IWorkContext _workContext;
        private readonly IFollowService _followService;
        private readonly IHostingEnvironment _env;
        private readonly ICommentService _commentService;
        public PagingParams PagingParams { get; set; }
        #endregion 

        #region ctor
        public PostController(
            ICategoryService categoryService,
            IRateSrevice rateService,
            IUrlHelper urlHelper,
             ICustomerService customerService,
            ISettingService _settingsService,
             IFollowService _followService,
            IPostService postService,
            Core.IWorkContext workContext,
            INotificationService _notificationService,
            IHostingEnvironment env,
            ICommentService commentService
            )
        {
            this.rateService = rateService;
            this._settingsService = _settingsService;
            customerService = customerService;
            this._followService = _followService;
            this._categoryService = categoryService;
            this._urlHelper = urlHelper;
            this._postService = postService;
            this._workContext = workContext;
            this._notificationService = _notificationService;
            this._env = env;
            this._commentService = commentService;

            Setting = _settingsService.GetSettings();
            PagingParams = new PagingParams();
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

        #region Methods
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
        public IActionResult RefreshPost(int postId)
        {
            if (_workContext.CurrentCustomer.IsRegistered())
            {
                var post = _postService.GetPost(postId, "");

                if (post == null)
                {
                    return Ok(new { state = false, error = "لا يمكن ايجاد هذا الاعلان" });
                }

                if (DateTime.Now - post.DateCreated < TimeSpan.FromDays(Setting.UpdatePostBefor))
                {
                    return Ok(new { state = false, error = "لا يمكن تحديث الاعلان الا بعد مرور " + Setting.UpdatePostBefor + " ايام علي نشره" });
                }
                var refreshed = _postService.RefreshPost(postId);

                return Ok(new { state = true });
            }
            return Ok(new { state = false, error = "سجل الدخول اولا" });
        }

        [HttpGet]
        public IActionResult SetPostFeatured(int postId)
        {
            if (_workContext.CurrentCustomer.IsRegistered() && _workContext.CurrentCustomer.IsFeatured)
            {
                var post = _postService.GetPost(postId, "");

                if (post == null)
                {
                    return Ok(new { state = false });
                }
                if (_postService.CanSetFeaturedPost(_workContext.CurrentCustomer.Id))
                {
                    if ((bool)post.IsFeatured)
                    {
                        return Ok(new { state = false, added = false });
                    }
                    else
                    {
                        var refreshed = _postService.SetFeatured(post.Id);
                        return Ok(new { state = true, added = true });
                    }
                }
                else
                {
                    return Ok(new { state = false });
                }
            }
            return Ok(new { state = false });
        }

        [HttpGet]
        public IActionResult OpenCloseCommentingPost(int postId)
        {
            if (_workContext.CurrentCustomer.IsRegistered() && _workContext.CurrentCustomer.IsFeatured)
            {
                var post = _postService.GetPost(postId, "");

                if (post == null)
                {
                    return Ok(new { state = false });
                }

                if (post.IsCommentingClosed)
                {
                    var closed = _postService.openCommenting(postId);

                    return Ok(new { state = true, closed = false });
                }
                else
                {
                    var closed = _postService.CloseCommenting(postId);
                    return Ok(new { state = true, closed = true });
                }

            }
            return Ok(new { state = false });
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

        [HttpGet]
        public IActionResult HaragUpdatePost(int postId)
        {

            if (_workContext.CurrentCustomer.IsRegistered())
            {
                if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                    ViewBag.UserRole = RolesType.Administrators;
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                    ViewBag.UserRole = RolesType.Consultant;
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                    ViewBag.UserRole = RolesType.Registered;

                //if (_postService.CanAddNewPost(_workContext.CurrentCustomer.Id))
                //{
                //    ViewBag.CanAddNewPost = true;
                //}
                //else
                {
                    ViewBag.CanAddNewPost = false;
                }

                var post = _postService.GetPost(postId, "");

                var postModel = new PostForPostListModel
                {
                    CategoryId = post.CategoryId,
                    CityId = (int)post.CityId,
                    Contact = post.Contact,
                    Files = post.Z_Harag_Photo.Select(m => m.Url).ToList(),
                    NeighborhoodId = post.NeighborhoodId == null ? 0 : (int)post.NeighborhoodId,
                    Id = post.Id,

                    Title = post.Title,
                    Text = post.Text
                };

                return View("~/Themes/Pavilion/Views/Harag/Post/UpdatePost.cshtml", postModel);
            }
            else
            {
                return RedirectToRoute("Login", new { returnUrl = "Harag" });
            }
        }
        [HttpPost]
        public IActionResult UpdatePostAjax([FromBody]PostForPostListModel postForPostModel)
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

            var post = _postService.UpdatePost(postForPostModel, currentUserId, postForPostModel.Files, errors);

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

            var notifications = _notificationService.PushUserPostsNotification(new UserForNotifyModel
            {
                CustomerId = currentUserId,
                Text = post.Title,
                PostId = post.Id,
                Time = DateTime.Now
            });

            string userName = _workContext.CurrentCustomer.Username;

            //var postToReturn = post.ToPostModel();
            //postToReturn.PostOwner = userName;
            //return CreatedAtRoute("Consultant.Api.Post.GetPost", new { PostId = postToReturn.Id }, postToReturn);
            return Ok(new { postId = post.Id });
        }


        [HttpPost]
        public IActionResult AddNewPostAjax([FromBody]PostForPostListModel postForPostModel)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                return Redirect("/Login");


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

            var notifications = _notificationService.PushUserPostsNotification(new UserForNotifyModel
            {
                CustomerId = currentUserId,
                Text = post.Title,
                PostId = post.Id,
                Time = DateTime.Now
            });

            string userName = _workContext.CurrentCustomer.Username;

            //var postToReturn = post.ToPostModel();
            //postToReturn.PostOwner = userName;
            //return CreatedAtRoute("Consultant.Api.Post.GetPost", new { PostId = postToReturn.Id }, postToReturn);
            return Ok(new { postId = post.Id });
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
                    Lat = (double)(post.Lattiude == null ? 0 : post.Lattiude),
                    Len = (double)(post.Longtiude == null ? 0 : post.Longtiude)
                };

                return View("~/Themes/Pavilion/Views/Harag/Post/UpdatePostLocation.cshtml", model);
            }
            else
            {
                return RedirectToRoute("Login", new { returnUrl = "Harag" });
            }
        }


        [HttpPost]
        public ActionResult UpdatePostLocationAjax([FromBody] UpdatePostLocationModel post)
        {
            if (_workContext.CurrentCustomer.IsRegistered())
            {
                if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                    ViewBag.UserRole = RolesType.Administrators;
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                    ViewBag.UserRole = RolesType.Consultant;
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                    ViewBag.UserRole = RolesType.Registered;

                var postId = _postService.UpdatePostLocation(post);

                if (postId == 0)
                {
                    return Ok(new { status = false });
                }

                return Ok(new { status = true, postId = postId });
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
              
            var Following = false;
            var isPostOwner = false;
            var post = this._postService.GetPost(PostId, "");
            var relatedPosts = _postService.SearchByCategory(post.CategoryId, PagingParams);
            var sameCityPosts = _postService.SearchByCity((int)post.CityId, PagingParams);
            var currentUserId = 0;

            if (post != null)
            {
                currentUserId = _workContext.CurrentCustomer == null ? 0 : _workContext.CurrentCustomer.Id;
                ViewBag.UserName = _workContext.CurrentCustomer.Username;
                ViewBag.LoggedIn = true;

                UserRating userRating = new UserRating
                {
                    DownRating = rateService.GetUserDownRates(post.CustomerId).Count,
                    UpRating   = rateService.GetUserUpRates(post.CustomerId).Count
                };

                if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                {
                    Following = _followService.IsPostFollowed(post.Id, currentUserId);
                    isPostOwner = (post.CustomerId == currentUserId);
                }
                else
                {
                    ViewBag.UserName = "";
                    ViewBag.LoggedIn = false;

                }
                model = new Models.Harag.Post.PostWithFilesModel
                {
                    Following = Following,
                    IsPostOwner = isPostOwner,
                    CategoryId = post.CategoryId,
                    CategoryName = post.Category.Name,
                    Text = post.Text,
                    Title = post.Title,
                    DateCreated = post.DateCreated,
                    Files = post.Z_Harag_Photo.Select(p => p.Url).ToList(),
                    //Rate = post.Rate,
                    Len = (double)(post.Longtiude ?? 0),
                    Lat = (double)(post.Lattiude ?? 0),
                    userId = post.CustomerId,
                    CityName = post.City.ArName,
                    Contact = post.Contact,
                    CityId = (int)post.CityId,
                    Id = post.Id,
                    IsUserFeatured = post.Customer.IsFeatured,
                    DateUpdated = post.DateUpdated,
                    IsDispayed = post.IsDispayed,
                    IsFeatured = (bool)post.IsFeatured,
                    IsCommentingClosed = post.IsCommentingClosed,
                    PostOwner = post.Customer.Username,
                    PostOwnerFullName = post.Customer.GetFullName(),
                    RelatedPosts = relatedPosts.Select(m => new Models.Harag.Post.PostModel
                    {
                        Photo = m.Z_Harag_Photo.FirstOrDefault() == null ? "" : m.Z_Harag_Photo.FirstOrDefault().Url,
                        Id = m.Id
                    }).ToList(),

                    SameCityPosts = sameCityPosts.Select(m => new Models.Harag.Post.PostModel
                    {
                        Photo = m.Z_Harag_Photo.FirstOrDefault() == null ? "" : m.Z_Harag_Photo.FirstOrDefault().Url,
                        Id = m.Id
                    }).ToList(),
                    UserRating = userRating
                };
                 
            }
            else
            {
                return NotFound();
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
        public IActionResult GetHaragMobileNavbar()
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
                return PartialView("~/Themes/Pavilion/Views/Harag/Shared/_NavbarMobile.cshtml", model);
            }
            return PartialView("~/Themes/Pavilion/Views/Harag/Shared/_NavbarMobile.cshtml", model);
        }

        [HttpGet]
        public IActionResult GetAllFeaturedPosts(int postId, int pageNo = 0)
        {
            PagingParams.PageNumber = pageNo;

            var posts = _postService.SearchByCategoryPage(postId, PagingParams);

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
                // Rate = p.Rate,
                DateUpdated = p.DateUpdated,
                IsDispayed = p.IsDispayed,
                IsFeatured = (bool)p.IsFeatured,
                PostOwner = p.Customer.Username,
                PostOwnerFullName = p.Customer.GetFullName()
            }).ToList();

            if (pageNo == 0)
            {
                return View("~/Themes/Pavilion/Views/Harag/Post/RelatedPosts.cshtml", modelOutput);
            }

            return View("~/Themes/Pavilion/Views/Harag/posts/PostsAjax.cshtml", modelOutput);
        }

        [HttpGet]
        public IActionResult GetLeatestPosts()
        {
            var posts = _postService.GetLatestPosts(PagingParams);

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
                DateUpdated = p.DateUpdated,
                IsFeatured = (bool)p.IsFeatured,
                IsDispayed = p.IsDispayed,
                PostOwner = p.Customer.Username,
                PostOwnerFullName = p.Customer.GetFullName()
            }).ToList();

            return View("~/Themes/Pavilion/Views/Harag/Post/PostsAjax.cshtml", modelOutput);
        }


        [HttpGet]
        public IActionResult GetAllHaragPostsAjax(int pageNo = 0)
        {
            PagingParams.PageNumber = pageNo;
            var posts = _postService.GetFeaturedPosts(PagingParams);

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
                // Rate = p.Rate,
                IsFeatured = (bool)p.IsFeatured,
                DateUpdated = p.DateUpdated,
                IsDispayed = p.IsDispayed,
                PostOwner = p.Customer.Username,
                PostOwnerFullName = p.Customer.GetFullName()
            }).ToList();

            return View("~/Themes/Pavilion/Views/Harag/Post/PostsAjax.cshtml", modelOutput);

        }

        [HttpGet]
        public IActionResult GetUserPostsByUserId(int userId, int pageNo = 0)
        {
            PagingParams.PageNumber = pageNo;
            var posts = _postService.GetCurrentUserPosts(userId, PagingParams);

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
                //Rate = p.Rate,
                IsFeatured = (bool)p.IsFeatured,
                DateUpdated = p.DateUpdated,
                IsDispayed = p.IsDispayed,
                PostOwner = p.Customer.Username,
                PostOwnerFullName = p.Customer.GetFullName()
            }).ToList();

            return View("~/Themes/Pavilion/Views/Harag/Post/PostsAjax.cshtml", modelOutput);

        }

        [HttpGet]
        public IActionResult GetHaragCityPosts(string city, int pageNo = 0)
        {
            PagingParams.PageNumber = pageNo;

            var cityO = _postService.GetCity(city);

            if (cityO == null)
                return NotFound();

            var posts = _postService.SearchByCity(cityO.Id, PagingParams);

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
                IsFeatured = (bool)p.IsFeatured,
                // Rate = p.Rate,
                DateUpdated = p.DateUpdated,
                IsDispayed = p.IsDispayed,
                PostOwner = p.Customer.Username,
                PostOwnerFullName = p.Customer.GetFullName()
            }).ToList();


            if (pageNo == 0)
            {
                return View("~/Themes/Pavilion/Views/Harag/Post/PostsForSearch.cshtml", modelOutput);
            }

            return View("~/Themes/Pavilion/Views/Harag/posts/PostsAjax.cshtml", modelOutput);
        }

        [HttpGet]
        public IActionResult GetHaragCatPosts(int catId, int pageNo = 0)
        {
            PagingParams.PageNumber = pageNo;

            var posts = _postService.SearchByCategory(catId, PagingParams);

            var modelOutput = new Models.Harag.Post.PostOutputModel();

            modelOutput.Items = posts.Select(p => new Models.Harag.Post.PostModel
            {
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                Text = p.Text,
                Id = p.Id,
                Title = p.Title,
                IsFeatured = (bool)p.IsFeatured,
                City = p.City.ArName,
                DateCreated = p.DateCreated,
                Photo = p.Z_Harag_Photo.Select(ppp => ppp.Url).FirstOrDefault(),
                // Rate = p.Rate,
                DateUpdated = p.DateUpdated,
                IsDispayed = p.IsDispayed,
                PostOwner = p.Customer.Username,
                PostOwnerFullName = p.Customer.GetFullName()
            }).ToList();


            if (pageNo == 0)
            {
                return View("~/Themes/Pavilion/Views/Harag/Post/PostsForSearch.cshtml", modelOutput);
            }

            return View("~/Themes/Pavilion/Views/Harag/posts/PostsAjax.cshtml", modelOutput);

        }

        [HttpGet]
        public IActionResult GetHaragFavoritesPosts(int pageNo = 0)
        {
            PagingParams.PageNumber = pageNo;
            if (!_workContext.CurrentCustomer.IsRegistered())
                return RedirectToRoute("Login", new { returnUrl = "Harag" });

            var user = _workContext.CurrentCustomer;

            var posts = _postService.GetFavoritesPosts(user.Id, PagingParams);

            var modelOutput = new Models.Harag.Post.PostOutputModel();

            modelOutput.Items = posts.Select(p => new Models.Harag.Post.PostModel
            {
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                Text = p.Text,
                Id = p.Id,
                IsFeatured = (bool)p.IsFeatured,
                Title = p.Title,
                City = p.City.ArName,
                DateCreated = p.DateCreated,
                Photo = p.Z_Harag_Photo.Select(ppp => ppp.Url).FirstOrDefault(),
                DateUpdated = p.DateUpdated,
                IsDispayed = p.IsDispayed,
                PostOwner = p.Customer.Username,
                PostOwnerFullName = p.Customer.GetFullName()
            }).ToList();

            return View("~/Themes/Pavilion/Views/Harag/Post/Posts.cshtml", modelOutput);

        }

        [HttpGet]
        public IActionResult GetHaragUserPosts(int userId, int pageNo = 0)
        {
            PagingParams.PageNumber = pageNo;


            var posts = _postService.GetCurrentUserPosts(userId, PagingParams);

            var modelOutput = new Models.Harag.Post.PostOutputModel();

            modelOutput.Items = posts.Select(p => new Models.Harag.Post.PostModel
            {
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                Text = p.Text,
                Id = p.Id,
                Title = p.Title,
                IsFeatured = (bool)p.IsFeatured,
                City = p.City.ArName,
                DateCreated = p.DateCreated,
                Photo = p.Z_Harag_Photo.Select(ppp => ppp.Url).FirstOrDefault(),
                //Rate = p.Rate,
                DateUpdated = p.DateUpdated,
                IsDispayed = p.IsDispayed,
                PostOwner = p.Customer.Username,
                PostOwnerFullName = p.Customer.GetFullName()
            }).ToList();



            if (pageNo == 0)
            {
                return View("~/Themes/Pavilion/Views/Harag/Post/PostsForSearch.cshtml", modelOutput);
            }

            return View("~/Themes/Pavilion/Views/Harag/posts/PostsAjax.cshtml", modelOutput);
        }

        [HttpGet]
        public IActionResult GetNextPostsAjax(int pageId, int type, int pageNo = 0)
        {
            PagingParams.PageNumber = pageNo;
            if (!_workContext.CurrentCustomer.IsRegistered())
                return RedirectToRoute("Login", new { returnUrl = "Harag" });

            var posts = _postService.GetFeaturedPosts(PagingParams);

            var modelOutput = new Models.Harag.Post.PostOutputModel();

            if (posts.Count <= 0)
            {
                modelOutput.Items = posts.Select(p => new Models.Harag.Post.PostModel
                {
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    Text = p.Text,
                    IsFeatured = (bool)p.IsFeatured,
                    Title = p.Title,
                    DateCreated = p.DateCreated,
                    Photo = p.Z_Harag_Photo.Select(ppp => ppp.Url).FirstOrDefault(),
                    // Rate = p.Rate,
                    DateUpdated = p.DateUpdated,
                    IsDispayed = p.IsDispayed,
                    PostOwner = p.Customer.Username,
                    PostOwnerFullName = p.Customer.GetFullName()
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
        public IActionResult GetHaragNeighborhoods(int cityId)
        {
            var cities = _postService.GetNeighborhoods(cityId);
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
                return RedirectToRoute("Login", new { returnUrl = "Harag/ReportPost?postid=" + postId });

            var post = _postService.IsExists(postId);
            if (!post)
                return NotFound();

            var model = new PostReport { PostId = postId };

            ViewBag.ReportAdded = false;

            return View("~/Themes/Pavilion/Views/Harag/Report/AddReport.cshtml", model);
        }
        [HttpPost]
        public IActionResult ReportPostAjax(PostReport model)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            var report = new Z_Harag_Reports
            {
                ReportDescription = model.ReportMessage,
                PostId = model.PostId,
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

            if (isAddedToFav)
            {
                var result = _postService.RemovePostFromFavorite(postId, _workContext.CurrentCustomer.Id);
                return Ok(new { stat = result, ok = 0 });
            }
            else
            {
                var res = _postService.AddPostToFavorite(postId, _workContext.CurrentCustomer.Id);
                return Ok(new { stat = res, ok = 1 });
            }
        }

        [HttpGet]
        public IActionResult DeletePost(int postId)
        {

            var post = _postService.IsExists(postId);

            if (!post)
                return NotFound();

            var obj = new DeletePost
            {
                PostId = postId
            };

            return View("~/Themes/Pavilion/Views/Harag/Post/DeletePost.cshtml", obj);

        }


        [HttpPost]
        public IActionResult DeletePost(DeletePost deleteData)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return RedirectToRoute("Login", new { returnUrl = "Harag/post/" + deleteData.PostId });

            var post = _postService.IsExists(deleteData.PostId);
            if (!post)
                return NotFound();

            var deleted = _postService.DeletePost(deleteData);

            if (deleted)
            {
                return View("~/Themes/Pavilion/Views/Harag/Post/_PostAddedSuccessfully.cshtml");
            }
            return View("~/Themes/Pavilion/Views/Harag/Post/_PostAddedSuccessfully.cshtml");
        }

        [HttpGet]
        public IActionResult HaragSearch(string Term, int pageNo = 0)
        {
            PagingParams.PageNumber = pageNo;
            SearchModel SearchModel = new SearchModel() { Term = Term };

            var model = _postService.SearchPosts(SearchModel, PagingParams);

            var outputModel = new PostOutputModel
            {
                Items = model.Select(m => new Models.Harag.Post.PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    PostOwnerFullName = m.Customer.GetFullName(),
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsFeatured = (bool)m.IsFeatured,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsCommentingClosed,
                    IsDispayed = m.IsDispayed,
                    City = m.City.ArName,
                    CategoryName = m.Category.Name,
                    Photo = m.Z_Harag_Photo?.FirstOrDefault()?.Url
                }).ToList(),
            };
            if (pageNo == 0)
            {
                return View("~/Themes/Pavilion/Views/Harag/Search/searchPage.cshtml", outputModel);

            }

            return View("~/Themes/Pavilion/Views/Harag/posts/PostsAjax.cshtml", outputModel);

        }

        [HttpGet]
        public IActionResult HaragSearchCatCity(int Cat, int City, int pageNo = 0)
        {
            PagingParams.PageNumber = pageNo;

            var model = _postService.SearchPostsCatCity(Cat, City, PagingParams);

            var outputModel = new PostOutputModel
            {
                Items = model.Select(m => new Models.Harag.Post.PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    PostOwner = m.Customer.Username,
                    PostOwnerFullName = m.Customer.GetFullName(),
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsCommentingClosed,
                    IsFeatured = (bool)m.IsFeatured,
                    IsDispayed = m.IsDispayed,
                    City = m.City.ArName,
                    CategoryName = m.Category.Name,
                    Photo = m.Z_Harag_Photo?.FirstOrDefault()?.Url
                }).ToList(),
            };

            return View("~/Themes/Pavilion/Views/Harag/Post/PostsAjax.cshtml", outputModel);
        }

        #endregion

    }
}
