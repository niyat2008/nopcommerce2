 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Message
{
    public class MessageModel
    {
        public int PostId { get; set; }
        public int FromUserId { get; set; }
        public string Message  { get; set; } 
        public int Type  { get; set; }
        public int ToUserId { get; set; }
    }
}
