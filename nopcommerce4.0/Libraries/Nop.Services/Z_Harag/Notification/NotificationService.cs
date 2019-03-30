using Microsoft.AspNetCore.Hosting;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Services.Z_Harag.Comment;
using Nop.Services.Z_Harag.Follow;
using Nop.Services.Z_Harag.Helpers;
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
        //private readonly ICustomerService _userRepository; 
        private readonly IRepository<Z_Harag_Follow> _followRepository;
        //private readonly IRepository<Customer> _customerRepository;
        private readonly Nop.Services.Z_ConsultantAdmin.Customers.ICustomerService _customerService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IHostingEnvironment _env;

        public NotificationService(IRepository<Z_Harag_Notification> notificationService,
            IRepository<Z_Harag_Follow> _followRepository,
        IEventPublisher eventPublisher, IHostingEnvironment env, Nop.Services.Z_ConsultantAdmin.Customers.ICustomerService customerService)
        {
            this._notificationService = notificationService;
            this._followRepository = _followRepository;
            this._customerService = customerService; 
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
               if(item == notifyModel.PostOwner)
                {
                    this.AddCommentNotification(new Z_Harag_Notification
                    {
                        NotificationType = (int)NotificationType.PostOwnnerComment,
                        OwnerId = item,
                        NotificationTime = notifyModel.Time,
                        PostId = notifyModel.PostId,
                        CustomerId = notifyModel.CustomerId,
                        IsRead = false
                    });
                }
                else
                {
                    this.AddCommentNotification(new Z_Harag_Notification
                    {
                        NotificationType = (int)NotificationType.Comment,
                        OwnerId = item,
                        NotificationTime = notifyModel.Time,
                        PostId = notifyModel.PostId,
                        CustomerId = notifyModel.CustomerId,
                        IsRead = false
                    });
                }
            }
            return true;
        }

        public bool PushUserPostsNotification(Comment.UserForNotifyModel notifyModel)
        {
            var postFollowers = this.GetUsersFollowNotification(notifyModel.CustomerId);

            foreach (var item in postFollowers)
            {
                this.AddCommentNotification(new Z_Harag_Notification
                {
                    NotificationType = (int)NotificationType.Post,
                    OwnerId = item,
                    NotificationTime = notifyModel.Time,
                    PostId = notifyModel.PostId,
                    CustomerId = notifyModel.CustomerId,
                    IsRead = false
                });
            }
            return true;
        }

        public int GetUnSeenNotificationCount(int id)
        {
            return _notificationService.TableNoTracking.Where(m => m.OwnerId == id && m.IsRead == false).ToList().Count;
        }

        public bool SetUserNotificationsSeen(int id)
        {
            var nots =  _notificationService.Table.Where(m => m.OwnerId == id && m.IsRead == false).ToList();

            foreach (var item in nots)
            {
                item.IsRead = true;
                _notificationService.Update(item);
            }

            return true;
        }

        public bool PushRateNotification(int id, bool adviceDeal)
        {
            this.AddCommentNotification(new Z_Harag_Notification
            {
                NotificationType = (int)NotificationType.Rate,
                OwnerId = id,
                NotificationTime = DateTime.Now,
                PostId = 1,
                NotificationContent = adviceDeal.ToString(),
                CustomerId =1,
                IsRead = false
            });

            return true;
        }

        public bool PushPostOwnerNotification(CommentForNotifyModel postCommentModel)
        {
            this.AddCommentNotification(new Z_Harag_Notification
            {
                NotificationType = (int)NotificationType.PostOwnnerComment,
                OwnerId = postCommentModel.PostOwner,
                NotificationTime = DateTime.Now,
                PostId = postCommentModel.PostId, 
                CustomerId = postCommentModel.CustomerId,
                IsRead = false
            });

            return true;
        }

        public bool PushSiteToUserNotification(SiteToUserNotificationModel notificationModel)
        {
            var notification = new Z_Harag_Notification
            {
                NotificationType = (int)NotificationType.General,
                OwnerId = notificationModel.UserId,
                NotificationTime = DateTime.Now, 
                CustomerId = notificationModel.AdminId,
                NotificationContent = notificationModel.Content,
                IsRead = false
            };

            try
            {
                this.AddGeneralNotification(notification);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool PushSiteToAllUserNotification(SiteToUserNotificationModel notificationModel)
        {
            // registered users
            //var users = _userRepository.GetAllCustomers();

            var users = _customerService.GetCustomers();


            foreach (var user in users)
            {

                


                //if (inRole != null && inRole == )
                //{

                //}

                var notification = new Z_Harag_Notification
                {
                    NotificationType = (int)NotificationType.General,
                    OwnerId = user.Id,
                    NotificationTime = DateTime.Now,
                    NotificationContent = notificationModel.Content,
                    CustomerId = notificationModel.AdminId,
                    IsRead = false
                }; 
                this.AddGeneralNotification(notification);
                     
            }
            return true;
        }

        public bool DeleteNotifications(int id)
        {
            var userNotifications = _notificationService.Table.Where(m => m.OwnerId == id).ToList();

            foreach (var item in userNotifications)
            {
                _notificationService.Delete(item);
            }

            return true;
        }
    }
}
