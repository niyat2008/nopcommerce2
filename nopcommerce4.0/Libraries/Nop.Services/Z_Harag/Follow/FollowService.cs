using Microsoft.AspNetCore.Hosting;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Follow
{
    public class FollowService: IFollowService
    {
        private readonly IRepository<Z_Harag_Follow> _followService;
 
        private readonly IEventPublisher _eventPublisher;
        private readonly IHostingEnvironment _env;


        public FollowService(IRepository<Z_Harag_Follow> _followService , IEventPublisher eventPublisher, IHostingEnvironment env)
        {
            this._followService = _followService; 
            this._eventPublisher = eventPublisher;
            this._env = env;
        }

        public Z_Harag_Follow AddCategoryToFollow(Z_Harag_Follow catFollow)
        {
            _followService.Insert(catFollow);
            return catFollow;
        }

        public Z_Harag_Follow AddPostToFollow(Z_Harag_Follow postFollow)
        {
            _followService.Insert(postFollow);

            return postFollow;
        }

        public Z_Harag_Follow AddUserToFollow(Z_Harag_Follow userFollow)
        {
            _followService.Insert(userFollow);

            return userFollow;
        }

        public List<Z_Harag_Follow> GetFollowedCategory(int userId)
        {
            var followList = _followService.TableNoTracking.Where(m => m.FollowType == (int)FollowType.Category 
            && m.UserId == userId).ToList();
            return followList;
        }

        public List<Z_Harag_Follow> GetFollowedPosts(int userId)
        {
            var followList = _followService.TableNoTracking
                .Include(m => m.User)  
                .Include(m => m.Post) .Where(m => m.FollowType == (int)FollowType.Post 
            && m.UserId == userId).ToList();
 
            return followList;
        }

        public List<Z_Harag_Follow> GetFollowedUsers(int userId)
        {
            var followList = _followService.TableNoTracking.Include(m => m.User)
                .Include(m => m.Followed).Where(m => m.FollowType == (int)FollowType.User
            && m.UserId == userId).ToList();
            return followList;
        }

        public bool IsCatFollowed(int id, int uid)
        {
            var followList = _followService.TableNoTracking.Where(m => m.FollowType == (int)FollowType.Category
             && m.CategoryId == id && m.UserId == uid).FirstOrDefault();
             
            return followList == null? false : true;
        }

        public bool IsPostFollowed(int id, int uid)
        {
            var followList = _followService.TableNoTracking.Where(m => m.FollowType == (int)FollowType.Post
                && m.PostId == id && m.UserId == uid).FirstOrDefault();

            return followList == null ? false : true;
        }

        public bool IsUserFollowed(int id, int uid)
        {
            var followList = _followService.TableNoTracking.Where(m => m.FollowType == (int)FollowType.User
                && m.FollowedId == id && m.UserId == uid).FirstOrDefault();

            return followList == null ? false : true;
        }

        public bool RemoveCategoryFromFollow(int catId, int userId)
        {
            var followList = _followService.Table.Where(m => m.FollowType == (int)FollowType.Category
              && m.CategoryId == catId && m.UserId == userId).FirstOrDefault();

            if (followList == null)
                return false;

            _followService.Delete(followList);

            return  true;
        }

        public bool RemovePostFromFollow(int postId, int userId)
        {
            var followList = _followService.Table.Where(m => m.FollowType == (int)FollowType.Post
                 && m.PostId == postId && m.UserId == userId).FirstOrDefault();

            if (followList == null)
                return false;

            _followService.Delete(followList);

            return true;
        }

        public bool RemoveUserFromFollow(int id, int userId)
        {
            var followList = _followService.Table.Where(m => m.FollowType == (int)FollowType.User
               && m.FollowedId == id && m.UserId == userId).FirstOrDefault();

            if (followList == null)
                return false;

            _followService.Delete(followList);

            return true;
        }
    }
}
