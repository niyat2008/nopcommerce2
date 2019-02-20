using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Harag.Message
{
    public class MessageModel : BaseNopEntityModel
    {
        public int postId { get; set; }
        public int userId { get; set; }
        public string Message  { get; set; } 
    }
}
