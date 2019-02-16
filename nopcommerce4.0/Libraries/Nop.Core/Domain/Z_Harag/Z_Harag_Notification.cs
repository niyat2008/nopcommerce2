using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Harag
{
    public partial class Z_Harag_Notification:BaseEntity
    { 
        public Nullable<byte> NotificationType { get; set; }
        public Nullable<System.DateTime> NotificationTime { get; set; }
        public string NotificationContent { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> PostId { get; set; }
        public Nullable<int> OwnerId { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Customer Owner { get; set; }
        public virtual Z_Harag_Post Z_Harag_Post { get; set; }
    }
}
