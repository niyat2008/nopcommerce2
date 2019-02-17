using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Z_Consultant;
using Nop.Services.Z_Consultant.Post;

namespace Nop.Web.Controllers.Consultant
{
    public class AddressController : BasePublicController
    {
        private readonly IUrlHelper _urlHelper;
        private readonly IPostService _postService;
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env; 


        public AddressController(
            IUrlHelper urlHelper,
            IPostService postService,
            Core.IWorkContext workContext,
            IHostingEnvironment env 
            )
        {
            this._urlHelper = urlHelper;
            this._postService = postService;
            this._workContext = workContext;
            this._env = env; 
        }
         
        private IActionResult GetAllCitiesAjax()
        {
            var cities = _postService.GetCities();

            return Json(cities);
        } 
        private IActionResult GetAllCityNeighborhoodAjax(int cityId)
        { 
            var cityNeighborhood = _postService.GetCityNeighborhood(cityId);

            return Json(cityNeighborhood);
        }



    }
}