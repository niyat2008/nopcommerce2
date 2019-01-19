using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Data;
using Nop.Core.Domain.Z_Consultant;
using Nop.Services.Events;
using Nop.Services.Z_Consultant.Helpers;

namespace Nop.Services.Z_Consultant.Category
{
    public class CategoryService : ICategoryService
    {

        private readonly IRepository<Z_Consultant_Category> _categoryRepository;
        private readonly IEventPublisher _eventPublisher;


        public CategoryService(IRepository<Z_Consultant_Category> categoryRepository,
        IEventPublisher eventPublisher)
        {
            this._categoryRepository = categoryRepository;
            this._eventPublisher = eventPublisher;
        }

        public PagedList<Z_Consultant_Category> GetCategories(PagingParams pagingParams)
        {
            var query = _categoryRepository.TableNoTracking;

            return new PagedList<Z_Consultant_Category>(
                query, pagingParams.PageNumber, pagingParams.PageSize,false);
        }

        public PagedList<Z_Consultant_Category> GetCategoriesWithSubCategories(PagingParams pagingParams)
        {
            var query = _categoryRepository.TableNoTracking.Include(c => c.SubCategories);

            return new PagedList<Z_Consultant_Category>(
                query, pagingParams.PageNumber, pagingParams.PageSize,false);
        }

        public Z_Consultant_Category GetCategoryById(int CategoryId)
        {
            return _categoryRepository.Table.Where(c => c.Id == CategoryId).FirstOrDefault();
        }

        public Z_Consultant_Category GetCategoryWithSubCategories(int id)
        {
            var category = _categoryRepository.TableNoTracking
                .Include(c => c.SubCategories).FirstOrDefault(c => c.Id == id);
            return category;
        }
    }
}
