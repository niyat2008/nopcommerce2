using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Z_HaragAdmin.BankAccount;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Z_Harag.Helpers;
using Nop.Services.Events;
using Nop.Web.Models.HaragAdmin.BankAccount;

namespace Nop.Web.Controllers.HaragAdmin
{
    public class BankAccountController : Controller
    {
        #region Fields
        private readonly IBankAccountService _bankService;
        private readonly IWorkContext _workContext;
        private readonly IEventPublisher _env;
        #endregion
        #region Ctor
        public BankAccountController(IBankAccountService bankService, IWorkContext workContext, IEventPublisher env)
        {
            this._bankService = bankService;
            this._workContext = workContext;
            this._env = env;
        }
        #endregion
        #region Actions
        //Get Bank Accounts 
        public IActionResult GetBankAccounts()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            var model = new BankAccountModel();
           

            return View("~/Themes/Pavilion/Views/HaragAdmin/BankAccount/GetAllBankAccounts.cshtml", model);
        }
        //Get Bank Accounts Ajax 
        [HttpPost]
        public IActionResult GetBankAccountsAjax()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            //Server Side Parameters
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());

            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];

          

            var modelInDb = _bankService.GetAllAccount( start,  length,  searchValue,  sortColumnName,  sortDirection);


            var model = new BankAccountOutputModel
            {
                Items = modelInDb.Select(c => new BankAccountModel
                {
                    Id=c.Id,
                    BankName=c.BankName,
                    IBANNumber=c.IBANNumber,
                    AccountNo=c.AccountNo
                }).ToList()
            };

            return Json(new { data=model.Items});
        }

        //Add Bank Account 
        public IActionResult AddBankAccount(int accountId=0)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            var model = new PostBankAccount();
            if (accountId != 0)
            {
                var accountModel = _bankService.GetBankAccount(accountId);
                return View("~/Themes/Pavilion/Views/HaragAdmin/BankAccount/AddBankAccount.cshtml", accountModel);
            }

            return View("~/Themes/Pavilion/Views/HaragAdmin/BankAccount/AddBankAccount.cshtml", model);
        }
        //Add Bank Account Ajax
        [HttpPost]
        public IActionResult AddBankAccountAjax([FromBody]PostBankAccount model)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            if(model.Id !=0)
            {
                var response = _bankService.UpdateBankAccount(model);
                return Json(new { data = response });
            }

            var result = _bankService.AddBankAccount(model);

            return Json(new { data = result });

        }
        //Delete Bank Account 
        [HttpDelete]
        public IActionResult DeleteBankAccount(int accountId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            if (accountId == 0)
            {
                return NotFound();
            }

            var result = _bankService.DeleteBankAccount(accountId);

            return Json(new { data = result });

        }
        #endregion
    }
}