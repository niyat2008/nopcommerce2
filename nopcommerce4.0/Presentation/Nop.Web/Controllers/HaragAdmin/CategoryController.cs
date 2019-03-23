using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Z_HaragAdmin.Categories;
using Nop.Services.Events;
using Nop.Core.Domain.Customers;
using Nop.Services.Z_Harag.Helpers;
using Nop.Web.Models.HaragAdmin.Category;

namespace Nop.Web.Controllers.HaragAdmin
{
    public class CategoryController : Controller
    {
          #region Fields
            private readonly ICategoryService _categoryService;
            private readonly Core.IWorkContext _workContext;
            private readonly IEventPublisher _env;
        #endregion


        #region Ctor
        public CategoryController(ICategoryService categoryService, Core.IWorkContext workContext, IEventPublisher env)
        {
            this._categoryService = categoryService;
            this._workContext = workContext;
            this._env = env;
        }
        #endregion
        #region Actions

        //Add Category
        public IActionResult AddHaragCategory(int id=0)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            var category = new PostCategoryModel();

            if (id !=0)
            {

                var categoryInDb = _categoryService.GetCategoryById(id);

                if(categoryInDb==null)
                     return View("~/Themes/Pavilion/Views/HaragAdmin/Category/AddCategory.cshtml",category);

                //var category = new PostCategoryModel
                //{
                category.Id = categoryInDb.Id;
                category.Name = categoryInDb.Name;
                category.Description = categoryInDb.Description;
                category.IsActive = categoryInDb.IsActive;
                //};

                return View("~/Themes/Pavilion/Views/HaragAdmin/Category/AddCategory.cshtml", category);
            }



            return View("~/Themes/Pavilion/Views/HaragAdmin/Category/AddCategory.cshtml", category);
        }
        //Add Category Ajax
        [HttpPost]
        public IActionResult AddHaragCategoryAjax([FromBody]PostCategoryModel category)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            if (!ModelState.IsValid)
                return View("~/Themes/Pavilion/Views/HaragAdmin/Category/AddCategory.cshtml", category);

            if(category.Id !=0 )
            {
                var response = _categoryService.UpdateCategory(category);
                return Json(new { data = response });
            }

            var result = _categoryService.AddCategory(category);

            return Json(new { data = result });
        }

        //Get All Categories
        public IActionResult GetHaragCategories()
        {
            return View("~/Themes/Pavilion/Views/HaragAdmin/Category/GetCategories.cshtml");
        }
        //Get All Categories Ajax
        [HttpPost]
        public IActionResult GetHaragCategoriesAjax()
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            //Server Side Parameters
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];

            var categoriesInDb = _categoryService.GetCategories(start, length, searchValue, sortColumnName, sortDirection);

            var categories = new CategoryOutputModel
            {
                Items = categoriesInDb.Select(c => new CategoryModel
                {
                    Id=c.Id,
                    Name = c.Name,
                    Description=c.Description,
                    IsActive=c.IsActive
                }).ToList()
            };


            return Json(new {data= categories.Items });
        }
        #endregion


    }
}