using Nop.Core.Domain.Z_Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_ConsultantAdmin.Categories
{
   public interface ICategoryService
    {
        //Get All Categories For DataTable
        List<Z_Consultant_Category> GetAllCategories(int start,int length,string searchValue,string sortColumnName,string sortDirection);
        //Add category
        Z_Consultant_Category AddCategory(CategoryModelForPost category);
        //Get All Categories
        List<Z_Consultant_Category> GetAllCategor();

        //Delete Category
        void DeleteCategory(int categoryId);
    }
}
