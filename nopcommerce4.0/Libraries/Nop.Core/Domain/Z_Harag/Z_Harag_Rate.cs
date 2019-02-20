using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Harag
{
   public class Z_Harag_Rate:BaseEntity
    {
        public Nullable<int> CustomerId { get; set; }
        public string RateComment { get; set; }
        public bool AdviceDeal { get; set; }
        public bool IsBuyDone { get; set; }
        public Nullable<System.DateTime> BuyTime { get; set; }
        public Nullable<int> UserId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Customer User { get; set; }


    }
}
