using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Directory;
using Nop.Plugin.Shipping.ByTotal.Domain;
using Nop.Plugin.Shipping.ByTotal.Models;
using Nop.Plugin.Shipping.ByTotal.Services;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Linq;

namespace Nop.Plugin.Shipping.ByTotal.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    public class ShippingByTotalController : BasePluginController
    {
        #region Fields

        private readonly CurrencySettings _currencySettings;
        private readonly ICountryService _countryService;
        private readonly ICurrencyService _currencyService;
        private readonly ILocalizationService _localizationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IShippingByTotalService _shippingByTotalService;
        private readonly IShippingService _shippingService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IStoreService _storeService;
        private readonly ShippingByTotalSettings _shippingByTotalSettings;

        #endregion Fields

        #region Ctor

        public ShippingByTotalController(
            CurrencySettings currencySettings,
            ICountryService countryService,
            ICurrencyService currencyService,
            ILocalizationService localizationService,
            IPermissionService permissionService,
            ISettingService settingService,
            IShippingByTotalService shippingByTotalService,
            IShippingService shippingService,
            IStateProvinceService stateProvinceService,
            IStoreService storeService,
            ShippingByTotalSettings shippingByTotalSettings)
        {
            _countryService = countryService;
            _currencyService = currencyService;
            _currencySettings = currencySettings;
            _localizationService = localizationService;
            _permissionService = permissionService;
            _settingService = settingService;
            _shippingByTotalService = shippingByTotalService;
            _shippingByTotalSettings = shippingByTotalSettings;
            _shippingService = shippingService;
            _stateProvinceService = stateProvinceService;
            _storeService = storeService;
        }

        #endregion Ctor

        public IActionResult Configure()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
            {
                return AccessDeniedView();
            }

            var shippingMethods = _shippingService.GetAllShippingMethods();
            if (shippingMethods.Count == 0)
            {
                return Content("No shipping methods can be loaded");
            }

            var model = new ShippingByTotalListModel();

            // stores
            model.AvailableStores.Add(new SelectListItem() { Text = "*", Value = "0" });
            foreach (var store in _storeService.GetAllStores())
            {
                model.AvailableStores.Add(new SelectListItem() { Text = store.Name, Value = store.Id.ToString() });
            }

            // warehouses
            model.AvailableWarehouses.Add(new SelectListItem() { Text = "*", Value = "0" });
            foreach (var warehouse in _shippingService.GetAllWarehouses())
            {
                model.AvailableWarehouses.Add(new SelectListItem() { Text = warehouse.Name, Value = warehouse.Id.ToString() });
            }

            // shipping methods
            foreach (var sm in shippingMethods)
            {
                model.AvailableShippingMethods.Add(new SelectListItem() { Text = sm.Name, Value = sm.Id.ToString() });
            }

            // countries
            model.AvailableCountries.Add(new SelectListItem() { Text = "*", Value = "0" });
            var countries = _countryService.GetAllCountries(showHidden: true);
            foreach (var c in countries)
            {
                model.AvailableCountries.Add(new SelectListItem() { Text = c.Name, Value = c.Id.ToString() });
            }

            model.AvailableStates.Add(new SelectListItem() { Text = "*", Value = "0" });
            model.LimitMethodsToCreated = _shippingByTotalSettings.LimitMethodsToCreated;
            model.PrimaryStoreCurrencyCode = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId).CurrencyCode;

            return View("~/Plugins/Shipping.ByTotal/Views/Configure.cshtml", model);
        }

        [HttpPost, AdminAntiForgery]
        public IActionResult RatesList(DataSourceRequest command)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
            {
                return AccessDeniedKendoGridJson();
            }

            var records = _shippingByTotalService.GetAllShippingByTotalRecords(command.Page - 1, command.PageSize);
            var sbtModel = records.Select(x =>
                {
                    var m = new ShippingByTotalModel
                    {
                        Id = x.Id,
                        StoreId = x.StoreId,
                        WarehouseId = x.WarehouseId,
                        ShippingMethodId = x.ShippingMethodId,
                        CountryId = x.CountryId,
                        DisplayOrder = x.DisplayOrder,
                        From = x.From,
                        To = x.To,
                        UsePercentage = x.UsePercentage,
                        ShippingChargePercentage = x.ShippingChargePercentage,
                        ShippingChargeAmount = x.ShippingChargeAmount,
                    };

                    // shipping method
                    var shippingMethod = _shippingService.GetShippingMethodById(x.ShippingMethodId);
                    m.ShippingMethodName = (shippingMethod != null) ? shippingMethod.Name : "Unavailable";

                    // store
                    var store = _storeService.GetStoreById(x.StoreId);
                    m.StoreName = (store != null) ? store.Name : "*";

                    // warehouse
                    var warehouse = _shippingService.GetWarehouseById(x.WarehouseId);
                    m.WarehouseName = (warehouse != null) ? warehouse.Name : "*";

                    // country
                    var c = _countryService.GetCountryById(x.CountryId);
                    m.CountryName = (c != null) ? c.Name : "*";
                    m.CountryId = x.CountryId;

                    // state/province
                    var s = _stateProvinceService.GetStateProvinceById(x.StateProvinceId);
                    m.StateProvinceName = (s != null) ? s.Name : "*";
                    m.StateProvinceId = x.StateProvinceId;

                    // ZIP / postal code
                    m.ZipPostalCode = (!String.IsNullOrEmpty(x.ZipPostalCode)) ? x.ZipPostalCode : "*";

                    return m;
                })
                .ToList();
            var gridModel = new DataSourceResult
            {
                Data = sbtModel,
                Total = records.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost, AdminAntiForgery]
        public IActionResult RateUpdate(ShippingByTotalModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
            {
                return AccessDeniedKendoGridJson();
            }

            var shippingByTotalRecord = _shippingByTotalService.GetShippingByTotalRecordById(model.Id);
            shippingByTotalRecord.ZipPostalCode = model.ZipPostalCode == "*" ? null : model.ZipPostalCode;
            shippingByTotalRecord.DisplayOrder = model.DisplayOrder;
            shippingByTotalRecord.From = model.From;
            shippingByTotalRecord.To = model.To;
            shippingByTotalRecord.UsePercentage = model.UsePercentage;
            shippingByTotalRecord.ShippingChargePercentage = model.UsePercentage ? model.ShippingChargePercentage : 0;
            shippingByTotalRecord.ShippingChargeAmount = !model.UsePercentage ? model.ShippingChargeAmount : 0;
            shippingByTotalRecord.ShippingMethodId = model.ShippingMethodId;
            shippingByTotalRecord.StoreId = model.StoreId;
            shippingByTotalRecord.WarehouseId = model.WarehouseId;
            shippingByTotalRecord.StateProvinceId = model.StateProvinceId;
            shippingByTotalRecord.CountryId = model.CountryId;
            _shippingByTotalService.UpdateShippingByTotalRecord(shippingByTotalRecord);

            return new NullJsonResult();
        }

        [HttpPost, AdminAntiForgery]
        public IActionResult RateDelete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
            {
                return AccessDeniedKendoGridJson();
            }

            var shippingByTotalRecord = _shippingByTotalService.GetShippingByTotalRecordById(id);
            if (shippingByTotalRecord != null)
            {
                _shippingByTotalService.DeleteShippingByTotalRecord(shippingByTotalRecord);
            }
            return new NullJsonResult();
        }

        [HttpPost, AdminAntiForgery]
        public IActionResult AddShippingRate(ShippingByTotalListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
            {
                return Json(new { Result = false, Message = _localizationService.GetResource("Plugins.Shipping.ByTotal.ManageShippingSettings.AccessDenied") });
            }

            var zipPostalCode = model.AddZipPostalCode;

            if (zipPostalCode != null)
            {
                int zipMaxLength = ByTotalShippingComputationMethod.ZipPostalCodeMaxLength;
                zipPostalCode = zipPostalCode.Trim();
                if (zipPostalCode.Length > zipMaxLength)
                {
                    zipPostalCode = zipPostalCode.Substring(0, zipMaxLength);
                }
            }

            var shippingByTotalRecord = new ShippingByTotalRecord
            {
                ShippingMethodId = model.AddShippingMethodId,
                StoreId = model.AddStoreId,
                WarehouseId = model.AddWarehouseId,
                CountryId = model.AddCountryId,
                StateProvinceId = model.AddStateProvinceId,
                ZipPostalCode = zipPostalCode,
                DisplayOrder = model.AddDisplayOrder,
                From = model.AddFrom,
                To = model.AddTo,
                UsePercentage = model.AddUsePercentage,
                ShippingChargePercentage = (model.AddUsePercentage) ? model.AddShippingChargePercentage : 0,
                ShippingChargeAmount = (model.AddUsePercentage) ? 0 : model.AddShippingChargeAmount
            };
            _shippingByTotalService.InsertShippingByTotalRecord(shippingByTotalRecord);

            return Json(new { Result = true });
        }

        [HttpPost, AdminAntiForgery]
        public IActionResult SaveGeneralSettings(ShippingByTotalListModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
            {
                return Json(new { Result = false, Message = _localizationService.GetResource("Plugins.Shipping.ByTotal.ManageShippingSettings.AccessDenied") });
            }

            //save settings
            _shippingByTotalSettings.LimitMethodsToCreated = model.LimitMethodsToCreated;
            _settingService.SaveSetting(_shippingByTotalSettings);

            return Json(new { Result = true, Message = _localizationService.GetResource("Plugins.Shipping.ByTotal.ManageShippingSettings.Saved") });
        }
    }
}