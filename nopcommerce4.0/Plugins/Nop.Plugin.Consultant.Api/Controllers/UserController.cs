//using Microsoft.AspNetCore.Mvc;
//using Nop.Core.Domain.Customers;
//using Nop.Plugin.Consultant.Api.Models.User;
//using Nop.Services.Z_Consultant.Helpers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Nop.Plugin.Consultant.Api.Controllers
//{
//    public class UserController : Controller
//    {
//        private readonly Core.IWorkContext _workContext;

//        public UserController(Core.IWorkContext workContext)
//        {
//            this._workContext = workContext;
//        }

//        [HttpGet]
//        public IActionResult GetUserInfo()
//        {
//            if (!_workContext.CurrentCustomer.IsRegistered())
//                return Unauthorized();

//            UserModel model = new UserModel();
             
//            if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
//                model.UserRole = RolesType.Administrators;
//            else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
//                model.UserRole = RolesType.Consultant;
//            else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
//                model.UserRole = RolesType.Registered;

//            model.Id = _workContext.CurrentCustomer.Id;
//            model.Username = _workContext.CurrentCustomer.Username;

//            return Ok(model);
//        }
//    }
//}
