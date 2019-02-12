using Nop.Core.Domain.Z_Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_ConsultantAdmin.SubCategories
{
    public interface ISubCategoryService
    {
        //Get Subcategories
        List<Z_Consultant_SubCategory> GetSubCategories(int start, int length, string searchValue, string sortColumnName, string sortDirection);
        //Get SubCategories With Category ID
        List<Z_Consultant_SubCategory> GetSubCategoriesByCategoryId(int categoryId,int start, int length,string searchValue,string sortColumnName,string sortDirection);
        //Add SubCategory
        Z_Consultant_SubCategory AddSubCategory(SubCategoryForPost subCategory);
        //Delete SubCategory
         void DeleteSubCategory(int subCategoryId);
        //Get All Categories
        List<Z_Consultant_Category> GetAllCategor();
       

    }
}
