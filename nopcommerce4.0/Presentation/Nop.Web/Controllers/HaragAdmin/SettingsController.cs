﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Nop.Services.Z_HaragAdmin.Setting;
using Nop.Core.Domain.Customers;
using Nop.Services.Z_Harag.Helpers;

namespace Nop.Web.Controllers.HaragAdmin
{
    public class SettingsController : Controller
    {
        #region Fields
        private readonly ISettingService _settingService;
       
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;
        

        #endregion

        #region Ctor
        public SettingsController(ISettingService settingService, Core.IWorkContext workContext, IHostingEnvironment env)
        {
            this._settingService = settingService;
            
            this._workContext = workContext;
            this._env = env;
           
        }
        #endregion

        #region Actions
        //Update Settings
        public IActionResult UpdateSettings()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();



            return View("~/Themes/Pavilion/Views/HaragAdmin/Settings/UpdateSettings.cshtml");
        }

        //Update Settings Ajax
        public IActionResult UpdateSettingsAjax([FromBody]SettingsModel model)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();


            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();

            var updated = _settingService.UpdateSettings(model);

            return Json(new { result= updated });
        }
        #endregion
    }
}