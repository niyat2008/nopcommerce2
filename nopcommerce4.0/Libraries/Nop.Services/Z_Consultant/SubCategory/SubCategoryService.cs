using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Data;
using Nop.Core.Domain.Z_Consultant;
using Nop.Services.Events;
using Nop.Services.Z_Consultant.Helpers;

namespace Nop.Services.Z_Consultant.SubCategory
{
    public class SubCategoryService : ISubCategoryService
    {

        private readonly IRepository<Z_Consultant_SubCategory> _subCategoryRepository;
        private readonly IEventPublisher _eventPublisher;


        public SubCategoryService(IRepository<Z_Consultant_SubCategory> subCategoryRepository,
        IEventPublisher eventPublisher)
        {
            this._subCategoryRepository = subCategoryRepository;
            this._eventPublisher = eventPublisher;
        }


        public PagedList<Z_Consultant_SubCategory> GetSubCategories(PagingParams pagingParams)
        {
            var query = _subCategoryRepository.TableNoTracking;

            return new PagedList<Z_Consultant_SubCategory>(
                query, pagingParams.PageNumber, pagingParams.PageSize,false);
        }


        public PagedList<Z_Consultant_SubCategory> GetSubCategoriesByCategoryId(PagingParams pagingParams, int categoryId)
        {
            var query = _subCategoryRepository.TableNoTracking.Where(s => s.CategoryId == categoryId);
                

            return new PagedList<Z_Consultant_SubCategory>(
                query, pagingParams.PageNumber, pagingParams.PageSize,false);
        }
        public List<Z_Consultant_SubCategory> GetSubCategoriesByCategory( int categoryId)
        {
            var query = _subCategoryRepository.TableNoTracking.Where(s => s.CategoryId == categoryId);


            return query.ToList();
                 
        }


        public Z_Consultant_SubCategory GetSubCategory(int subCategoryId)
        {
            var subCategory = _subCategoryRepository
                .TableNoTracking.FirstOrDefault(c => c.Id == subCategoryId);
            return subCategory;
        }
    }
}
