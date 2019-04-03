using Nop.Web.Framework.Mvc.Models;
using Nop.Web.Models.Consultant.SubCategory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;


namespace Nop.Web.Models.Consultant.Category
{
    public class CategoryWithSubCategoriesModel : BaseNopEntityModel
    {
        public CategoryWithSubCategoriesModel()
        {
            SubCategories = new Collection<SubCategoryModel>();
        }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public ICollection<SubCategoryModel> SubCategories { get; set; }
    }
}
