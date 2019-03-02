using System;

namespace Nop.Services.Z_Harag.Comment
{
    public class UserForNotifyModel
    {
        public int CustomerId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentTime { get; set; }
        public int PostId { get; set; }
        public int OwnerId { get; set; } 
    }
}