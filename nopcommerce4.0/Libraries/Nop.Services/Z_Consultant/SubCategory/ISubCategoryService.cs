using Nop.Core.Domain.Z_Consultant;
using Nop.Services.Z_Consultant.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Consultant.SubCategory
{
    public interface ISubCategoryService
    {
        PagedList<Z_Consultant_SubCategory> GetSubCategories(PagingParams pagingParams);
        Z_Consultant_SubCategory GetSubCategory(int subCategoryId);
        PagedList<Z_Consultant_SubCategory> GetSubCategoriesByCategoryId(PagingParams pagingParams, int categoryId);
        List<Z_Consultant_SubCategory> GetSubCategoriesByCategory(int categoryId);
    }
}
