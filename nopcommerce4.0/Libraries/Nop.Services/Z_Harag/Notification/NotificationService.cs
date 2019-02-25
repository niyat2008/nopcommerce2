using Microsoft.AspNetCore.Hosting;
using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Events;
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


    }
}
