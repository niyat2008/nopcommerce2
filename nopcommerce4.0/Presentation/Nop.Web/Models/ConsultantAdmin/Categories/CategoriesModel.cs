using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.ConsultantAdmin.Categories
{
    public class CategoriesModel:BaseNopEntityModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
