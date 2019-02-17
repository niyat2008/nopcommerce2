using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;

namespace Nop.Services.Z_HaragAdmin.Categories
{
   public class CategoryService:ICategoryService
    {
        #region Fields
        private readonly IRepository<Z_Harag_Category> _categoryRepository;
        #endregion

        #region Ctor
        public CategoryService(IRepository<Z_Harag_Category> categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }
        #endregion

        #region Mehtods
        public bool AddCategory(PostCategoryModel category)
        {
            var categoryInDb = new Z_Harag_Category
            {
                Name = category.Name,
                DateCreated = DateTime.Now
            };

            if (categoryInDb != null)
            {
                _categoryRepository.Insert(categoryInDb);
                return true;
            }
            return false;
        }
        //Get Categories
        public List<Z_Harag_Category> GetCategories(int start, int length, string searchValue, string sortColumnName, string sortDirection)
        {
            var categories = _categoryRepository.TableNoTracking;

            //search
            if (!string.IsNullOrEmpty(searchValue))
                categories = categories.Where(c => c.Name.ToLower().Contains(searchValue));

            //sort
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
                categories = categories.OrderBy(sortColumnName + " " + sortDirection);

            //paging
            categories = categories.OrderByDescending(c => c.DateCreated).Skip(start).Take(length);

            return categories.ToList();

        }
        #endregion
    }
}
