using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Consultant.Api.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    public class ConsultantApiController : BasePluginController 
    {
    }
}