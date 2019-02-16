using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Harag
{
   public class Z_Harag_BlackList:BaseEntity
    { 
        public Nullable<int> CustomerId { get; set; }
        public string Phone { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
