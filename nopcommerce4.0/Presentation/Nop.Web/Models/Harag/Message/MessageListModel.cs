using Nop.Web.Framework.Mvc.Models;
using Nop.Web.Models.Harag.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Harag.Message
{
    public class MessageListModel : BaseNopEntityModel
    {
        public PostModel Post { get; set; } 
        public List<MessageOutputModel> Messages  { get; set; }
    }

    public class MessageThreadListModel : BaseNopEntityModel
    {
        public PostModel Post { get; set; }
        public List<MessageOutputModel> Messages { get; set; }
    }
}
