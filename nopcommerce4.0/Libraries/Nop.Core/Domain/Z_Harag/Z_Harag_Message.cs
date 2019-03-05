using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Harag
{
   public class Z_Harag_Message:BaseEntity
    { 
        public Nullable<int> PostId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> CreatedTime { get; set; }
        public int UserId { get; set; }
        public int MessageType { get; set; }
        public string MessageTitle { get; set; }


        public virtual Customer Customer { get; set; }
        public virtual Customer User { get; set; }
        public virtual Z_Harag_Post Z_Harag_Post { get; set; }
    }
}
