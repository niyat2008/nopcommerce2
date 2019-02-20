using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Harag.Rate
{
    public class UserRateOutList
    {
        public int UpRateCount { get; set; }
        public int DownRateCount { get; set; }
        public int ConfirmedUserUpRateCount { get; set; }
        public int ConfirmedUserDownRateCount { get; set; }
        public List<RateModel> Rates { get; set; }
    }
}
