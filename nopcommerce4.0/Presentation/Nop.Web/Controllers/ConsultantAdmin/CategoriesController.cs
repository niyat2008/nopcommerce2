using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Z_ConsultantAdmin.Categories;
using Microsoft.AspNetCore.Hosting;
using Nop.Core.Domain.Customers;
using Nop.Services.Z_Consultant.Helpers;
using Nop.Web.Models.ConsultantAdmin.Categories;
using Nop.Services.Z_ConsultantAdmin.SubCategories;
using Nop.Services.Z_ConsultantAdmin;

namespace Nop.Web.Controllers.ConsultantAdmin
{
    public class CategoriesController : Controller
    {
        #region  Fields
        private readonly ICategoryService _category;
        private readonly ISubCategoryService _subCategory;
        private readonly IPostService _post;
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;
        #endregion

        #region Ctor
        public CategoriesController(ICategoryService category, Core.IWorkContext workContext, IHostingEnvironment env, ISubCategoryService subCategory, IPostService post)
        {
            this._category = category;
            this._workContext = workContext;
            this._env = env;
            this._subCategory = subCategory;
            this._post = post;

        }
        #endregion


        #region Methods
        //Get Categories
        public IActionResult GetCategories()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) &&!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.ConsultationAdmin, true))
                return Forbid();

            return View("~/Themes/Pavilion/Views/ConsultantAdmin/Categories/GetCategories.cshtml");
        }
        //Get categories In Json
        [HttpPost]
        public ActionResult GetCategoriesInJson()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) &&!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.ConsultationAdmin, true))
                return Forbid();

            //Server Side Parameters
            int start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());

            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];





            var categoryInDb = _category.GetAllCategories(start, length, searchValue, sortColumnName, sortDirection);

            var category = new CategoriesOutputModel
            {
                Categories = categoryInDb.Select(m => new CategoriesModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    IsActive=m.IsActive
                }).ToList()
            };

            return Json(new { data = category.Categories });
        }
        //Add Category 
        public ActionResult AddCategory()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) &&!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.ConsultationAdmin, true))
                return Forbid();
            
            return View("~/Themes/Pavilion/Views/ConsultantAdmin/Categories/AddCategory.cshtml");
        }
        //Add Category Ajax
        [HttpPost]
        public ActionResult AddCategoryAjax([FromBody] CategoryModelForPost categoryModel)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) &&!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.ConsultationAdmin, true))
                return Forbid();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = _category.AddCategory(categoryModel);
            if (category == null)
                return Json(new { result = false });

            return Json(new { result = true });

        }



        //Add Category 
        public ActionResult UpdateCategory(int catId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();
             
            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.ConsultationAdmin, true))
                return Forbid();

            var cat = _category.GetCategory(catId);

            var model = new CategoryModelForPost()
            {
                Id = cat.Id,
                Name = cat.Name,
                Description = cat.Description ,
                IsActive=cat.IsActive
            };

            return View("~/Themes/Pavilion/Views/ConsultantAdmin/Categories/UpdateCategory.cshtml", model);
        }
        //Add Category Ajax
        [HttpPost]
        public ActionResult UpdateCategoryAjax([FromBody] CategoryModelForPost categoryModel)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.ConsultationAdmin, true))
                return Forbid();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = _category.UpdateCategory(categoryModel);
            if (category == null)
                return Json(new { result = false });

            return Json(new { result = true });

        }

        //Delete Category
        [HttpDelete]
        public ActionResult DeleteCategory(int categoryId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) &&!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.ConsultationAdmin, true))
                return Forbid();

            if (categoryId == null || categoryId == 0)
                return NotFound();

            _category.DeleteCategory(categoryId);

            return Json(new { result = true });

        }
        #endregion

    }
}