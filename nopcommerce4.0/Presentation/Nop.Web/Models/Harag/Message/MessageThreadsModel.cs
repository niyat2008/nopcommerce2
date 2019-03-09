using Nop.Web.Framework.Mvc.Models;
using Nop.Web.Models.Harag.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Harag.Message
{
    public class MessageThreadsModel : BaseNopEntityModel
    {
        public int postId { get; set; }
        public string title { get; set; }
        public string LastMessageText { get; set; } 
        public string MessageUser { get; set; } 
        public int MessageUserId { get; set; } 
        public DateTime LastMessageTime { get; set; }
        public string UserName { get; set; }
        public string SenderName { get; set; }  
    }
}
