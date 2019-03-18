using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core.Domain.Customers;

namespace Nop.Web.Models.Harag.Rate
{ 
    public class RateModel
    {
        public Core.Domain.Customers.Customer Customer{ get; set; }
        public Core.Domain.Customers.Customer User{ get; set; }

        public int UserId { get; set; }
        public string RateComment { get; set; }
        public string Username { get; set; }
        public bool IsBuyDone { get; set; }
        public bool AdviceDeal { get; set; }
        public int WhenBuyDone { get; set; }
 
    }
}
