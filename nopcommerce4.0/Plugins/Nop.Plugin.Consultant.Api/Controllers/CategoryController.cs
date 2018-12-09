using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Z_Consultant;
using Nop.Plugin.Consultant.Api.Extensions;
using Nop.Plugin.Consultant.Api.Models.Category;
using Nop.Plugin.Consultant.Api.Models.SubCategory;
using Nop.Services.Z_Consultant.Category;
using Nop.Services.Z_Consultant.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Consultant.Api.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IUrlHelper _urlHelper;

        public CategoryController(ICategoryService categoryService, IUrlHelper urlHelper)
        {
            this._categoryService = categoryService;
            this._urlHelper = urlHelper;
        }


        private List<LinkInfo> GetLinks(PagedList<Z_Consultant_Category> list, string routName)
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
        public IActionResult GetCategory(int CategoryId)
        {
            var model = _categoryService.GetCategoryById(CategoryId);
            var categoryToReturn = model.ToCategoryModel();
            return Ok(categoryToReturn);
        }




        [HttpGet]
        public IActionResult GetCategories(PagingParams pagingParams)
        {

            var model = _categoryService.GetCategories(pagingParams);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new CategoryOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Category.GetCategories"),
                Items = model.List.Select(m => new CategoryModel()
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToList(),
            };
            return Ok(outputModel);
        }


        

        [HttpGet]
        public IActionResult GetCategoryWithSubCategories(int CategoryId)
        {
            var categoryWithSubCategories = _categoryService.GetCategoryWithSubCategories(CategoryId);
            var categoryWithSubCategoriesToReturn = categoryWithSubCategories.ToCategoryWithSubCategoriesModel();
            return Ok(categoryWithSubCategoriesToReturn);
        }


        [HttpGet]
        public IActionResult GetCategoriesWithSubCategories(PagingParams pagingParams)
        {

            var model = _categoryService.GetCategoriesWithSubCategories(pagingParams);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new CategoryWithSubCategoriesOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Api.Category.GetCategoriesWithSubCategories"),
                Items = model.List.Select(m => new CategoryWithSubCategoriesModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    SubCategories = m.SubCategories.Select(s => new SubCategoryModel()
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToList()
                }).ToList(),
            };
            return Ok(outputModel);
        }


    }
}
