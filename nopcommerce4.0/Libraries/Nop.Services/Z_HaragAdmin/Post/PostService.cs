using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Z_Harag;
using Nop.Core.Data;
using System.Data.Entity;
using System.Linq.Dynamic;

namespace Nop.Services.Z_HaragAdmin.Post
{
    public class PostService : IPostService
    {
        #region Fields
        private readonly IRepository<Z_Harag_Post> _postRepository;
        private readonly IRepository<Z_Harag_Message> _messageRepository;
        private readonly IRepository<Z_Harag_Reports> _reportRepository;
        #endregion
        #region Ctor
        public PostService(IRepository<Z_Harag_Post> postRepository, IRepository<Z_Harag_Message> messageRepository, IRepository<Z_Harag_Reports> reportRepository)
        {
            this._postRepository = postRepository;
            this._messageRepository = messageRepository;
            this._reportRepository = reportRepository;
        }
        #endregion

        #region Methods
        //Get All Posts
        public List<Z_Harag_Post> GetAllPosts(int start, int length, string searchValue, string sortColumnName, string sortDirection)
        {
            var posts = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.Customer).Include(p => p.Z_Harag_Photo);

            //filter

            if (!string.IsNullOrEmpty(searchValue))
            {
                posts = posts.Where(p => p.Title.ToLower().Contains(searchValue.ToLower()) || p.Text.ToLower().Contains(searchValue.ToLower()));
            }

            //sorting
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {

                posts = posts.OrderBy(sortColumnName + " " + sortDirection);

            }

            //paging
            posts = posts.OrderByDescending(p=>p.DateCreated).Skip(start).Take(length);

            return posts.ToList();
        }
        //Delete Post
       public void DeletePost(int id)
        {
            var query = _postRepository.Table.Where(p => p.Id == id).FirstOrDefault();
        }
        //post Details
       public Z_Harag_Post PostDetails(int postId)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Category).Include(p => p.Customer).Include(p => p.City).Include(p => p.Z_Harag_Photo).FirstOrDefault();

            return query;
        }
        //post Message
        public List<Z_Harag_Message> GetPostMessage(int postId)
        {
            var query = _messageRepository.TableNoTracking.Where(m => m.PostId == postId);
            return query.ToList();
        }
        //Post Reports
        public List<Z_Harag_Reports> GetPostReports(int postId)
        {
            var query = _reportRepository.TableNoTracking.Where(r => r.PostId == postId);
            return query.ToList();
        }

        //Get Post Reports
        public List<Z_Harag_Reports> GetPostReports(int start, int length, string searchValue, string sortColumnName, string sortDirection)
        {
            var query = _reportRepository.TableNoTracking;

            //search
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(r => r.ReportDescription.Contains(searchValue) || r.ReportTitle.Contains(searchValue));
            }
            //sort
            if (!string.IsNullOrEmpty(sortColumnName) || !string.IsNullOrEmpty(sortDirection))
            {
                query = query.OrderBy(sortColumnName + " " + sortDirection);
            }
            //pagining
            query = query.OrderByDescending(r => r.ReportTitle).Skip(start).Take(length);

            return query.ToList();

        }

        //Get Posts By Date
        //public List<Z_Harag_Post> GetPostsByDate(DateTime date)
        //{
        //    var query = _postRepository.TableNoTracking.FirstOrDefault(p => p.DateCreated == date);
        //}

        //Get Post By ID
        public List<Z_Harag_Post> GetPostById(int postId)
        {
            var post = _postRepository.TableNoTracking.Include(p => p.Customer).Include(p => p.Category).Include(p => p.City).Where(p=>p.Id==postId);

            return post.ToList();
        }
         

        //Get Posts By CategoryId
        public List<Z_Harag_Post> GetPostsByCategory(int categoryId, int start, int length, string searchValue, string sortColumnName, string sortDirection)
        {
            var query = _postRepository.TableNoTracking.Include(p=>p.Customer).Include(p=>p.Category).Include(p=>p.City).Where(p => p.CategoryId == categoryId);

            //search
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(r => r.Text.Contains(searchValue) && r.Title.Contains(searchValue));
            }
            //sort
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
                query = query.OrderBy(sortColumnName + " " + sortDirection);
            }
            //pagining
            query = query.OrderByDescending(r => r.Text).Skip(start).Take(length);

            return query.ToList();

        }

        //Get Posts By City Id
        public List<Z_Harag_Post> GetPostsByCity(int cityId, int start, int length, string searchValue, string sortColumnName, string sortDirection)
        {
            var query = _postRepository.TableNoTracking.Include(p => p.Customer).Include(p => p.Category).Include(p => p.City).Where(p => p.CityId == cityId);

            //search
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(r => r.Text.Contains(searchValue) && r.Title.Contains(searchValue));
            }
            //sort
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
                query = query.OrderBy(sortColumnName + " " + sortDirection);
            }
            //pagining
            query = query.OrderByDescending(r => r.Text).Skip(start).Take(length);

            return query.ToList();
        }


        ////Post Comments
        //public List<Z_Harag_Comment> GetPostComments(int postId)
        //{
        //    var comments = _.TableNoTracking.Include(p => p.Customer).Where(p => p.PostId == postId);
        //    return comments.ToList();
        //}

        #endregion
    }
}
