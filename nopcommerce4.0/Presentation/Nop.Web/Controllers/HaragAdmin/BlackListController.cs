using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Models.HaragAdmin.BlackList;
using Nop.Services.Z_HaragAdmin.BlackList;
using Nop.Services.Events;
using Nop.Core.Domain.Customers;
using Nop.Services.Z_Consultant.Helpers;

namespace Nop.Web.Controllers.HaragAdmin
{
    public class BlackListController : Controller
    {
        #region Fields
        private readonly IBlackListService _blackListService;
        private readonly Core.IWorkContext _workContext;
        private readonly IEventPublisher _env;
        #endregion

        #region Ctor
        public BlackListController(IBlackListService blackListService, Core.IWorkContext workContext, IEventPublisher env)
        {
            this._blackListService = blackListService;
            this._workContext = workContext;
            this._env = env;
        }
        #endregion
        #region Actions
        //Get Black List Customers
        public IActionResult GetBlackListCustomers()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            return View("~/Themes/Pavilion/Views/HaragAdmin/BlackList/GetBlackListCustomers.cshtml");
        }
        //Get Black List Customers Ajax
        public IActionResult GetBlackListCustomersAjax()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            var customersInDb = _blackListService.GetBlackListCustomers();

            var customers = new BlackListOutputModel
            {
                Items = customersInDb.Select(c => new BlackListModel
                {
                    Id=c.Id,
                    CustomerId=c.Customer.Id,
                    Name=c.Customer?.Username,
                    Phone=c.Customer?.Mobile,
                    Email=c.Customer?.Email

                }).ToList()
            };



            return Json(new { data=customers.Items});
        }
        //Add Black List Customer
        public IActionResult AddBlackListCustomer([FromBody]PostBlackListModel model)
        {
            if (!ModelState.IsValid)
                return NotFound();

            var result = _blackListService.AddBlackListCustomer(model);

            return Json(new { data = result });

        }

        //DElete BlackList Customer
        [HttpDelete]
        public IActionResult DeleteBlackListCustomer(int id=0,int blackId=0)
        {
            if (id == 0 || blackId==0)
                return NotFound();
            _blackListService.DeleteBlackListCustomer(id,blackId);

            return Json(new { result = true });

        }
        #endregion
    }
}