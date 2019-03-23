using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Services.Z_Consultant.Helpers;
using Nop.Services.Z_ConsultantAdmin;
using Nop.Services.Z_ConsultantAdmin.Customers;
using Nop.Services.Z_ConsultantAdmin.Post;
using Nop.Web.Models.ConsultantAdmin.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Controllers.ConsultantِAdmin
{
    //BasePublicController
    public class DashboardConsultantController : BasePublicController
    {

        #region Fields
        private readonly IPostService _postService;
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;
        private readonly ICustomerService _customer;
        #endregion

        #region Ctor
        public DashboardConsultantController(IPostService postService, Core.IWorkContext workContext, IHostingEnvironment env, ICustomerService customer)
        {
            this._postService = postService;
            this._workContext = workContext;
            this._env = env;
            this._customer = customer;
        }
        #endregion




        public virtual IActionResult Home()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) &&!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.ConsultationAdmin, true))
                return Forbid();
            var posts = _postService.GetPostsNumber();
            var members = _customer.GetMembersNumber();
            var consultants = _customer.GetConsultantsNumber();
            var onlineMembers = _customer.GetOnlineMembersNumber();
            var onlineConsultants = _customer.GetOnlineConsultantsNumber();


            var model = new Dashboard
            {
                PostsCount=posts,
                MembersCount=members,
                ConsultantCount=consultants,
                OnlineMembersCount=onlineMembers,
                OnlineConsultantsCount=onlineConsultants
            };

            return View("~/Themes/Pavilion/Views/ConsultantAdmin/DashboardConsultant/Home.cshtml",model);
        }


    }
}
