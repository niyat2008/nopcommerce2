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
                .Include(m => m.Followed)
                .Include(m => m.Post)
                .Include(m => m.Category).Where(m => m.FollowType == (int)FollowType.Post 
            && m.UserId == userId).ToList();
            return followList;
        }

        public List<Z_Harag_Follow> GetFollowedUsers(int userId)
        {
            var followList = _followService.TableNoTracking.Where(m => m.FollowType == (int)FollowType.User
            && m.UserId == userId).ToList();
            return followList;
        }
    }
}
