﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Z_Consultant;
using Nop.Services.Z_Consultant.Category;
using Nop.Services.Z_Consultant.Comment;
using Nop.Services.Z_Consultant.Helpers;
using Nop.Services.Z_Harag.Post;
using Nop.Services.Z_Consultant.SubCategory;
using Nop.Services.Z_HaragAdmin.Setting;
using Nop.Web.Extensions.Consultant;
using Nop.Web.Models.Consultant.Category;
using Nop.Web.Models.Consultant.Comment;
using Nop.Web.Models.Harag.Post;
using Nop.Web.Models.Consultant.SubCategory;
using Nop.Web.Models.Consultant.User;

namespace Nop.Web.Controllers.Harag
{
    public class HaragHomeController : BasePublicController
    {
        #region Fields




        private readonly ICategoryService _categoryService;
        private readonly IUrlHelper _urlHelper;
        private readonly IPostService _postService;
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;
        private readonly ICommentService _commentService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly ISettingService settingService;
        SettingsModel SettingsModel;
        #endregion

        #region ctor
        public HaragHomeController(
            ICategoryService categoryService,
            IUrlHelper urlHelper,
            IPostService postService,
            Core.IWorkContext workContext,
            IHostingEnvironment env,
            ICommentService commentService,
            ISubCategoryService subCategoryService,
            ISettingService settingService
            
            )
        {
            this._categoryService = categoryService;
            this._urlHelper = urlHelper;
            this._postService = postService;
            this._workContext = workContext;
            this._env = env;
            this._commentService = commentService;
            this._subCategoryService = subCategoryService;
            SettingsModel = settingService.GetSettings();
        }
        #endregion




        #region helpers

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




        #endregion



        #region methods


        public virtual IActionResult Home()
        {
            if (_workContext.CurrentCustomer.IsRegistered())
            {
                if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                    ViewBag.UserRole = RolesType.Administrators;
                //else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                //    ViewBag.UserRole = RolesType.Consultant;
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                    ViewBag.UserRole = RolesType.Registered;
            }
            else
            {
                ViewBag.UserRole = "vistor";
            }
            var tags = _postService.GetTagsList(new Services.Z_Harag.Helpers.PagingParams());
            return View("~/Themes/Pavilion/Views/Harag/Home/Home.cshtml", tags);
        }


        public virtual IActionResult DisallaowedProducts()
        {
            ViewBag.RateCount = 20;
            return View("~/Themes/Pavilion/Views/Harag/GeneralPages/notallowed.cshtml");
        }

        public virtual IActionResult Agrement()
        {
            ViewBag.Agreement = SettingsModel.UseWebsiteCompact;

            return View("~/Themes/Pavilion/Views/Harag/GeneralPages/terms.cshtml");
        }

        public virtual IActionResult FeaturedProducts()
        {
            
            return View("~/Themes/Pavilion/Views/Harag/GeneralPages/featuredproducts.cshtml");
        }


        public virtual IActionResult Navbar(PagingParams pagingParams, int catId)
        {

            var model = _categoryService.GetCategoriesWithSubCategories(pagingParams);

            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());

            var outputModel = new CategoryWithSubCategoriesOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinks(model, "Consultant.Navbar"),
                Items = model.List.Select(m => new CategoryWithSubCategoriesModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    IsSelected = (catId == m.Id),
                    SubCategories = m.SubCategories.Select(s => new SubCategoryModel()
                    {
                        Id = s.Id,
                        Name = s.Name
                    }).ToList()
                }).ToList(),
            };


            return PartialView("~/Themes/Pavilion/Views/Harag/Shared/_Navbar.cshtml", outputModel);
        }


        




        #endregion

    }
}
