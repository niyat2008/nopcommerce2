using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.HaragAdmin.Rate
{
    public class RateModel
    {
        public int Id { get; set; }
        public string RaterUser { get; set; }
        public string RatedUser { get; set; }
        public bool AdviceDeal { get; set; }
        public bool IsBuyDone { get; set; }
        public DateTime? BuyTime { get; set; }
        public string RateComment { get; set; }
    }
}
