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

namespace Nop.Services.Z_ConsultantAdmin.Post
{
   public class PostService:IPostService
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
        public List<Z_Consultant_Post> GetClosedDisplayedPosts()
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p=>p.SubCategory).Include(p => p.Customer).Where(p => p.IsClosed == true && p.IsDispayed == true);
            query = query.OrderByDescending(p => p.DateCreated);
            return query.ToList();
               
        }

        //Closed && Displayed With Category ID
        public List<Z_Consultant_Post> GetClosedDisplayedPostsWithCategoryId(int categoryId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsClosed == true && p.IsDispayed == true && (p.CategoryId==categoryId||p.SubCategory.CategoryId==categoryId));
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
        public List<Z_Consultant_Post> GetPostById(int postId,string type)
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
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.IsAnswered==false).ToList();
            }
            if (type == "Reserved")
            {
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.IsReserved==true).ToList();
            }
            if (type == "Open")
            {
                return _postRepository.TableNoTracking.Include(p => p.Photos).Include(p => p.Customer).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.Id == postId && p.IsClosed == false && p.IsDispayed == false &&p.IsAnswered==true).ToList();
            }



            return null;
        }
        //Remove Closed Displayed Post
        public void RemovePost(int postId)
        {

            var post = _postRepository.Table.Where(p => p.Id == postId).FirstOrDefault();
            if (post != null)
            {
                var comments=_commentRepository.Table.Where(c => c.PostId==postId).ToList();
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
            var query = _postRepository.TableNoTracking.Include(p=>p.Category).Include(p=>p.SubCategory).Include(p=>p.Customer).Include(p=>p.Photos).Where(p => p.Id == postId).FirstOrDefault();
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
            var comments = _commentRepository.TableNoTracking.Include(p=>p.Customer).Where(p => p.PostId == postId);
            return comments.ToList();
        }
        //Get Posts By Date
        public List<Z_Consultant_Post> GetPostsByDate(DateTime firstDate, DateTime secondDate,string type)
        {
            if (type == "ClosedDisplayed")
            {
                return _postRepository.TableNoTracking.Include(p => p.Customer).Include(p => p.Photos).Include(p => p.Category).Include(p => p.SubCategory).Where( p=> p.IsClosed == true && p.IsDispayed == true && p.DateCreated >= firstDate && p.DateCreated <= secondDate).OrderByDescending(p=>p.DateCreated).ToList();
            }
            if (type == "ClosedNotDisplayed")
            {
                return _postRepository.TableNoTracking.Include(p => p.Customer).Include(p => p.Photos).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.DateCreated >= firstDate && p.DateCreated <= secondDate && p.IsClosed == true && p.IsDispayed == false).OrderByDescending(p => p.DateCreated).ToList();
            }
            if (type == "NotReplied")
            {
                return _postRepository.TableNoTracking.Include(p => p.Customer).Include(p => p.Photos).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.DateCreated >= firstDate && p.DateCreated <= secondDate && p.IsAnswered==false).OrderByDescending(p=>p.DateCreated).ToList();
            }
            if (type == "Reserved")
            {
                return _postRepository.TableNoTracking.Include(p => p.Customer).Include(p => p.Photos).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.DateCreated >= firstDate && p.DateCreated <= secondDate && p.IsReserved==true).OrderByDescending(p => p.DateCreated).ToList();
            }
            if (type == "Open")
            {
                return _postRepository.TableNoTracking.Include(p => p.Customer).Include(p => p.Photos).Include(p => p.Category).Include(p => p.SubCategory).Where(p => p.DateCreated >= firstDate && p.DateCreated <= secondDate &&p.IsAnswered==true && p.IsClosed == false && p.IsDispayed == false).OrderByDescending(p => p.DateCreated).ToList();
            }
            return null;
        }





        //Closed && Not Displayed
        public List<Z_Consultant_Post> GetClosedNotDisplayedPosts()
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsClosed == true && p.IsDispayed == false);
            query = query.OrderByDescending(p => p.DateCreated);
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
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsClosed == true && p.IsDispayed == false && p.SubCategoryId==subCategoryId);
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
        public List<Z_Consultant_Post> GetNotRepliedPosts()
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsAnswered == false);
            query = query.OrderByDescending(p => p.DateCreated);
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
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsAnswered == false && p.SubCategoryId==subCategoryId);
            query = query.OrderByDescending(p => p.DateCreated);
            return query.ToList();
        }






        //IS Reserved
        public List<Z_Consultant_Post> GetIsReservedPosts()
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsReserved == true);
            query = query.OrderByDescending(p => p.DateCreated);
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
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsReserved == true && p.SubCategoryId==subCategoryId);
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
        public List<Z_Consultant_Post> GetOpenPosts()
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsAnswered == true && p.IsClosed == false && p.IsDispayed == true);
            query = query.OrderByDescending(p => p.DateCreated);
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
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.SubCategory).Include(p => p.Customer).Where(p => p.IsAnswered == true && p.IsClosed == false && p.IsDispayed == true && p.SubCategoryId==subCategoryId);
            query = query.OrderByDescending(p => p.DateCreated);
            return query.ToList();
        }
        #endregion
    }
}
