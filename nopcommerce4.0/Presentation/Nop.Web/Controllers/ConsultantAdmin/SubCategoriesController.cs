using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Z_ConsultantAdmin.SubCategories;
using Microsoft.AspNetCore.Hosting;
using Nop.Core.Domain.Customers;
using Nop.Services.Z_Consultant.Helpers;
using Nop.Web.Models.ConsultantAdmin.Subcategories;
using Nop.Services.Z_Consultant.Category;

namespace Nop.Web.Controllers.ConsultantAdmin
{
    public class SubCategoriesController : Controller
    {
        #region  Fields
        private readonly ICategoryService _category;
        private readonly ISubCategoryService _subCategory;
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;
        #endregion

        #region Ctor
        public SubCategoriesController(ISubCategoryService subCategory, ICategoryService category, Core.IWorkContext workContext, IHostingEnvironment env)
        {
            this._category = category;
            this._subCategory = subCategory;
            this._workContext = workContext;
            this._env = env;

        }
        #endregion


        #region Methods
        //Get SubCategories
        public IActionResult GetSubCategories()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            return View("~/Themes/Pavilion/Views/ConsultantAdmin/Subcategories/GetSubCategories.cshtml");
        }
        //Get SubCategories In Json
        [HttpPost]
        public ActionResult GetSubCategoriesInJson()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            //Server Side Parameters
            int start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());

            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];





            var subCategoryInDb = _subCategory.GetSubCategories(start, length, searchValue, sortColumnName, sortDirection);

            var subCategory = new SubCategoryOutputModel
            {
                SubCategories = subCategoryInDb.Select(m => new SubCategoryModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    CategoryId=m.CategoryId,
                    CategoryName=m.Category?.Name
                }).ToList()
            };

            return Json(new { data = subCategory.SubCategories });
        }
        //Get SubCategories By CategoryId
        public ActionResult GetSubCategoriesByCategoryId(int categoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            ViewBag.categoryId = categoryId;

            return View("~/Themes/Pavilion/Views/ConsultantAdmin/Subcategories/GetSubCategoriesByCategory.cshtml");
        }
        //Get SubCategories By CategoryId In Json
        [HttpPost]
        public ActionResult GetSubCategoriesByCategoryIdInJson(int categoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            if (categoryId == null || categoryId == 0)
                return NotFound();

            //Server Side Parameters
            int start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());

            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];

           var subcategoryInDb= _subCategory.GetSubCategoriesByCategoryId(categoryId, start, length, searchValue, sortColumnName, sortDirection);

            var subCategory = new SubCategoryOutputModel
            {
                SubCategories = subcategoryInDb.Select(m => new SubCategoryModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    CategoryId = m.CategoryId,
                    CategoryName = m.Category?.Name
                }).ToList()
            };

            return Json(new { data = subCategory.SubCategories });
        }
        //Add SubCategory
        public ActionResult AddSubCategory()
        {
            var allCategories = _subCategory.GetAllCategor();
            var subCategoryViewModel = new SubCategoryForPost
            {
                Categories=allCategories
            };
            
            return View("~/Themes/Pavilion/Views/ConsultantAdmin/Subcategories/AddSubCategory.cshtml", subCategoryViewModel);
        }
        //Add SubCategory Ajax
        public ActionResult AddSubCategoryAjax([FromBody]SubCategoryForPost subCategoryModel)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var subCategory = _subCategory.AddSubCategory(subCategoryModel);

            if (subCategory == null)
                return Json(new { result = false });

            return Json(new { result = true });
        }
        //Delete Category
        [HttpDelete]
        public ActionResult DeleteSubCategory(int subCategoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            if (subCategoryId == null || subCategoryId == 0)
                return NotFound();

            _subCategory.DeleteSubCategory(subCategoryId);

            return Json(new { result = true });

        }
        #endregion
    }
}