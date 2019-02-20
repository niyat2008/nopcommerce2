using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Harag.Rate
{ 
    public class RateModel
    {
        public int UserId { get; set; }
        public string RateComment { get; set; }
        public string Username { get; set; }
        public bool IsBuyDone { get; set; }
        public bool AdviceDeal { get; set; }
        public int WhenBuyDone { get; set; }
 
    }
}
