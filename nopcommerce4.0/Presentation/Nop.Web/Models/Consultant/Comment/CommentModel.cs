using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Consultant.Comment
{
    public class CommentModel : BaseNopEntityModel
    {
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CommentedBy { get; set; }
        public string CommentOwner { get; set; }
        public int PostId { get; set; }
    }
}
