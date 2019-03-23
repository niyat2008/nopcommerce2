using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Nop.Services.Z_HaragAdmin.Cities;
using Nop.Core.Domain.Customers;
using Nop.Services.Z_Harag.Helpers;
using Nop.Web.Models.HaragAdmin.Cities;

namespace Nop.Web.Controllers.HaragAdmin
{
    public class CityController : Controller
    {
        #region Fields
        private readonly ICityService _cityService;
       
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;
       

        #endregion

        #region Ctor
        public CityController(ICityService cityService,Core.IWorkContext workContext, IHostingEnvironment env)
        {
            this._cityService = cityService;
            this._workContext = workContext;
            this._env = env;
           
        }
        #endregion

        #region Actions
        //Get Cities
        public IActionResult GetCities()
        {

            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            return View("~/Themes/Pavilion/Views/HaragAdmin/Cities/GetCities.cshtml");
        }
        //Get Cities Ajax
        public IActionResult GetCitiesAjax()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true) && !_workContext.CurrentCustomer.IsInCustomerRole(RolesType.HaragAdmin, true))
                return Forbid();

            var citiesInDb = _cityService.GetCities();
            var cities = new CityOutputModel
            {
                Cities = citiesInDb.Select(c => new CityModel
                {
                    Id=c.Id,
                    Name=c.ArName,
                    ProvinceId=c.ProvinceId
                }).ToList()
            };

            return Json(new { data = cities.Cities });
        }
        #endregion
    }
}