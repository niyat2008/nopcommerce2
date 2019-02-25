using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Harag
{
   public class Z_Harag_Follow : BaseEntity
    { 
        public Nullable<int> FollowType { get; set; }
        public Nullable<int> PostId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> FollowedId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<DateTime> CreatedTime { get; set; }
        public Nullable<DateTime> UpdatedTime { get; set; }

        public virtual Customer User { get; set; }
        public virtual Customer Followed { get; set; }
        public virtual Z_Harag_Category Category { get; set; }
        public virtual Z_Harag_Post Post { get; set; } 
    }
}
