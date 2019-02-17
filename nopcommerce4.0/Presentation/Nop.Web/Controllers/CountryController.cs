using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Localization;
using Nop.Web.Factories;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Web.Controllers
{
    public partial class CountryController : BasePublicController
	{
        #region Fields

        private readonly ICountryModelFactory _countryModelFactory;
        private readonly IWorkContext _workContext;
        //private readonly ILocalizationService _localizationService;

        #endregion

        #region Ctor

        public CountryController(ICountryModelFactory countryModelFactory,
            ILocalizationService localizationService,  IWorkContext workContext)
		{
            this._countryModelFactory = countryModelFactory;
            //this._localizationService = localizationService;
            this._workContext = workContext;
        }
        
        #endregion
        
        #region States / provinces

        //available even when navigation is not allowed
        [CheckAccessPublicStore(true)]
        public virtual IActionResult GetStatesByCountryId(string countryId, bool addSelectStateItem)
        {
            var model = _countryModelFactory.GetStatesByCountryId(countryId, addSelectStateItem);
            //ali country coding
            //foreach (var item in model)
            //{
            //    item.name = _localizationService.GetResource(item.name);
            //}

            //string lang = _workContext.WorkingLanguage.Name;

            //if (lang != "English")
            //{
            //    foreach (var item in model)
            //    {
            //        item.name = item.arname;
            //    }

            //}
           


            return Json(model);
        }
        
        #endregion
    }
}