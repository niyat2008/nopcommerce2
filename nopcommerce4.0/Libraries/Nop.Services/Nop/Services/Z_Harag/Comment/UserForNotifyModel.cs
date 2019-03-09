using System;

namespace Nop.Services.Z_Harag.Comment
{
    public class UserForNotifyModel
    {
        public int CustomerId { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }
        public int PostId { get; set; }
        public int OwnerId { get; set; } 
    }
}