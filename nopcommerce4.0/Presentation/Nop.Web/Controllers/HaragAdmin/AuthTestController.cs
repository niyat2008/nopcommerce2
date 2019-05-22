using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Models.Customer;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework.Security.Captcha;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Customers;
using Nop.Services.Customers;
using Nop.Services.Orders;
using Nop.Core;
using Microsoft.AspNetCore.Authentication;
using Nop.Services.Events;
using Nop.Services.Logging;
using Nop.Services.Localization;
using Nop.Web.Factories;

namespace Nop.Web.Controllers.HaragAdmin
{
    public class AuthTestController : BasePublicController
    {
        #region  Fields
        private readonly CaptchaSettings _captchaSettings;
        private readonly LocalizationSettings _localizationSettings;
        private readonly CustomerSettings _customerSettings;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly ICustomerService _customerService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IWorkContext _workContext;
        private readonly IAuthenticationService _authenticationService;
        private readonly IEventPublisher _eventPublisher;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ILocalizationService _localizationService;
        private readonly ICustomerModelFactory _customerModelFactory;
        #endregion
        #region Ctor
        public AuthTestController(CaptchaSettings captchaSettings,
            LocalizationSettings localizationSettings,
            CustomerSettings customerSettings,
            ICustomerRegistrationService customerRegistrationService,
            ICustomerService customerService,
            IShoppingCartService shoppingCartService,
            IWorkContext workContext,
            IAuthenticationService authenticationService,
            IEventPublisher eventPublisher,
            ICustomerActivityService customerActivityService,
            ILocalizationService localizationService,
            ICustomerModelFactory customerModelFactory)
        {
            this._captchaSettings = captchaSettings;
            this._localizationSettings = localizationSettings;
            this._customerSettings = customerSettings;
            this._customerRegistrationService = customerRegistrationService;
            this._customerService = customerService;
            this._shoppingCartService = shoppingCartService;
            this._workContext = workContext;
            this._authenticationService = authenticationService;
            this._eventPublisher = eventPublisher;
            this._customerActivityService = customerActivityService;
            this._localizationService = localizationService;
            this._customerModelFactory = customerModelFactory;
        }
        #endregion
        #region  Methods
        [HttpPost]
        public IActionResult Login([FromBody]LoginModel model, string returnUrl, bool captchaValid)
        {
           return RedirectToAction("Login", "Customer",new { model=model, returnUrl=returnUrl, captchaValid=captchaValid });
        }

        //[HttpPost]
        //[ValidateCaptcha]
        ////available even when a store is closed
        //[CheckAccessClosedStore(true)]
        ////available even when navigation is not allowed
        //[CheckAccessPublicStore(true)]
        //public virtual IActionResult Login(LoginModel model, string returnUrl, bool captchaValid)
        //{
            

        //    if (ModelState.IsValid)
        //    {
        //        if (_customerSettings.UsernamesEnabled && model.Username != null)
        //        {
        //            model.Username = model.Username.Trim();
        //        }

        //        var loginResult = _customerRegistrationService.ValidateCustomerByMobile(_customerSettings.UsernamesEnabled ? model.Username : model.Mobile, model.Password);

        //        switch (loginResult)
        //        {
        //            case CustomerLoginResults.Successful:
        //                {
        //                    var customer = _customerSettings.UsernamesEnabled
        //                        ? _customerService.GetCustomerByUsername(model.Username)
        //                        : _customerService.GetCustomerByMobile(model.Mobile);

        //                    //migrate shopping cart
        //                    _shoppingCartService.MigrateShoppingCart(_workContext.CurrentCustomer, customer, true);

        //                    //sign in new customer
        //                    _authenticationService.SignIn(customer, model.RememberMe);

        //                    //raise event       
        //                    _eventPublisher.Publish(new CustomerLoggedinEvent(customer));

        //                    //activity log
        //                    _customerActivityService.InsertActivity(customer, "PublicStore.Login", _localizationService.GetResource("ActivityLog.PublicStore.Login"));

        //                    if ((string)TempData["ReturnURL"] == "consultations")
        //                        return RedirectToRoute("Consultant.ConsultantHome");

        //                    if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
        //                        return RedirectToRoute("HomePage");

        //                    return Redirect(returnUrl);
        //                }
        //            case CustomerLoginResults.CustomerNotExist:
        //                ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.CustomerNotExist"));
        //                break;
        //            case CustomerLoginResults.Deleted:
        //                ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.Deleted"));
        //                break;
        //            case CustomerLoginResults.NotActive:
        //                ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.NotActive"));
        //                break;
        //            case CustomerLoginResults.NotRegistered:
        //                ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.NotRegistered"));
        //                break;
        //            case CustomerLoginResults.LockedOut:
        //                ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.LockedOut"));
        //                break;
        //            case CustomerLoginResults.WrongPassword:
        //            default:
        //                ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials"));
        //                break;
        //        }
        //    }

        //    //If we got this far, something failed, redisplay form
        //    model = _customerModelFactory.PrepareLoginModel(model.CheckoutAsGuest);

        //    return View(model);
        //}
        #endregion


    }
}