using Microsoft.AspNetCore.Hosting;
using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Events;
using Nop.Services.Z_Harag.Follow;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Notification
{
    public class NotificationService:INotificationService
    {
        private readonly IRepository<Z_Harag_Notification> _notificationService;
        private readonly IRepository<Z_Harag_Post> _postRepository;
        private readonly IRepository<Z_Harag_Follow> _followRepository; 

        private readonly IEventPublisher _eventPublisher;
        private readonly IHostingEnvironment _env;

        public NotificationService(IRepository<Z_Harag_Notification> notificationService, IEventPublisher eventPublisher, IHostingEnvironment env)
        {
            this._notificationService = notificationService;
            this._eventPublisher = eventPublisher;
            this._env = env;
        }

        public Z_Harag_Notification AddCategoryNotification(Z_Harag_Notification n)
        {
            _notificationService.Insert(n);
            return n;
        }

        public Z_Harag_Notification AddCommentNotification(Z_Harag_Notification n)
        {
            _notificationService.Insert(n);
            return n;
        }

        public Z_Harag_Notification AddGeneralNotification(Z_Harag_Notification n)
        {
            _notificationService.Insert(n);
            return n;
        }

        public Z_Harag_Notification AddPostNotification(Z_Harag_Notification n)
        {
            _notificationService.Insert(n);
            return n;
        }

        public Z_Harag_Notification AddUserNotification(Z_Harag_Notification n)
        {
            _notificationService.Insert(n);
            return n;
        }

        public List<Z_Harag_Notification> GetUserNotifications(int userId)
        {
            var notifiaction = _notificationService.TableNoTracking
                .Include(m => m.Owner)
                .Include(m => m.Customer)  
                .Include(m => m.Category)
                .Include(m => m.Z_Harag_Post).Where(n => n.OwnerId == userId).ToList();

            return notifiaction;
        }
        
        public List<int> GetUsersFollowNotification(int userId) 
        {
            var list = _followRepository.Table
                .Where(m => m.FollowedId == userId && m.FollowType == (int)FollowType.User)
                .Select(m => (int) m.UserId).ToList();

            return list;
        }

        public List<int> GetPostFollowNotification(int postId)
        {
            var list = _followRepository.Table
                .Where(m => m.PostId == postId && m.FollowType == (int)FollowType.Post)
                .Select(m => (int)m.UserId).ToList();

            return list;
        }

        public bool PushPostCommentNotification(Comment.CommentForNotifyModel notifyModel)
        {
            var postFollowers = this.GetPostFollowNotification(notifyModel.PostId);

            foreach (var item in postFollowers)
            {
                this.AddCommentNotification(new Z_Harag_Notification
                {
                    NotificationType = (int)NotificationType.Comment,
                    OwnerId = item,
                    NotificationTime = notifyModel.CommentTime,
                    PostId = notifyModel.PostId,
                    CustomerId = notifyModel.CommentId
                });
            }
            return true;
        }

        public bool PushUserCommentNotification(Comment.UserForNotifyModel notifyModel)
        {
            var postFollowers = this.GetUsersFollowNotification(notifyModel.PostId);

            foreach (var item in postFollowers)
            {
                this.AddCommentNotification(new Z_Harag_Notification
                {
                    NotificationType = (int)NotificationType.User,
                    OwnerId = notifyModel.OwnerId,
                    NotificationTime = notifyModel.CommentTime,
                    PostId = notifyModel.PostId,
                    CustomerId = notifyModel.CustomerId
                });
            }
            return true;
        } 
    }
}
