//using Microsoft.AspNetCore.Mvc;
//using Nop.Core.Domain.Customers;
//using Nop.Services.Customers;
//using Nop.Services.Events;
//using Nop.Services.Localization;
//using Nop.Services.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Nop.Services.Authentication;
//using Nop.Web.Framework.Mvc.Filters;
//using Nop.Web.Framework.Security;
//using Nop.Core;
//using Nop.Plugin.Consultant.Api.Models.Login;


//namespace Nop.Plugin.Consultant.Api.Controllers
//{
//    public class AuthController : Controller
//    {
//        private readonly ICustomerService _customerService;
//        private readonly ICustomerRegistrationService _customerRegistrationService;
//        private readonly IAuthenticationService _authenticationService;
//        private readonly IEventPublisher _eventPublisher;
//        private readonly ICustomerActivityService _customerActivityService;
//        private readonly IWorkContext _workContext;
//        private readonly ILocalizationService _localizationService;


//        public AuthController(
//            ICustomerService customerService,
//            ICustomerRegistrationService customerRegistrationService,
//            IAuthenticationService authenticationService,
//            IEventPublisher eventPublisher,
//            ICustomerActivityService customerActivityService,
//            IWorkContext workContext,
//            ILocalizationService localizationService
//            )
//        {
            
//            this._authenticationService = authenticationService;
//            this._localizationService = localizationService;
//            this._customerService = customerService;
//            this._customerRegistrationService = customerRegistrationService;
//            this._customerActivityService = customerActivityService;
//            this._eventPublisher = eventPublisher;
//            this._workContext = workContext;
//        }


//        [HttpGet]
//        public IActionResult GetValues()
//        {
//            var values = new int[] { 1, 2, 3 };
//            //_customerService.GetCustomerById(280);
//            return Ok(values);
//        }

//        [HttpPost]
//        public  IActionResult Login([FromBody]LoginModel model)
//        {



//            if (ModelState.IsValid)
//            {

//                var loginResult = _customerRegistrationService.ValidateCustomerByMobile(model.Phone, model.Password);

//                switch (loginResult)
//                {
//                    case CustomerLoginResults.Successful:
//                        {
//                            var customer = _customerService.GetCustomerByMobile(model.Phone);


//                            //sign in new customer
//                            _authenticationService.SignIn(customer, true);

//                            //raise event       
//                            _eventPublisher.Publish(new CustomerLoggedinEvent(customer));

//                            //activity log
//                            _customerActivityService.InsertActivity(customer, "PublicStore.Login", _localizationService.GetResource("ActivityLog.PublicStore.Login"));

//                            return Ok();
//                        }
//                    case CustomerLoginResults.CustomerNotExist:
//                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.CustomerNotExist"));
//                        break;
//                    case CustomerLoginResults.Deleted:
//                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.Deleted"));
//                        break;
//                    case CustomerLoginResults.NotActive:
//                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.NotActive"));
//                        break;
//                    case CustomerLoginResults.NotRegistered:
//                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.NotRegistered"));
//                        break;
//                    case CustomerLoginResults.LockedOut:
//                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.LockedOut"));
//                        break;
//                    case CustomerLoginResults.WrongPassword:
//                    default:
//                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials"));
//                        break;
//                }
//            }

//            //If we got this far, something failed, redisplay form

//            return BadRequest(model);

            
//        }


//        [HttpGet]
//        //[HttpsRequirement(SslRequirement.Yes)]
//        public virtual IActionResult Info()
//        {
//            if (!_workContext.CurrentCustomer.IsRegistered())
//                return BadRequest();

//            //var model = new CustomerInfoModel();
//            //model = _customerModelFactory.PrepareCustomerInfoModel(model, _workContext.CurrentCustomer, false);

//            //return View(model);
//            return Ok(_workContext.CurrentCustomer.Mobile+"l,m/l");
            
//        }
//    }
//}
