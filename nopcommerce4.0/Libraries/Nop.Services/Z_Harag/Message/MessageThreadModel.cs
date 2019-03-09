using System;

namespace Nop.Services.Z_Harag.Message
{
    public class MessageThreadModel
        { 
        public int? PostId { get; set; }
        public int? CustomerId { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int UserId { get; set; }
        public int MessageType { get; set; }
        public string MessageTitle { get; set; } 
        public string SentFromName { get; set; }
        public string PostTitle { get; set; } 
        public string SentToName { get; set; }

    }
}
