using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Harag
{
    public class Z_Harag_CustomerServicesMessage : BaseEntity
    {  
        public int? UserId { get; set; }
        public string Message { get; set; }

        public Customer User { get; set; }
    }
}
