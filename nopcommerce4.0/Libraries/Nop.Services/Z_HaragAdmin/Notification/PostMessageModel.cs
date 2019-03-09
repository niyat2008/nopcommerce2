using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.Notification
{
   public class PostMessageModel
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int? UserId { get; set; }
        public string Content { get; set; }
    }
}
