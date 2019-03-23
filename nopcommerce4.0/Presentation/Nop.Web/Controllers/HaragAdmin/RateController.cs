using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Nop.Services.Z_HaragAdmin.Rate;
using Nop.Core.Domain.Customers;
using Nop.Services.Z_Harag.Helpers;
using Nop.Web.Models.HaragAdmin.Rate;

namespace Nop.Web.Controllers.HaragAdmin
{
    public class RateController : Controller
    {
        #region Fields
        private readonly IRateService _rateService;
       
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;
        

        #endregion

        #region Ctor
        public RateController(IRateService rateService,  Core.IWorkContext workContext, IHostingEnvironment env)
        {
            this._rateService = rateService;
           
            this._workContext = workContext;
            this._env = env;
            
        }
        #endregion
        //Get Rates
        public IActionResult GetRates()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            return View("~/Themes/Pavilion/Views/HaragAdmin/Rates/GetRates.cshtml");
        }

        //Get Rates Ajax
        [HttpPost]
        public IActionResult GetRatesAjax()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            //Server Side Parameters
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            //int startRec = Request.Form.GetValues("start").First;
            //int start = Convert.ToInt32(Request.Form.GetValues("start")[0]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];
           

            var RatesInDb = _rateService.GetRates(start, length, searchValue, sortColumnName, sortDirection);
            var rates = new RateOutputModel
            {
                Rates=RatesInDb.Select(r=>new RateModel
                {
                    Id=r.Id,
                    RaterUser=r.Customer?.Username,
                    RatedUser=r.UserId.ToString(),
                    AdviceDeal=r.AdviceDeal,
                    IsBuyDone=r.IsBuyDone,
                    BuyTime=r.BuyTime,
                    RateComment=r.RateComment
                }).ToList()
            };

            return Json(new { data=rates.Rates});
        }

        // Get Rate Details
        public IActionResult GetRateDetails(int rateId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            if (rateId == 0)
                return NotFound();

            var rateInDb = _rateService.GetRateDetails(rateId);

            if (rateInDb == null)
                return NotFound();

            var rate = new RateModel
            {
                Id= rateInDb.Id,
                RaterUser= rateInDb.Customer?.Username,
                RatedUser= rateInDb.User?.Username,
                IsBuyDone= rateInDb.IsBuyDone,
                AdviceDeal= rateInDb.AdviceDeal,
                RateComment= rateInDb.RateComment
            };

            return Json(new {rate });

        }
        //Delete Rate
        [HttpDelete]
        public IActionResult DeleteRate(int rateId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            if (rateId == 0)
                return NotFound();

            _rateService.DeleteRate(rateId);

            return Json(new { data = true });
        }
    }
}