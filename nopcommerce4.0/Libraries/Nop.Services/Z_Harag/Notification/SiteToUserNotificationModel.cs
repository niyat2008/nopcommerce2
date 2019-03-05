namespace Nop.Services.Z_Harag.Notification
{
    public class SiteToUserNotificationModel
    {
        public int UserId { get; set; }
        public int AdminId { get; set; }
        public string Content { get; set; }
    }
}
