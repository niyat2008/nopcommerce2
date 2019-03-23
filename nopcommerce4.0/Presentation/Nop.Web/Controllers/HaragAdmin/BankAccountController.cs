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
using Nop.Web.Models.HaragAdmin.BankPayment;
using Nop.Services.Customers;
using Nop.Services.Z_HaragAdmin.Setting;
using Nop.Services.Z_Harag.Payment;

namespace Nop.Web.Controllers.HaragAdmin
{
    public class BankAccountController : Controller
    {
        #region Fields
        private readonly IBankAccountService _bankService;
        private readonly IWorkContext _workContext;
        private readonly ISettingService settingService;
        private readonly ICustomerService customerService; 
        private readonly IPaymentService paymentService;
        private readonly SettingsModel Settings;
        private readonly IEventPublisher _env;
        #endregion
        #region Ctor
        public BankAccountController(ISettingService settingService, IBankAccountService bankService, IPaymentService paymentService, IWorkContext workContext, IEventPublisher env, ICustomerService customerService)
        {
            this._bankService = bankService;
            this.customerService = customerService;
            this.settingService = settingService;
            this.paymentService = paymentService;
            this._workContext = workContext;
            this._env = env;

            Settings = settingService.GetSettings();
        }
        #endregion
        #region Actions
        //Get Bank Accounts 
        public IActionResult GetBankAccounts()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
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
            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
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
            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
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

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            if (model.Id !=0)
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

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            if (accountId == 0)
            {
                return NotFound();
            }

            var result = _bankService.DeleteBankAccount(accountId);

            return Json(new { data = result });

        }

        //Get Payment 
        public IActionResult GetPayments()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            return View("~/Themes/Pavilion/Views/HaragAdmin/BankPayment/GetBankPayments.cshtml");
        }

        //Get Payment Ajax
        [HttpPost]
        public IActionResult GetPaymentsAjax()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());

            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];

            
            var paymentInDb = _bankService.GetPayments(start, length, searchValue, sortColumnName, sortDirection);

            var model = new BankPaymentOutputModel
            {
                Items = paymentInDb.Select(b => new BankPaymentModel
                {
                    Id=b.Id,
                    BankName=b.BankAccount?.BankName,
                    BankAccount=b.BankAccount?.AccountNo,
                    UserName=b.UserName,
                    TransatctorUser=b.TransatctorUser,
                    SiteAmount=b.SiteAmount,
                    Notes=b.Notes,
                    TransactionDate = b.TransactionDate,
                    Confirmed = b.PaymentConfirmed,
                    PostId=b.Post.Id
                }).ToList()
            };

            return Json(new { data = model.Items });
        }


        [HttpDelete]
        public IActionResult ConfirmPayment(int paymentId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            var payment = _bankService.ConfirmSitePayment(paymentId);

            if ( payment != null )
            {
                var user = customerService.GetCustomerById(payment.UserId);
                if (user != null && user.IsFeatured == false)
                {
                    var paymentA = paymentService.GetUserPayments(user.Id);
                    if (paymentA.Sum(m => m.SiteAmount) >= Settings.FeaturedMemberCommissionSum
                        && paymentA.Count >= Settings.FeaturedMemberCommissionNumber)
                    {
                        user.IsFeatured = true;

                    }
                    customerService.UpdateCustomer(user);
                }

                return Ok();

            }
            else
            {
                return BadRequest();
            }
            return BadRequest();
        }
        #endregion
    }
}