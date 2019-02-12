using Microsoft.AspNetCore.Hosting;
using Nop.Core.Data;
using Nop.Core.Domain.Z_Consultant;
using Nop.Services.Events;
using Nop.Services.Localization;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using Nop.Services.Z_ConsultantAdmin.Customers;

namespace Nop.Services.Z_ConsultantAdmin.Post
{
    public class PostService : IPostService
    {
        #region Fields

        private readonly IRepository<Z_Consultant_Post> _postRepository;
        private readonly IRepository<Z_Consultant_Category> _categoryRepository;
        private readonly IRepository<Z_Consultant_Comment> _commentRepository;
        private readonly IRepository<Z_Consultant_Photo> _photoRepository;
        #endregion
        #region Ctor
        public PostService(IRepository<Z_Consultant_Post> postRepository,
           IRepository<Z_Consultant_Category> categoryRepository,
           IRepository<Z_Consultant_Comment> commentRepository,
           IRepository<Z_Consultant_Photo> photoRepository)
        {
            this._postRepository = postRepository;
            this._categoryRepository = categoryRepository;
            this._commentRepository = commentRepository;
            this._photoRepository = photoRepository;

        }
        #endregion
        #region Methods
        //Closed && Displayed
        public List<Z_Consultant_Post> GetClosedDisplayedPosts(int start,int length, string searchValue,string sortColumnName,string sortDirection)
        {
            var query = _postRepository.Table.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsClosed == true && p.IsDispayed == true);

            query = query.OrderBy(p => p.DateCreated);

            //filter

            if (!string.IsNullOrEmpty(searchValue)) 
            {
                query = query.Where(p=>p.Title.ToLower().Contains(searchValue.ToLower()) || p.Text.ToLower().Contains(searchValue.ToLower()));
            }

            //sorting
            if(!string.IsNullOrEmpty(sortColumnName)&& !string.IsNullOrEmpty(sortDirection))
            {
              
                query = query.OrderBy(sortColumnName +" "+ sortDirection);

            }
            
            //paging
            query = query.Skip(start).Take(length);

            return query.ToList();

        }

        //Closed && Displayed With Category ID
        public List<Z_Consultant_Post> GetClosedDisplayedPostsWithCategoryId(int categoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsClosed == true && p.IsDispayed == true && (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId));
            query = query.OrderByDescending(p => p.DateCreated);

            return query.ToList();

        }
        //Closed && Displayed With SubCategory ID
        public List<Z_Consultant_Post> GetClosedDisplayedPostsWithSubCategoryId(int subCategoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsClosed == true && p.IsDispayed == true && p.SubCategoryId == subCategoryId);
            query = query.OrderByDescending(p => p.DateCreated);
            return query.ToList();

        }
        //Get  Post By Id
        public List<Z_Consultant_Post> GetPostById(int postId, string type)
        {
            if (type == "ClosedDisplayed")
            {
                //var post = _postRepository.TableNoTracking.Where(p => p.Id == postId && p.IsClosed==true && p.IsDispayed==true);
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.IsClosed == true && p.IsDispayed == true).ToList();
            }
            if (type == "ClosedNotDisplayed")
            {
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.IsClosed == true && p.IsDispayed == false).ToList();
            }
            if (type == "NotReplied")
            {
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.IsAnswered == false).ToList();
            }
            if (type == "Reserved")
            {
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.IsReserved == true).ToList();
            }
            if (type == "Open")
            {
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.IsClosed == false && p.IsDispayed == false && p.IsAnswered == true).ToList();
            }



            return null;
        }
        //Remove Closed Displayed Post
        public void RemovePost(int postId)
        {

            var post = _postRepository.Table.Where(p => p.Id == postId).FirstOrDefault();
            if (post != null)
            {
                var comments = _commentRepository.Table.Where(c => c.PostId == postId).ToList();
                if (comments != null)
                    _commentRepository.Delete(comments);

                var photos = _photoRepository.Table.Where(c => c.PostId == postId).ToList();
                if (photos != null)
                {

                    _photoRepository.Delete(photos);
                    foreach (var photo in photos)
                    {

                    }
                }
                _postRepository.Delete(post);
            }

        }
        //Get Photos
        public List<Z_Consultant_Photo> GetPhotos(int postId)
        {
            var photos = _photoRepository.TableNoTracking.Where(p => p.PostId == postId);
            return photos.ToList();
        }
        //Get Closed Displayed Post Details
        public Z_Consultant_Post GetPostDetails(int postId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Include(p => p.Photos).Where(p => p.Id == postId).FirstOrDefault();
            //if(query !=null)
            //{
            //    var comments = _commentRepository.TableNoTracking.Where(p => p.PostId == postId).ToList();
            //    if(comments !=null)
            //    {
            //        var photos = _photoRepository.TableNoTracking.Where(p => p.PostId == postId).ToList();

            //    }
            //}
            return query;

        }
        //Get Post Comments
        public List<Z_Consultant_Comment> GetPostComments(int postId)
        {
            var comments = _commentRepository.TableNoTracking.Include(p => p.Customer).Where(p => p.PostId == postId);
            return comments.ToList();
        }
        //Get Posts By Date
        public List<Z_Consultant_Post> GetPostsByDate(DateTime firstDate, DateTime secondDate, string type)
        {
            if (type == "ClosedDisplayed")
            {
                return _postRepository.TableNoTracking.Include(p => p.Customer).Include(p => p.Photos).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.IsClosed == true && p.IsDispayed == true && p.DateCreated >= firstDate && p.DateCreated <= secondDate).OrderByDescending(p => p.DateCreated).ToList();
            }
            if (type == "ClosedNotDisplayed")
            {
                return _postRepository.TableNoTracking.Include(p => p.Customer).Include(p => p.Photos).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.DateCreated >= firstDate && p.DateCreated <= secondDate && p.IsClosed == true && p.IsDispayed == false).OrderByDescending(p => p.DateCreated).ToList();
            }
            if (type == "NotReplied")
            {
                return _postRepository.TableNoTracking.Include(p => p.Customer).Include(p => p.Photos).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.DateCreated >= firstDate && p.DateCreated <= secondDate && p.IsAnswered == false).OrderByDescending(p => p.DateCreated).ToList();
            }
            if (type == "Reserved")
            {
                return _postRepository.TableNoTracking.Include(p => p.Customer).Include(p => p.Photos).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.DateCreated >= firstDate && p.DateCreated <= secondDate && p.IsReserved == true).OrderByDescending(p => p.DateCreated).ToList();
            }
            if (type == "Open")
            {
                return _postRepository.TableNoTracking.Include(p => p.Customer).Include(p => p.Photos).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.DateCreated >= firstDate && p.DateCreated <= secondDate && p.IsAnswered == true && p.IsClosed == false && p.IsDispayed == false).OrderByDescending(p => p.DateCreated).ToList();
            }
            return null;
        }
        //Post Update
        public void SetPostToCommon(int id)
        {
            var post = _postRepository.Table.Where(p => p.Id == id).FirstOrDefault();
            if (post != null)
            {
                post.IsCommon = true;
            }
            _postRepository.Update(post);
        }



            //Closed && Not Displayed
            public List<Z_Consultant_Post> GetClosedNotDisplayedPosts(int start, int length, string searchValue, string sortColumnName, string sortDirection)
            {
                var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsClosed == true && p.IsDispayed == false);
                query = query.OrderByDescending(p => p.DateCreated);

            //filter

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(p => p.Title.ToLower().Contains(searchValue.ToLower()) || p.Text.ToLower().Contains(searchValue.ToLower()));
            }

            //sorting
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
               
                    query = query.OrderBy(sortColumnName + " " + sortDirection);

            }

            //paging
            query = query.Skip(start).Take(length);

            return query.ToList();
            }
            //Closed && Not Displayed With Category Id
            public List<Z_Consultant_Post> GetClosedNotDisplayedPostsWithCategoryId(int categoryId)
            {
                var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsClosed == true && p.IsDispayed == false && (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId));
                query = query.OrderByDescending(p => p.DateCreated);
                return query.ToList();
            }
            //Closed && Not Displayed With SubCategory Id
            public List<Z_Consultant_Post> GetClosedNotDisplayedPostsWithSubCategoryId(int subCategoryId)
            {
                var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsClosed == true && p.IsDispayed == false && p.SubCategoryId == subCategoryId);
                query = query.OrderByDescending(p => p.DateCreated);
                return query.ToList();
            }
            // Post Display
            public void PostDisplay(int postId)
            {
                var query = _postRepository.Table.Where(p => p.Id == postId).FirstOrDefault();
                query.IsDispayed = true;
                _postRepository.Update(query);

            }




            //Not Replied
            public List<Z_Consultant_Post> GetNotRepliedPosts(int start, int length, string searchValue, string sortColumnName, string sortDirection)
            {
                var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsAnswered == false);
                query = query.OrderByDescending(p => p.DateCreated);


            //filter

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(p => p.Title.ToLower().Contains(searchValue.ToLower()) || p.Text.ToLower().Contains(searchValue.ToLower()));
            }

            //sorting
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
               
                    query = query.OrderBy(sortColumnName + " " + sortDirection);

            }

            //paging
            query = query.Skip(start).Take(length);


            return query.ToList();
            }
            //Not Replied With Category Id
            public List<Z_Consultant_Post> GetNotRepliedPostsWithCategoryId(int categoryId)
            {
                var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsAnswered == false && (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId));
                query = query.OrderByDescending(p => p.DateCreated);
                return query.ToList();
            }
            //Not Replied With SubCategory Id
            public List<Z_Consultant_Post> GetNotRepliedPostsWithSubCategoryId(int subCategoryId)
            {
                var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsAnswered == false && p.SubCategoryId == subCategoryId);
                query = query.OrderByDescending(p => p.DateCreated);
                return query.ToList();
            }






            //IS Reserved
            public List<Z_Consultant_Post> GetIsReservedPosts(int start, int length, string searchValue, string sortColumnName, string sortDirection)
            {
                var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsReserved == true);
                query = query.OrderByDescending(p => p.DateCreated);

            //filter

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(p => p.Title.ToLower().Contains(searchValue.ToLower()) || p.Text.ToLower().Contains(searchValue.ToLower()));
            }

            //sorting
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
               
                    query = query.OrderBy(sortColumnName + " " + sortDirection);

            }

            //paging
            query = query.Skip(start).Take(length);


            return query.ToList();
            }
            //IS Reserved With Category Id
            public List<Z_Consultant_Post> GetIsReservedPostsWithCategoryId(int categoryId)
            {
                var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsReserved == true && (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId));
                query = query.OrderByDescending(p => p.DateCreated);
                return query.ToList();
            }
            //IS Reserved With SubCategory Id
            public List<Z_Consultant_Post> GetIsReservedPostsWithSubCategoryId(int subCategoryId)
            {
                var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsReserved == true && p.SubCategoryId == subCategoryId);
                query = query.OrderByDescending(p => p.DateCreated);
                return query.ToList();
            }
            // Post Cancel Reserving
            public void PostCancelReserving(int postId)
            {
                var query = _postRepository.Table.Where(p => p.Id == postId).FirstOrDefault();
                query.IsReserved = false;
                _postRepository.Update(query);

            }



            //Open Posts
            public List<Z_Consultant_Post> GetOpenPosts(int start, int length, string searchValue, string sortColumnName, string sortDirection)
            {
                var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsAnswered == true && p.IsClosed == false && p.IsDispayed == false);
                query = query.OrderByDescending(p => p.DateCreated);

            //filter

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(p => p.Title.ToLower().Contains(searchValue.ToLower()) || p.Text.ToLower().Contains(searchValue.ToLower()));
            }

            //sorting
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
                
                    query = query.OrderBy(sortColumnName + " " + sortDirection);

            }

            //paging
            query = query.Skip(start).Take(length);


            return query.ToList();
            }
            //Open Posts With Category Id
            public List<Z_Consultant_Post> GetOpenPostsWithCategoryId(int categoryId)
            {
                var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsAnswered == true && p.IsClosed == false && p.IsDispayed == true && (p.CategoryId == categoryId || p.SubCategory.CategoryId == categoryId));
                query = query.OrderByDescending(p => p.DateCreated);
                return query.ToList();
            }
            //Open Posts With SubCategory Id
            public List<Z_Consultant_Post> GetOpenPostsWithSubCategoryId(int subCategoryId)
            {
                var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsAnswered == true && p.IsClosed == false && p.IsDispayed == true && p.SubCategoryId == subCategoryId);
                query = query.OrderByDescending(p => p.DateCreated);
                return query.ToList();
            }



        //Get Posts By Customer Id
       public List<Z_Consultant_Post> GetCustomerPosts(int CustomerId, string type, int start, int length, string searchValue, string sortColumnName, string sortDirection, DateTime? from = null, DateTime? to = null)
        {
            var posts = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer);

            // All
            if((type=="All" || type==null ) &&  from !=null && to !=null)
            {
                posts = posts.Where(p => p.CustomerId == CustomerId && p.DateCreated >= from && p.DateCreated <= to);
            }

            if (type == "All" && from == null && to == null)
            {
                posts = posts.Where(p => p.CustomerId == CustomerId);
            }

            //Closed Displayed
            if (type == "ClosedDisplayed" && from != null && to != null)
            {
                posts = posts.Where(p => p.CustomerId == CustomerId && p.DateCreated >= from && p.DateCreated <= to && p.IsClosed==true && p.IsDispayed==true );
            }

            if (type == "ClosedDisplayed" && from == null && to == null)
            {
                posts = posts.Where(p => p.CustomerId == CustomerId && p.IsClosed == true && p.IsDispayed == true);
            }

            //Closed Not Displayed
            if (type == "ClosedNotDisplayed" && from != null && to != null)
            {
                posts = posts.Where(p => p.CustomerId == CustomerId && p.DateCreated >= from && p.DateCreated <= to && p.IsClosed == true && p.IsDispayed == false );
            }

            if (type == "ClosedNotDisplayed" && from == null && to == null)
            {
                posts = posts.Where(p => p.CustomerId == CustomerId && p.IsClosed == true && p.IsDispayed == false);
            }

            // Not Replied
            if (type == "NotReplied" && from != null && to != null)
            {
                posts = posts.Where(p => p.CustomerId == CustomerId && p.DateCreated >= from && p.DateCreated <= to && p.IsAnswered == false );
            }

            if (type == "NotReplied" && from == null && to == null)
            {
                posts = posts.Where(p => p.CustomerId == CustomerId && p.IsAnswered == false );
            }

            // Reserved
            if (type == "Reserved" && from != null && to != null)
            {
                posts = posts.Where(p => p.CustomerId == CustomerId && p.DateCreated >= from && p.DateCreated <= to  && p.IsReserved==true );
            }

            if (type == "Reserved" && from == null && to == null)
            {
                posts = posts.Where(p => p.CustomerId == CustomerId && p.DateCreated <= to && p.IsReserved == true);
            }

            // Open
            if (type == "Open" && from != null && to != null)
            {
                posts = posts.Where(p => p.CustomerId == CustomerId && p.DateCreated >= from && p.DateCreated <= to && p.IsAnswered == true && p.IsClosed==false && p.IsDispayed==false );
            }

            if (type == "Open" && from == null && to == null)
            {
                posts = posts.Where(p => p.CustomerId == CustomerId && p.IsAnswered == true && p.IsClosed == false && p.IsDispayed == false);
            }


            //filter

            if (!string.IsNullOrEmpty(searchValue))
            {
                posts = posts.Where(p => p.Title.ToLower().Contains(searchValue.ToLower()) || p.Text.ToLower().Contains(searchValue.ToLower())|| p.Customer.SystemName.ToLower().Contains(searchValue.ToLower()));
            }

            //sorting
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
                
                    posts = posts.OrderBy(sortColumnName + " " + sortDirection);

            }

            //paging
            posts = posts.OrderBy(p=>p.Id).Skip(start).Take(length);

            return posts.ToList();

        }
        //Get Customer Post By PostId and CustomerId
       public List<Z_Consultant_Post> GetCustomerPostById(int customerId, int postId,string type)
        {
            if ((type == "All" || type == null) )
            {
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.CustomerId==customerId).ToList();
            }
            if (type == "ClosedDisplayed")
            {
                //var post = _postRepository.TableNoTracking.Where(p => p.Id == postId && p.IsClosed==true && p.IsDispayed==true);
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.IsClosed == true && p.IsDispayed == true && p.CustomerId==customerId).ToList();
            }
            if (type == "ClosedNotDisplayed")
            {
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.IsClosed == true && p.IsDispayed == false && p.CustomerId == customerId).ToList();
            }
            if (type == "NotReplied")
            {
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.IsAnswered == false && p.CustomerId == customerId).ToList();
            }
            if (type == "Reserved")
            {
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.IsReserved == true && p.CustomerId == customerId).ToList();
            }
            if (type == "Open")
            {
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.IsClosed == false && p.IsDispayed == false && p.IsAnswered == true && p.CustomerId == customerId).ToList();
            }

            return null;
        }


        //Get Posts By Consultant Id
        public List<Z_Consultant_Post> GetConsultantPosts(int ConsultantId, string type, int start, int length, string searchValue, string sortColumnName, string sortDirection, DateTime? from = null, DateTime? to = null)
        {
            var posts = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p=>p.Consultant);

            // All
            if ((type == "All" || type == null) && from != null && to != null)
            {
                posts = posts.Where(p => p.ConsultantId == ConsultantId && p.DateCreated >= from && p.DateCreated <= to);
            }

            if (type == "All" && from == null && to == null)
            {
                posts = posts.Where(p => p.ConsultantId == ConsultantId);
            }

            //Closed Displayed
            if (type == "ClosedDisplayed" && from != null && to != null)
            {
                posts = posts.Where(p => p.ConsultantId == ConsultantId && p.DateCreated >= from && p.DateCreated <= to && p.IsClosed == true && p.IsDispayed == true);
            }

            if (type == "ClosedDisplayed" && from == null && to == null)
            {
                posts = posts.Where(p => p.ConsultantId == ConsultantId && p.IsClosed == true && p.IsDispayed == true);
            }

            //Closed Not Displayed
            if (type == "ClosedNotDisplayed" && from != null && to != null)
            {
                posts = posts.Where(p => p.ConsultantId == ConsultantId && p.DateCreated >= from && p.DateCreated <= to && p.IsClosed == true && p.IsDispayed == false);
            }

            if (type == "ClosedNotDisplayed" && from == null && to == null)
            {
                posts = posts.Where(p => p.ConsultantId == ConsultantId && p.IsClosed == true && p.IsDispayed == false);
            }

            // Not Replied
            if (type == "NotReplied" && from != null && to != null)
            {
                posts = posts.Where(p => p.ConsultantId == ConsultantId && p.DateCreated >= from && p.DateCreated <= to && p.IsAnswered == false);
            }

            if (type == "NotReplied" && from == null && to == null)
            {
                posts = posts.Where(p => p.ConsultantId == ConsultantId && p.IsAnswered == false);
            }

            // Reserved
            if (type == "Reserved" && from != null && to != null)
            {
                posts = posts.Where(p => p.ConsultantId == ConsultantId && p.DateCreated >= from && p.DateCreated <= to && p.IsReserved == true);
            }

            if (type == "Reserved" && from == null && to == null)
            {
                posts = posts.Where(p => p.ConsultantId == ConsultantId && p.DateCreated <= to && p.IsReserved == true);
            }

            // Open
            if (type == "Open" && from != null && to != null)
            {
                posts = posts.Where(p => p.ConsultantId == ConsultantId && p.DateCreated >= from && p.DateCreated <= to && p.IsAnswered == true && p.IsClosed == false && p.IsDispayed == false);
            }

            if (type == "Open" && from == null && to == null)
            {
                posts = posts.Where(p => p.ConsultantId == ConsultantId && p.IsAnswered == true && p.IsClosed == false && p.IsDispayed == false);
            }


            //filter

            if (!string.IsNullOrEmpty(searchValue))
            {
                posts = posts.Where(p => p.Title.ToLower().Contains(searchValue.ToLower()) || p.Text.ToLower().Contains(searchValue.ToLower()) || p.Customer.SystemName.ToLower().Contains(searchValue.ToLower()));
            }

            //sorting
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
                
                    posts = posts.OrderBy(sortColumnName + " " + sortDirection);

            }

            //paging
            posts = posts.OrderBy(p => p.Id).Skip(start).Take(length);

            return posts.ToList();

        }
        //Get Consultant Post By PostId and ConsultantId
        public List<Z_Consultant_Post> GetConsultantPostById(int ConsultantId, int postId, string type)
        {
            if ((type == "All" || type == null))
            {
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p=>p.Consultant).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.ConsultantId == ConsultantId).ToList();
            }
            if (type == "ClosedDisplayed")
            {
                //var post = _postRepository.TableNoTracking.Where(p => p.Id == postId && p.IsClosed==true && p.IsDispayed==true);
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.IsClosed == true && p.IsDispayed == true && p.ConsultantId == ConsultantId).ToList();
            }
            if (type == "ClosedNotDisplayed")
            {
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.IsClosed == true && p.IsDispayed == false && p.ConsultantId == ConsultantId).ToList();
            }
            if (type == "NotReplied")
            {
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.IsAnswered == false && p.ConsultantId == ConsultantId).ToList();
            }
            if (type == "Reserved")
            {
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.IsReserved == true && p.ConsultantId == ConsultantId).ToList();
            }
            if (type == "Open")
            {
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.IsClosed == false && p.IsDispayed == false && p.IsAnswered == true && p.ConsultantId == ConsultantId).ToList();
            }

            return null;
        }


        //Get Top 20 Member By Posts number
       public List<Top20CustomerByPostsNumber> GetTop20MemberByPostsNumber()
        {
            var query = _postRepository.TableNoTracking.Include(c => c.Customer).GroupBy(p => p.CustomerId).OrderByDescending(i => i.Count()).Select(m => new Top20CustomerByPostsNumber
            {
                Id=m.FirstOrDefault().Customer.Id,
                Name=m.FirstOrDefault().Customer.SystemName,
                Email=m.FirstOrDefault().Customer.Email,
                Phone=m.FirstOrDefault().Customer.Mobile,
                PostsCount=m.Count()
            });

            query = query.Take(20);
            

            return query.ToList();

        }

        //Get Top 20 Consultant By Posts number
        public List<Top20CustomerByPostsNumber> GetTop20ConsultantByPostsNumber()
        {
            var query = _postRepository.TableNoTracking.Include(c => c.Consultant).Where(x=>x.ConsultantId != null).GroupBy(p => p.ConsultantId).OrderByDescending(c => c.Count()).Select(m => new Top20CustomerByPostsNumber {
                Id = m.FirstOrDefault().Consultant.Id,
                Name = m.FirstOrDefault().Consultant.SystemName,
                Email = m.FirstOrDefault().Consultant.Email,
                Phone = m.FirstOrDefault().Consultant.Mobile,
                PostsCount = m.Count(),
                EvaluateCount=m.Where(x=>x.Rate !=null).Count()

            });

           

            return query.ToList();
        }



        //Get  Posts number
        public int GetPostsNumber()
        {
            var query = _postRepository.TableNoTracking.Count();

            return query;
        }
        //Get  Members number
      

        #endregion
    }
    }
