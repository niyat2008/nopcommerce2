﻿using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Services.Customers;
using Nop.Services.Z_Consultant.Helpers;
using Nop.Services.Z_Harag.BlackList;
using Nop.Services.Z_Harag.Rate;
using Nop.Services.Z_Harag.Post;
using Nop.Web.Models.Consultant.User;
using Nop.Web.Models.Harag.Post;
using Nop.Web.Models.Harag.Profile;
using Nop.Web.Models.Harag.Rate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core.Domain.Z_Harag;
using Nop.Web.Models.Harag;
using Nop.Services.Z_Harag.Payment;

namespace Nop.Web.Controllers.Harag
{
    public class PaymentController : BasePublicController
    {
        private readonly Core.IWorkContext _workContext;
        private readonly  ICustomerService _customerContext;
        private readonly  IRateSrevice _rateRepository
            ;
        private readonly  IBlackListService _blackListService;
        private readonly  IPostService _postService;
        private readonly  IPaymentService _paymentService;

        public PaymentController(Core.IWorkContext workContext, IRateSrevice _rateRepository,
             IPaymentService _paymentService,
             IBlackListService _blackListService, IPostService _postService,ICustomerService _customerContext)
        {
            this._paymentService = _paymentService;
            this._workContext = workContext;
            this._rateRepository = _rateRepository;
            this._customerContext = _customerContext;
            this._postService = _postService;
            this._blackListService = _blackListService;
        }

  

        
        [HttpGet]
        public IActionResult PaymentBanks()
        {
            ViewBag.Added = false;
            var banks = _paymentService.GetBaknAccountsDetails().Select(m => new BankAccountModel
            {
                AccountNumber = m.AccountNo,
                BankId = m.Id,
                IBANNumber = m.IBANNumber,
                BankName = m.BankName
            }).ToList();

            var model = new PaymentModel { Banks = banks};
            return View("~/Themes/Pavilion/Views/Harag/Payment/bankpayment.cshtml", model);
        }

        [HttpPost]
        public IActionResult AddPaymentBankAjax(PaymentModel payment)
        {

            if(!ModelState.IsValid)
                return View("~/Themes/Pavilion/Views/Harag/Payment/bankpayment.cshtml", payment);
            var model = new Z_Harag_BankPayment
            {
                SiteAmount = payment.SiteAmount,
                BankId = payment.BankId,
                Notes = payment.Notes,
                PostId = payment.PostId,
                TransatctorUser = payment.TransatctorUser,
                TransactionDate = payment.TransactionDate,
                UserName = payment.UserName,
                UserId = payment.UserId
            };

            ViewBag.Added = true;

            return View("~/Themes/Pavilion/Views/Harag/Payment/bankpayment.cshtml", payment);
        }

        [HttpGet]
        public IActionResult PaymentSadad()
        { 
            return View("~/Themes/Pavilion/Views/Harag/Payment/sadadpayment.cshtml"); 
        }

        [HttpGet]
        public IActionResult GetBaksAccountsDetails()
        { 
            var banks = _paymentService.GetBaknAccountsDetails();

            var model = banks.Select(m => new BankAccountModel
            {
                AccountNumber = m.AccountNo,
                BankId = m.Id,
                IBANNumber = m.IBANNumber,
                BankName = m.BankName
            }).ToList();

            return PartialView("~/Themes/Pavilion/Views/Harag/Payment/_BanksList.cshtml", model);
        }
        //[HttpGet]
        //public IActionResult GetUserInfo()
        //{
        //    UserModel model = new UserModel();

        //    if (_workContext.CurrentCustomer.IsRegistered())
        //    {
        //        if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //            model.UserRole = RolesType.Administrators;
        //        else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
        //            model.UserRole = RolesType.Consultant;
        //        else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
        //            model.UserRole = RolesType.Registered;

        //        model.Id = _workContext.CurrentCustomer.Id;
        //        model.Username = _workContext.CurrentCustomer.Username;
        //    }
        //    else
        //        model.UserRole = null;

        //    return PartialView("~/Themes/Pavilion/Views/Consultant/User/_UserInfo.cshtml", model);
        //}

        //[HttpGet]
        //public IActionResult GetAdminLink()
        //{
        //    UserModel model = new UserModel();

        //    if (_workContext.CurrentCustomer.IsRegistered())
        //    {
        //        if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //            return PartialView("~/Themes/Pavilion/Views/Consultant/User/_AdminLink.cshtml", 1);
        //    }
        //    return PartialView("~/Themes/Pavilion/Views/Consultant/User/_AdminLink.cshtml", 0);
        //}

        //[HttpGet]
        //public IActionResult GetUserRoles()
        //{
        //    if (_workContext.CurrentCustomer.IsRegistered())
        //    {
        //        if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
        //            ViewBag.UserRole = RolesType.Administrators;
        //        else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
        //            ViewBag.UserRole = RolesType.Consultant;
        //        else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
        //            ViewBag.UserRole = RolesType.Registered;
        //    }
        //    else
        //        ViewBag.UserRole = "vistor";
        //    return PartialView("~/Themes/Pavilion/Views/Consultant/User/_UserRolesAndThreeButtons.cshtml");
        //}
    }
}
