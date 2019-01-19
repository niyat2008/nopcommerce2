using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Z_Consultant;
using Nop.Plugin.Consultant.Api.Extensions;
using Nop.Plugin.Consultant.Api.Models.Post;
using Nop.Services.Z_Consultant.Helpers;
using Nop.Services.Z_Consultant.Post;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Consultant.Api.Controllers
{
    
    public class PostController : Controller
    {

        private readonly IPostService _postService;
        private readonly IUrlHelper _urlHelper;
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;


        public PostController(IPostService postService,
            IHostingEnvironment env,
            Core.IWorkContext workContext,
            IUrlHelper urlHelper)
        {
            this._postService = postService;
            this._workContext = workContext;
            this._urlHelper = urlHelper;
            this._env = env;
        }


        private List<LinkInfo> GetLinks(PagedList<Z_Consultant_Post> list, string routName)
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



        #region public customer and anonymous

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Search([FromBody] SearchModel SearchModel, PagingParams pagingParams)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var model = _postService.SearchPosts(pagingParams,SearchModel);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.Search"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title=m.Title,
                    CategoryId = m.CategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    SubCategoryId = m.SubCategoryId,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    Rate = m.Rate,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllClosedPosts(PagingParams pagingParams)
        {
            var model = _postService.GetClosedPosts(pagingParams);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllClosedPosts"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    SubCategoryId = m.SubCategoryId,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    IsDispayed=m.IsDispayed,
                    IsReserved=m.IsReserved,
                    Rate=m.Rate,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);

        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllClosedPostsByCategoryId(PagingParams pagingParams, int CategoryId)
        {
            var model = _postService.GetClosedPostsByCategoryId(pagingParams, CategoryId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllClosedPostsByCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAllClosedPostsBySubCategoryId(PagingParams pagingParams, int SubCategoryId)
        {

            var model = _postService.GetClosedPostsBySubCategoryId(pagingParams, SubCategoryId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllClosedPostsBySubCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }


        #endregion



        #region private customers



        [HttpGet]
        public IActionResult GetAllOpenPostsForCustomer(PagingParams pagingParams)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();
            

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                return Forbid();


            var currentUserId = _workContext.CurrentCustomer.Id;


            var model = _postService.GetOpenPostsForCustomer(pagingParams, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllOpenPostsForCustomer"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }


        [HttpGet]
        public IActionResult GetAllOpenPostsForCustomerByCategoryId(PagingParams pagingParams, int CategoryId)
        {


            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                return Forbid();


            var currentUserId = _workContext.CurrentCustomer.Id;


            var model = _postService.GetOpenPostsForCustomerByCategoryId(pagingParams, CategoryId, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllOpenPostsForCustomerByCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);

        }


        [HttpGet]
        public IActionResult GetAllOpenPostsForCustomerBySubCategoryId(PagingParams pagingParams, int SubCategoryId)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                return Forbid();


            var currentUserId = _workContext.CurrentCustomer.Id;

            var model = _postService.GetOpenPostsForCustomerBySubCategoryId(pagingParams, SubCategoryId, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllOpenPostsForCustomerBySubCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }


        [HttpGet]
        public IActionResult GetAllClosedPostsForCustomer(PagingParams pagingParams)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                return Forbid();


            var currentUserId = _workContext.CurrentCustomer.Id;

            var model = _postService.GetClosedPostsForCustomer(pagingParams, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllClosedPostsForCustomer"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }



        [HttpGet]
        public IActionResult GetAllClosedPostsForCustomerByCategoryId(PagingParams pagingParams, int CategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                return Forbid();


            var currentUserId = _workContext.CurrentCustomer.Id;


            var model = _postService.GetClosedPostsForCustomerByCategoryId(pagingParams, CategoryId, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllClosedPostsForCustomerByCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);

        }


        [HttpGet]
        public IActionResult GetAllClosedPostsForCustomerBySubCategoryId(PagingParams pagingParams, int SubCategoryId)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                return Forbid();


            var currentUserId = _workContext.CurrentCustomer.Id;


            var model = _postService.GetClosedPostsForCustomerBySubCategoryId(pagingParams, SubCategoryId, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllClosedPostsForCustomerBySubCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }


        [HttpGet]
        public IActionResult GetAllPostsForCustomer(PagingParams pagingParams)
        {


            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                return Forbid();


            var currentUserId = _workContext.CurrentCustomer.Id;

            var model = _postService.GetPostsForCustomer(pagingParams, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllPostsForCustomer"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }


        [HttpGet]
        public IActionResult GetAllPostsForCustomerByCategoryId(PagingParams pagingParams, int CategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                return Forbid();


            var currentUserId = _workContext.CurrentCustomer.Id;


            var model = _postService.GetPostsForCustomerByCategoryId(pagingParams, CategoryId, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllPostsForCustomerByCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);

        }


        [HttpGet]
        public IActionResult GetAllPostsForCustomerBySubCategoryId(PagingParams pagingParams, int SubCategoryId)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                return Forbid();


            var currentUserId = _workContext.CurrentCustomer.Id;

            var model = _postService.GetPostsForCustomerBySubCategoryId(pagingParams, SubCategoryId, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllPostsForCustomerBySubCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }





        [HttpPost]
        public  IActionResult AddPost([FromBody]PostForPostModel postForPostModel)
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

            var post =  _postService.AddNewPost(postForPostModel, currentUserId, postForPostModel.Files, errors);

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


            ;

            string userName = _workContext.CurrentCustomer.Username;

            var postToReturn = post.ToPostModel();
            postToReturn.PostOwner = userName;
            return CreatedAtRoute("Consultant.Api.Post.GetPost", new { PostId = postToReturn.Id }, postToReturn);
        }


        [HttpPost]
        public IActionResult CloseAndRatePost([FromBody]CloseAndRatePostModel closeAndRatePostModel)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                return Forbid();


            var currentUserId = _workContext.CurrentCustomer.Id;


            if (!_postService.IsCustomerAuthToPost(closeAndRatePostModel.Id, currentUserId))
                return Forbid();


            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (closeAndRatePostModel.Id <= 0)
                return BadRequest("Rating and closing must be belong to post");

            if (closeAndRatePostModel.IsColsed == null && closeAndRatePostModel.Rate == null)
                return BadRequest();

            if(!_postService.IsAnswered(closeAndRatePostModel.Id))
                return BadRequest("The post must be answered to Rating or closing it");

            var thereIsCahnges = _postService.CloseAndRatePost(closeAndRatePostModel, currentUserId);

            if (thereIsCahnges)
                return Ok();
            else
                return BadRequest();
        }


        [HttpPost]
        public IActionResult ClosePost([FromBody]ClosePostModel closePostModel)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                return Forbid();


            var currentUserId = _workContext.CurrentCustomer.Id;


            if (!_postService.IsCustomerAuthToPost(closePostModel.Id, currentUserId))
                return Forbid();


            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (closePostModel.Id <= 0)
                return BadRequest("Rating and closing must be belong to post");

            

            if (!_postService.IsAnswered(closePostModel.Id))
                return BadRequest("The post must be answered to  closing it");

            var thereIsCahnges = _postService.ClosePost(closePostModel, currentUserId);

            if (thereIsCahnges)
                return Ok();
            else
                return BadRequest();
        }


        [HttpPost]
        public IActionResult RatePost([FromBody]RatePostModel RatePostModel)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                return Forbid();


            var currentUserId = _workContext.CurrentCustomer.Id;


            if (!_postService.IsCustomerAuthToPost(RatePostModel.Id, currentUserId))
                return Forbid();


            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (RatePostModel.Id <= 0)
                return BadRequest("Rating and closing must be belong to post");


            if (!_postService.IsAnswered(RatePostModel.Id))
                return BadRequest("The post must be answered to  rating it");

            var thereIsCahnges = _postService.RatePost(RatePostModel, currentUserId);

            if (thereIsCahnges)
                return Ok();
            else
                return BadRequest();
        }
        #endregion



        #region public consultants
        [HttpGet]
        public IActionResult GetAllOpenPostsNotAns(PagingParams pagingParams)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();

            
            var model = _postService.GetOpenPostsNotAns(pagingParams);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllOpenPostsNotAns"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }


        [HttpGet]
        public IActionResult GetAllOpenPostsNotAnsByCategoryId(PagingParams pagingParams, int CategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();

            
            var model = _postService.GetOpenPostsNotAnsByCategoryId(pagingParams, CategoryId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllOpenPostsNotAnsByCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);

        }


        [HttpGet]
        public IActionResult GetAllOpenPostsNotAnsBySubCategoryId(PagingParams pagingParams, int SubCategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();


            var model = _postService.GetOpenPostsNotAnsBySubCategoryId(pagingParams, SubCategoryId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllOpenPostsNotAnsBySubCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }

        [HttpPost]
        public IActionResult ReservePost([FromBody] ReservePostModel reservePost)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_postService.IsExist(reservePost.PostId))
                return BadRequest("Post not found");
            if (_postService.IsReserved(reservePost.PostId))
                return Forbid("post already reserved");


            var curruntUserId = _workContext.CurrentCustomer.Id;
            _postService.ReservePost(reservePost.PostId, curruntUserId);
            return Ok();
        }



        #endregion



            #region private consultant

        [HttpGet]
        public IActionResult GetAllOpenPostsForConsultant(PagingParams pagingParams)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();

            var currentUserId = _workContext.CurrentCustomer.Id;


            var model = _postService.GetOpenPostsForConsultant(pagingParams, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllOpenPostsForConsultant"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }


        [HttpGet]
        public IActionResult GetAllOpenPostsForConsultantByCategoryId(PagingParams pagingParams, int CategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();

            var currentUserId = _workContext.CurrentCustomer.Id;


            var model = _postService.GetOpenPostsForConsultantByCategoryId(pagingParams, CategoryId, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllOpenPostsForConsultantByCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);

        }


        [HttpGet]
        public IActionResult GetAllOpenPostsForConsultantBySubCategoryId(PagingParams pagingParams, int SubCategoryId)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();

            var currentUserId = _workContext.CurrentCustomer.Id;

            var model = _postService.GetOpenPostsForConsultantBySubCategoryId(pagingParams, SubCategoryId, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllOpenPostsForConsultantBySubCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }


        [HttpGet]
        public IActionResult GetAllClosedPostsForConsultant(PagingParams pagingParams)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();

            var currentUserId = _workContext.CurrentCustomer.Id;


            var model = _postService.GetClosedPostsForConsultant(pagingParams, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllClosedPostsForConsultant"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }



        [HttpGet]
        public IActionResult GetAllClosedPostsForConsultantByCategoryId(PagingParams pagingParams, int CategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();

            var currentUserId = _workContext.CurrentCustomer.Id;


            var model = _postService.GetClosedPostsForConsultantByCategoryId(pagingParams, CategoryId, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllClosedPostsForConsultantByCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);

        }


        [HttpGet]
        public IActionResult GetAllClosedPostsForConsultantBySubCategoryId(PagingParams pagingParams, int SubCategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();

            var currentUserId = _workContext.CurrentCustomer.Id;


            var model = _postService.GetClosedPostsForConsultantBySubCategoryId(pagingParams, SubCategoryId, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllClosedPostsForConsultantBySubCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }



        [HttpGet]
        public IActionResult GetAllPostsForConsultant(PagingParams pagingParams)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();

            var currentUserId = _workContext.CurrentCustomer.Id;


            var model = _postService.GetPostsForConsultant(pagingParams, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllPostsForConsultant"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }



        [HttpGet]
        public IActionResult GetAllPostsForConsultantByCategoryId(PagingParams pagingParams, int CategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();

            var currentUserId = _workContext.CurrentCustomer.Id;


            var model = _postService.GetPostsForConsultantByCategoryId(pagingParams, CategoryId, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllPostsForConsultantByCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);

        }


        [HttpGet]
        public IActionResult GetAllPostsForConsultantBySubCategoryId(PagingParams pagingParams, int SubCategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();

            var currentUserId = _workContext.CurrentCustomer.Id;


            var model = _postService.GetPostsForConsultantBySubCategoryId(pagingParams, SubCategoryId, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllPostsForConsultantBySubCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }


        [HttpPost]
        [HttpGet]public IActionResult SetPostToSubCategory([FromBody] SetPostToSubCategoryModel model)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();

            var currentUserId = _workContext.CurrentCustomer.Id;



            if (_postService.IsConsultantAuthToPost(model.PostId, currentUserId))
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!_postService.IsExist(model.PostId))
                    return BadRequest("post not found");

                //if (_reop.IsSetedToSubCategory(model.PostId))
                //    return BadRequest("The post have been setted up to subcategory before that");

                if (!_postService.IsCategoryConatinTheSub(model))
                    return BadRequest("The SubCategory is not belong to the post category");

                _postService.SetPostToSubCategory(model);

                return Ok();
            }
            else
            {
                return Forbid();
            }

        }



        [HttpPost]
        public IActionResult SetPostToCategory([FromBody] SetPostToCategoryModel model)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();

            var currentUserId = _workContext.CurrentCustomer.Id;



            if (_postService.IsConsultantAuthToPost(model.PostId, currentUserId))
            {

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!_postService.IsExist(model.PostId))
                    return BadRequest("The post not found");


                _postService.SetPostToCategory(model);

                return Ok();
            }
            else
            {
                return Forbid();
            }

        }


        [HttpPost]
        public IActionResult SetPostToCategoryAndSubCategory([FromBody] SetPostToCategoryAndSubCategoryModel model)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();

            var currentUserId = _workContext.CurrentCustomer.Id;



            if (_postService.IsConsultantAuthToPost(model.PostId, currentUserId))
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!_postService.IsExist(model.PostId))
                    return BadRequest("post not found");

                //if (_reop.IsSetedToSubCategory(model.PostId))
                //    return BadRequest("The post have been setted up to subcategory before that");

                if (!_postService.IsCategoryConatinTheSub(model))
                    return BadRequest("The SubCategory is not belong to the category");

                _postService.SetPostToCategoryAndSubCategory(model);

                return Ok();
            }
            else
            {
                return Forbid();
            }

        }



        [HttpPost]
        public IActionResult UnReservePost([FromBody] ReservePostModel reservePost)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_postService.IsExist(reservePost.PostId))
                return BadRequest("Post not found");
            if (!_postService.IsReserved(reservePost.PostId))
                return BadRequest("Post not Reserved");

            var curruntUserId = _workContext.CurrentCustomer.Id;
            if (!_postService.IsConsultantAuthToPost(reservePost.PostId, curruntUserId))
                return Forbid();
            
            _postService.UnReservePost(reservePost.PostId);
            return Ok();
        }






        [HttpGet]
        public IActionResult GetAllReservedPostsForConsultant(PagingParams pagingParams)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();

            var currentUserId = _workContext.CurrentCustomer.Id;


            var model = _postService.GetReservedPostsForConsultant(pagingParams, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllReservedPostsForConsultant"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }


        [HttpGet]
        public IActionResult GetAllReservedPostsForConsultantByCategoryId(PagingParams pagingParams, int CategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();

            var currentUserId = _workContext.CurrentCustomer.Id;


            var model = _postService.GetReservedPostsForConsultantByCategoryId(pagingParams, CategoryId, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllReservedPostsForConsultantByCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);

        }


        [HttpGet]
        public IActionResult GetAllReservedPostsForConsultantBySubCategoryId(PagingParams pagingParams, int SubCategoryId)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                return Forbid();

            var currentUserId = _workContext.CurrentCustomer.Id;

            var model = _postService.GetReservedPostsForConsultantBySubCategoryId(pagingParams, SubCategoryId, currentUserId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Post.GetAllReservedPostsForConsultantBySubCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    Title = m.Title,
                    CategoryId = m.CategoryId,
                    SubCategoryId = m.SubCategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    IsAnswered = m.IsAnswered,
                    IsClosed = m.IsClosed,
                    Rate = m.Rate,
                    IsDispayed = m.IsDispayed,
                    IsReserved = m.IsReserved,
                    CategoryName = m.Category.Name,
                    SubCategoryName = m.SubCategory?.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }

        #endregion





        [HttpGet]
        public IActionResult GetPost(int PostId)
        {
            if (!_postService.IsExist(PostId))
                return NotFound();

            PostWithFilesModel model = new PostWithFilesModel();

            if (!_postService.IsClosed(PostId))
            {
                if (!_workContext.CurrentCustomer.IsRegistered())
                    return Unauthorized();

                var currentUserId = _workContext.CurrentCustomer.Id;


                if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                {
                    if (_postService.IsReserved(PostId))
                    {
                        if (_postService.IsConsultantAuthToPost(PostId, currentUserId))
                        {
                            model = GetPostWithPhotosPrivate(PostId);
                        }
                        else
                        {
                            return Forbid();
                        }
                    }
                    else
                    {
                        model = GetPostWithPhotosPrivate(PostId);
                    }

                }
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                {
                    if (_postService.IsCustomerAuthToPost(PostId, currentUserId))
                    {
                        model = GetPostWithPhotosPrivate(PostId);
                    }
                    else
                    {
                        return Forbid();
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                model = GetPostWithPhotos(PostId);
            }

            return Ok(model);
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult getFile(string path)
        {
            return File(Path.Combine(_env.WebRootPath, "/ConsultantApi/Uploads/Images/" + path), contentType: "image/*");
        }

        private PostWithFilesModel GetPostWithPhotos(int id)
        {
            Z_Consultant_Post model = null;
            model = _postService.GetPost(id);
            List<string> files = new List<string>();
            foreach (var photo in model.Photos)
            {
                String pathReplaced = Path.Combine(Request.Host.Host + ":" + Request.Host.Port, "\\Consultant\\api\\Images\\" + photo.Url)
                    .Replace("\\", "/");
                files.Add(pathReplaced);
            }
            var modelToReturn = model.ToPostWithFilesModel();
            modelToReturn.CategoryName = model.Category.Name;
            modelToReturn.SubCategoryName = model.SubCategory?.Name;
            modelToReturn.Files = files;
            return modelToReturn;
        }





        [HttpGet]
        public IActionResult getFilePrivate(string path)
        {
            return File(Path.Combine(_env.WebRootPath, "/ConsultantApi/Uploads/Images/" + path), contentType: "image/*");
        }

        private PostWithFilesModel GetPostWithPhotosPrivate(int id)
        {
            Z_Consultant_Post model = null;
            model = _postService.GetPost(id);
            List<string> files = new List<string>();
            foreach (var photo in model.Photos)
            {
                String pathReplaced = Path.Combine(Request.Host.Host + ":" + Request.Host.Port, "\\Consultant\\api\\Imgs\\" + photo.Url)
                    .Replace("\\", "/");
                files.Add(pathReplaced);
            }
            var modelToReturn = model.ToPostWithFilesModel();
            modelToReturn.CategoryName = model.Category.Name;
            modelToReturn.SubCategoryName = model.SubCategory?.Name;
            modelToReturn.Files = files;
            return modelToReturn;
        }



    }
}
