using System;

namespace Nop.Services.Z_Harag.Comment
{
    public class CommentForNotifyModel
    {
        public int CommentId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentTime { get; set; }
        public int PostId { get; set; } 
        public int OwnerId { get; set; }
    }
}