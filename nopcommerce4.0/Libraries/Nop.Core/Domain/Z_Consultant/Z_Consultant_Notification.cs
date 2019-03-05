using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Consultant
{
   public class Z_Consultant_Notification:BaseEntity
    {
        public int PostId { get; set; }
        public int OwnerId { get; set; }
        public  int? UserId { get; set; }
        public int Type { get; set; }
        public DateTime Time { get; set; }
        public bool IsRead { get; set; }
        public Z_Consultant_Post Post { get; set; }
        public Customer Owner { get; set; }
        public Customer User { get; set; }
    }
}
