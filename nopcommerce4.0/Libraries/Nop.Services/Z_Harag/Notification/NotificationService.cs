using Microsoft.AspNetCore.Hosting;
using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Notification
{
   public class NotificationService:INotificationService
    {
        private readonly IRepository<Z_Harag_Rate> _notificationService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IHostingEnvironment _env;

        public NotificationService(IRepository<Z_Harag_Rate> notificationService, IEventPublisher eventPublisher, IHostingEnvironment env)
        {
            this._notificationService = notificationService;
            this._eventPublisher = eventPublisher;
            this._env = env;
        }
    }
}
