using Nop.Core.Domain.Z_Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Consultant.Notification
{
   public interface INotificationService
    {
        //Add Notification
        bool AddPostNotification(NotificationModel model, List<int> consultantId);

        //Add Comment Notification
        bool AddCommentNotification(NotificationModel model);

        //Get Notifications
        List<Z_Consultant_Notification> GetNotifications(int userId);

        //Update Notification
         void UpdateNotification(int customerId);

        //Get Un Read Notifications
         int GetUnReadNotifications(int userId);
        void DeleteNotifications(int id);
    }
}
