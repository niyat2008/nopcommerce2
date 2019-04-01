using Nop.Core.Data;
using Nop.Core.Domain.Z_Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using Nop.Services.Z_ConsultantAdmin.SubCategories;

namespace Nop.Services.Z_ConsultantAdmin.Categories
{
    public class CategoryService : ICategoryService
    {
        #region  Fields
        private readonly IRepository<Z_Consultant_Category> _categoryRepository;
        private readonly IRepository<Z_Consultant_SubCategory> _subCategoryRepository;
        private readonly IRepository<Z_Consultant_Post> _postRepository;
        private readonly IRepository<Z_Consultant_Comment> _commentRepository;
        private readonly IRepository<Z_Consultant_Photo> _photoRepository;
        #endregion

        #region Ctor
        public CategoryService(IRepository<Z_Consultant_Category> categoryRepository, IRepository<Z_Consultant_SubCategory> subCategoryRepository, IRepository<Z_Consultant_Post> postRepository, IRepository<Z_Consultant_Comment> commentRepository, IRepository<Z_Consultant_Photo> photoRepository)
        {
            this._categoryRepository = categoryRepository;
            this._subCategoryRepository = subCategoryRepository;
            this._postRepository = postRepository;
            this._commentRepository = commentRepository;
            this._photoRepository = photoRepository;
        }
        #endregion

        #region Methods
        //Get ALL Categories For DataTable
        public List<Z_Consultant_Category> GetAllCategories(int start, int length, string searchValue, string sortColumnName, string sortDirection)
        {
            var category = _categoryRepository.TableNoTracking;
            category.OrderBy(c => c.Id);

            if (!string.IsNullOrEmpty(searchValue))
            {
                category = category.Where(c => c.Name.ToLower().Contains(searchValue.ToLower()));
            }

            //sorting
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
                if (sortDirection == "des")
                    category = category.OrderBy(sortColumnName + " " + sortDirection);

            }

            //paging
            category = category.OrderByDescending(c => c.DateCreated).Skip(start).Take(length);

            return category.ToList();
        }
        //Add category
       public Z_Consultant_Category AddCategory(CategoryModelForPost category)
        {
            var categoryDb = new Z_Consultant_Category
            {
                Name = category.Name,
                DateCreated = DateTime.Now,
                DateUpdated=DateTime.Now,
                Description=category.Description
            };

            if (categoryDb != null)
            {
                _categoryRepository.Insert(categoryDb);
            }
            return categoryDb;
        }


        //Update category
        public Z_Consultant_Category UpdateCategory(CategoryModelForPost category)
        {
            var query = _categoryRepository.TableNoTracking.AsQueryable();

            // query
            query.Where(m => m.Id == category.Id);

            var categoryDb = query.FirstOrDefault();
            categoryDb.Name = category.Name;
            categoryDb.Description = category.Description;

            categoryDb.DateUpdated = DateTime.Now;

            if (query.FirstOrDefault() == null)
            {
                return null;
            }
            
            
           _categoryRepository.Update(categoryDb);
           
            return categoryDb;
        }


        //Get All Categories
        public List<Z_Consultant_Category> GetAllCategor()
        {
            var allCategories = _categoryRepository.TableNoTracking;
            return allCategories.ToList();
        }
        //Delete Category
        public void DeleteCategory(int categoryId)
        {
            var category = _categoryRepository.Table.Where(c => c.Id == categoryId).FirstOrDefault();

            if (category != null)
            {
                var subcategories = _subCategoryRepository.Table.Where(s => s.CategoryId == categoryId).ToList();


                if (subcategories != null)
                {
                    foreach (var subCategory in subcategories)
                    {
                        var postts = _postRepository.Table.Where(p => p.SubCategoryId == subCategory.Id).ToList();
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



                    }
                    _subCategoryRepository.Delete(subcategories);
                }

                var posts = _postRepository.Table.Where(p => p.CategoryId == categoryId).ToList();
                if (posts != null)
                {
                    foreach (var post in posts)
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



                    _postRepository.Delete(posts);
                }


            }
            _categoryRepository.Delete(category);
        }

        public Z_Consultant_Category GetCategory(int catId)
        {
            var category = _categoryRepository.TableNoTracking.Where(m => m.Id == catId).FirstOrDefault();

            return category;
        }
        #endregion
    }
}
