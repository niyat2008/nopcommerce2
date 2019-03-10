using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Harag.Report
{
    public class PostReport
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public bool IsIllegal { get; set; }
        public int ReportType { get; set; }
        public string ReportMessage { get; set; }
    }
  
     
}
