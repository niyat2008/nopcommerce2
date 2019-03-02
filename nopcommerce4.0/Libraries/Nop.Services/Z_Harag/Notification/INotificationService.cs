using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Notification
{
    public interface INotificationService
    {
        List<Z_Harag_Notification> GetUserNotifications(int userId);

        Z_Harag_Notification AddPostNotification(Z_Harag_Notification n);
        Z_Harag_Notification AddCommentNotification(Z_Harag_Notification n);
        Z_Harag_Notification AddUserNotification(Z_Harag_Notification n);
        Z_Harag_Notification AddGeneralNotification(Z_Harag_Notification n);
        Z_Harag_Notification AddCategoryNotification(Z_Harag_Notification n);
        bool PushPostCommentNotification(Comment.CommentForNotifyModel notifyModel);
    }
}
