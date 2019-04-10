using Nop.Core.Domain.Z_Harag;
using Nop.Services.Z_Harag.Comment;
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
        bool PushUserPostsNotification(Comment.UserForNotifyModel notifyModel);
        int GetUnSeenNotificationCount(int id);
        bool SetUserNotificationsSeen(int id);
        bool PushRateNotification(int id, int ratedId,  bool adviceDeal);
        bool PushPostOwnerNotification(CommentForNotifyModel adviceDeal);
        bool PushSiteToUserNotification(SiteToUserNotificationModel notificationModel);
        bool PushSiteToAllUserNotification(SiteToUserNotificationModel notificationModel);
        bool DeleteNotifications(int id);
    }
}
