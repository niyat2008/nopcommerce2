//using Microsoft.AspNetCore.Mvc;
//using Nop.Services.Customers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Nop.Plugin.Consultant.Api.Controllers
//{
//    public class ValuesController:Controller
//    {
//        private readonly ICustomerService _customerService;

//        public ValuesController(ICustomerService customerService)
//        {
//            this._customerService = customerService;
//        }


//        [HttpGet]
//        public IActionResult GetValues()
//        {
//            var values = new int[] { 1,2,3 };
//            _customerService.GetCustomerById(280);
//            return Ok(values);
//        }
//    }
//}
