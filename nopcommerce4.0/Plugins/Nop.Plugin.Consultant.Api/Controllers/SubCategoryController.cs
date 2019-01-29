//using Microsoft.AspNetCore.Mvc;
//using Nop.Core.Domain.Z_Consultant;
//using Nop.Plugin.Consultant.Api.Extensions;
//using Nop.Plugin.Consultant.Api.Models.SubCategory;
//using Nop.Services.Z_Consultant.Helpers;
//using Nop.Services.Z_Consultant.SubCategory;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Nop.Plugin.Consultant.Api.Controllers
//{
//    public class SubCategoryController : Controller
//    {

//        private readonly ISubCategoryService _subCategoryService;
//        private readonly IUrlHelper _urlHelper;

//        public SubCategoryController(ISubCategoryService subCategoryService, IUrlHelper urlHelper)
//        {
//            this._subCategoryService = subCategoryService;
//            this._urlHelper = urlHelper;
//        }


//        private List<LinkInfo> GetLinks(PagedList<Z_Consultant_SubCategory> list, string routName)
//        {
//            var links = new List<LinkInfo>();

//            if (list.HasPreviousPage)
//                links.Add(CreateLink(routName, list.PreviousPageNumber,
//                           list.PageSize, "previousPage", "GET"));

//            links.Add(CreateLink(routName, list.PageNumber,
//                           list.PageSize, "self", "GET"));

//            if (list.HasNextPage)
//                links.Add(CreateLink(routName, list.NextPageNumber,
//                           list.PageSize, "nextPage", "GET"));

//            return links;
//        }

//        private LinkInfo CreateLink(
//            string routeName, int pageNumber, int pageSize,
//            string rel, string method)
//        {
//            return new LinkInfo
//            {
//                Href = _urlHelper.Link(routeName,
//                            new { PageNumber = pageNumber, PageSize = pageSize }),
//                Rel = rel,
//                Method = method
//            };
//        }



//        [HttpGet]
//        public IActionResult GetSubCategories(PagingParams pagingParams)
//        {
//            var model = _subCategoryService.GetSubCategories(pagingParams);

//            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

//            var outputModel = new SubCategoryOutputModel
//            {
//                Paging = model.GetHeader(),
//                Links = GetLinks(model, "Consultant.Api.SubCategory.GetSubCategories"),
//                Items = model.List.Select(m => new SubCategoryModel()
//                {
//                    Id = m.Id,
//                    Name = m.Name
//                }).ToList(),
//            };
//            return Ok(outputModel);
//        }


//        [HttpGet]
//        public IActionResult GetSubCategory(int subCategoryId)
//        {
//            var subCategory = _subCategoryService.GetSubCategory(subCategoryId);
//            var subCategoryToReturn = subCategory.ToSubCategoryModel();
//            return Ok(subCategoryToReturn);
//        }



//        [HttpGet]
//        public IActionResult GetSubCategoriesByCategoryId(PagingParams pagingParams, int categoryId)
//        {
//            var model = _subCategoryService.GetSubCategoriesByCategoryId(pagingParams, categoryId);

//            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

//            var outputModel = new SubCategoryOutputModel
//            {
//                Paging = model.GetHeader(),
//                Links = GetLinks(model, "Consultant.Api.SubCategory.GetSubCategoriesByCategoryId"),
//                Items = model.List.Select(m => new SubCategoryModel()
//                {
//                    Id = m.Id,
//                    Name = m.Name
//                }).ToList(),
//            };
//            return Ok(outputModel);
//        }


//    }
//}
