using Nop.Core.Data;
using Nop.Core.Domain.Z_Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using System.Data.Entity;

namespace Nop.Services.Z_ConsultantAdmin.SubCategories
{

    public class SubCategoryService : ISubCategoryService
    {
        #region Fields
        private readonly IRepository<Z_Consultant_Category> _categoryRepository;
        private readonly IRepository<Z_Consultant_SubCategory> _subcategoryRepository;
        private readonly IRepository<Z_Consultant_Post> _postRepository;
        private readonly IRepository<Z_Consultant_Comment> _commentRepository;
        private readonly IRepository<Z_Consultant_Photo> _photoRepository;
        #endregion
        #region Ctor
        public SubCategoryService(IRepository<Z_Consultant_SubCategory> subcategoryRepository, IRepository<Z_Consultant_Category> categoryRepository, IRepository<Z_Consultant_Post> postRepository, IRepository<Z_Consultant_Comment> commentRepository, IRepository<Z_Consultant_Photo> photoRepository)
        {
            this._categoryRepository = categoryRepository;
            this._subcategoryRepository = subcategoryRepository;
            this._postRepository = postRepository;
            this._commentRepository = commentRepository;
            this._photoRepository = photoRepository;
        }
        #endregion

        #region Methods

        //Get SubCategories
        public List<Z_Consultant_SubCategory> GetSubCategories(int start, int length, string searchValue, string sortColumnName, string sortDirection)
        {
            var subCategory = _subcategoryRepository.TableNoTracking.Include(c => c.Category);

            if (!string.IsNullOrEmpty(searchValue))
            {
                subCategory = subCategory.Where(c => c.Name.ToLower().Contains(searchValue.ToLower()));
            }

            //sorting
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
                if (sortDirection == "des")
                    subCategory = subCategory.OrderBy(sortColumnName + " " + sortDirection);

            }

            //paging
            subCategory = subCategory.OrderByDescending(c => c.DateCreated).Skip(start).Take(length);

            return subCategory.ToList();
        }
        //Get SubCategories With Category ID
        public List<Z_Consultant_SubCategory> GetSubCategoriesByCategoryId(int categoryId, int start, int length, string searchValue, string sortColumnName, string sortDirection)
        {
            
            var subcategories = _subcategoryRepository.TableNoTracking.Include(sub=>sub.Category).Where(sub => sub.CategoryId == categoryId);

            if (!string.IsNullOrEmpty(searchValue))
            {
                subcategories = subcategories.Where(c => c.Name.ToLower().Contains(searchValue.ToLower()));
            }

            //sorting
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
                if (sortDirection == "des")
                    subcategories = subcategories.OrderBy(sortColumnName + " " + sortDirection);

            }

            //paging
            subcategories = subcategories.OrderByDescending(c => c.DateCreated).Skip(start).Take(length);

            return subcategories.ToList();

        }
        //Add SubCategory
       public Z_Consultant_SubCategory AddSubCategory(SubCategoryForPost subCategoryModel)
        {
            var subCategory = new Z_Consultant_SubCategory
            {
                Name = subCategoryModel.Name,
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                Description = subCategoryModel.Description,
                CategoryId = subCategoryModel.CategoryId,
                IsActive=subCategoryModel.IsActive
            };
            if(subCategory !=null)
                _subcategoryRepository.Insert(subCategory);

            return subCategory;
        }

        //Update SubCategory
        public Z_Consultant_SubCategory UpdateSubCategory(SubCategoryForPost subCategoryModel)
        {
            var subCategory = _subcategoryRepository.Table.Where(m => m.Id == subCategoryModel.Id).FirstOrDefault();

            if (subCategory == null)
                return null;

            subCategory.Name = subCategoryModel.Name;
            subCategory.DateUpdated = DateTime.Now;
            subCategory.IsActive = subCategoryModel.IsActive;

            if (subCategory != null)
                _subcategoryRepository.Update(subCategory);

            return subCategory;
        }
        //Delete SubCategory
        public void DeleteSubCategory(int subCategoryId)
        {
            var subCategories = _subcategoryRepository.Table.Where(sub => sub.Id == subCategoryId).FirstOrDefault();

            if (subCategories != null)
            {

                var postts = _postRepository.Table.Where(p => p.SubCategoryId == subCategoryId).ToList();
                if (postts != null)
                {
                    foreach (var post in postts)
                    {
                        var comments = _commentRepository.Table.Where(c => c.PostId == post.Id).ToList();
                        if (comments != null)
                            _commentRepository.Delete(comments);

                        var photos = _photoRepository.Table.Where(c => c.PostId == post.Id).ToList();
                        if (photos != null)
                        {

                            _photoRepository.Delete(photos);

                        }
                    }



                    _postRepository.Delete(postts);
                }




                _subcategoryRepository.Delete(subCategories);
            }
        }
        //Get All Categories
        public List<Z_Consultant_Category> GetAllCategor()
        {
            var allCategories = _categoryRepository.TableNoTracking;
            return allCategories.ToList();
        }

        public Z_Consultant_SubCategory GetSubByCategoryId(int subCatId)
        {
            return _subcategoryRepository.GetById(subCatId);
        }

        #endregion
    }
}
