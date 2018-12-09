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




namespace Nop.Plugin.Consultant.Api.Controllers.Areas.Admin
{
    public class AdminPostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IUrlHelper _urlHelper;
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;


        public AdminPostController(IPostService postService,
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





        [HttpGet]
        public IActionResult GetAllPosts(PagingParams pagingParams)
        {


            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            var model = _postService.GetPosts(pagingParams);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetAllPosts"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
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
        public IActionResult GetAllPostsByCategoryId(PagingParams pagingParams, int CategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();



            var model = _postService.GetPostsByCategoryId(pagingParams, CategoryId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetAllPostsByCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
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
        public IActionResult GetAllPostsBySubCategoryId(PagingParams pagingParams, int SubCategoryId)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            var model = _postService.GetPostsBySubCategoryId(pagingParams, SubCategoryId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetAllPostsBySubCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
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
        public IActionResult GetAllClosedPosts(PagingParams pagingParams)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            var model = _postService.GetClosedPostsAdmin(pagingParams);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetAllClosedPosts"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    CategoryId = m.CategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    SubCategoryId = m.SubCategoryId,
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
        public IActionResult GetAllClosedPostsByCategoryId(PagingParams pagingParams, int CategoryId)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            var model = _postService.GetClosedPostsByCategoryIdAdmin(pagingParams, CategoryId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetAllClosedPostsByCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
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
        public IActionResult GetAllClosedPostsBySubCategoryId(PagingParams pagingParams, int SubCategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            var model = _postService.GetClosedPostsBySubCategoryIdAdmin(pagingParams, SubCategoryId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetAllClosedPostsBySubCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
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
        public IActionResult GetClosedDisplayedPosts(PagingParams pagingParams)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            var model = _postService.GetClosedDisplayedPosts(pagingParams);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetClosedDisplayedPosts"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    CategoryId = m.CategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    SubCategoryId = m.SubCategoryId,
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
        public IActionResult GetClosedDisplayedPostsByCategoryId(PagingParams pagingParams, int CategoryId)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            var model = _postService.GetClosedDisplayedPostsByCategoryId(pagingParams, CategoryId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetClosedDisplayedPostsByCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
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
        public IActionResult GetClosedDisplayedPostsBySubCategoryId(PagingParams pagingParams, int SubCategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            var model = _postService.GetClosedDisplayedPostsBySubCategoryId(pagingParams, SubCategoryId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetClosedDisplayedPostsBySubCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
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
        public IActionResult GetClosedHiddenPosts(PagingParams pagingParams)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            var model = _postService.GetClosedHiddenPosts(pagingParams);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetClosedHiddenPosts"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    CategoryId = m.CategoryId,
                    IsSetToSubCategory = m.IsSetToSubCategory,
                    PostOwner = m.Customer.Username,
                    SubCategoryId = m.SubCategoryId,
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
        public IActionResult GetClosedHiddenPostsByCategoryId(PagingParams pagingParams, int CategoryId)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            var model = _postService.GetClosedHiddenPostsByCategoryId(pagingParams, CategoryId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetClosedHiddenPostsByCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
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
        public IActionResult GetClosedHiddenPostsBySubCategoryId(PagingParams pagingParams, int SubCategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            var model = _postService.GetClosedHiddenPostsBySubCategoryId(pagingParams, SubCategoryId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetClosedHiddenPostsBySubCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
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
        public IActionResult GetAllOpenPosts(PagingParams pagingParams)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            var model = _postService.GetOpenPosts(pagingParams);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetAllOpenPosts"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
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
        public IActionResult GetAllOpenPostsByCategoryId(PagingParams pagingParams, int CategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            var model = _postService.GetOpenPostsByCategoryId(pagingParams, CategoryId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetAllOpenPostsByCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
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
        public IActionResult GetAllOpenPostsBySubCategoryId(PagingParams pagingParams, int id)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            var model = _postService.GetOpenPostsBySubCategoryId(pagingParams, id);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetAllOpenPostsBySubCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
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
        public IActionResult GetAllOpenPostsNotAns(PagingParams pagingParams)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            
            var model = _postService.GetOpenPostsNotAns(pagingParams);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetAllOpenPostsNotAns"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
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

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            
            var model = _postService.GetOpenPostsNotAnsByCategoryId(pagingParams, CategoryId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetAllOpenPostsNotAnsByCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
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
        public IActionResult GetAllOpenPostsNotAnsBySubCategoryId(PagingParams pagingParams, int id)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            var model = _postService.GetOpenPostsNotAnsBySubCategoryId(pagingParams, id);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetAllOpenPostsNotAnsBySubCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
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
        public IActionResult GetAllOpenPostsAns(PagingParams pagingParams)
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            var model = _postService.GetOpenPostsAns(pagingParams);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetAllOpenPostsAns"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
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
        public IActionResult GetAllOpenPostsAnsByCategoryId(PagingParams pagingParams, int CategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            var model = _postService.GetOpenPostsAnsByCategoryId(pagingParams, CategoryId);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetAllOpenPostsAnsByCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
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
        public IActionResult GetAllOpenPostsAnsBySubCategoryId(PagingParams pagingParams, int id)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            var model = _postService.GetOpenPostsAnsBySubCategoryId(pagingParams, id);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new PostOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Admin.AdminPost.GetAllOpenPostsAnsBySubCategoryId"),
                Items = model.List.Select(m => new PostModel()
                {
                    Id = m.Id,
                    Text = m.Text,
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
        public IActionResult GetPost(int PostId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            PostWithFilesModel model = new PostWithFilesModel();

            if (!_postService.IsExist(PostId))
                return NotFound();
            
            model = GetPostWithPhotos(PostId);
          

            return Ok(model);
        }


        
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
                String pathReplaced = Path.Combine(Request.Host.Host + ":" + Request.Host.Port, "Consultant\\api\\Admin\\Images\\" + photo.Url)
                    .Replace("\\", "/");
                files.Add(pathReplaced);
            }
            var modelToReturn = model.ToPostWithFilesModel();
            modelToReturn.CategoryName = model.Category.Name;
            modelToReturn.SubCategoryName = model.SubCategory?.Name;
            modelToReturn.Files = files;
            return modelToReturn;
        }


        [HttpPost]
        public IActionResult DisplayPost(DisplayingPostModel displayingPostModel)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            if (!_postService.IsExist(displayingPostModel.PostId))
                return BadRequest("Post is not exist");

            if (!_postService.IsClosed(displayingPostModel.PostId))
                return BadRequest("Post is not closed");

            _postService.DisplayPost(displayingPostModel.PostId);

            return Ok();
        }

        [HttpPost]
        public IActionResult HidePost(DisplayingPostModel displayingPostModel)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            if (!_postService.IsExist(displayingPostModel.PostId))
                return BadRequest("Post is not exist");

            _postService.HidePost(displayingPostModel.PostId);

            return Ok();
        }


        [HttpPost]
        public IActionResult UnReservePost(ReservePostModel reservePost)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();
            if (!_postService.IsExist(reservePost.PostId))
                return BadRequest("Post not found");
            if (!_postService.IsReserved(reservePost.PostId))
                return BadRequest("Post already is not Reserved");

            _postService.UnReservePost(reservePost.PostId);
            return Ok();
        }




    }
}
