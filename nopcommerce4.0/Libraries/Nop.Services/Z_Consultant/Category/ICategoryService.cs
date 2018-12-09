using Nop.Core.Domain.Z_Consultant;
using Nop.Services.Z_Consultant.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Consultant.Category
{
    public interface ICategoryService
    {
        Z_Consultant_Category GetCategoryById(int CategoryId);
        PagedList<Z_Consultant_Category> GetCategories(PagingParams pagingParams);

        Z_Consultant_Category GetCategoryWithSubCategories(int id);
        PagedList<Z_Consultant_Category> GetCategoriesWithSubCategories(PagingParams pagingParams);
    }
}
