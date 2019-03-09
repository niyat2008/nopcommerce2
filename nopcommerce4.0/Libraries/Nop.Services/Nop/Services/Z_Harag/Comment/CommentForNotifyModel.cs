using System;

namespace Nop.Services.Z_Harag.Comment
{
    public class CommentForNotifyModel
    {
        public int CustomerId { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public int PostId { get; set; } 
        public int OwnerId { get; set; }
        public int PostOwner { get; set; }
    }
}