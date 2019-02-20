using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Harag.Message
{
    public class MessageOutputModel : BaseNopEntityModel
    {
        public int postId { get; set; }
        public int userId { get; set; }
        public string User { get; set; }
        public DateTime DateTime { get; set; }
        public string Message  { get; set; }
    }
}
