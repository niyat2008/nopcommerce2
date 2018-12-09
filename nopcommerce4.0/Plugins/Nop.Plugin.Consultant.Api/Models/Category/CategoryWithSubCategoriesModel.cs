using Nop.Plugin.Consultant.Api.Models.SubCategory;
using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Consultant.Api.Models.Category
{
    public class CategoryWithSubCategoriesModel : BaseNopEntityModel
    {
        public CategoryWithSubCategoriesModel()
        {
            SubCategories = new Collection<SubCategoryModel>();
        }
        public string Name { get; set; }

        public ICollection<SubCategoryModel> SubCategories { get; set; }
    }
}
