using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.Categories
{
    public interface ICategoryService
    {
        //Add Category
        bool AddCategory(PostCategoryModel category);

        //Get Categories
        List<Z_Harag_Category> GetCategories(int start, int length, string searchValue, string sortColumnName, string sortDirection);
        //Update Category
        bool UpdateCategory(PostCategoryModel category);
        //Get Category By Id
        Z_Harag_Category GetCategoryById(int id);

        
    }
}
