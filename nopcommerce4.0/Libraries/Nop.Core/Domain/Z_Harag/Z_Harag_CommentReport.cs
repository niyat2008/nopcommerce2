using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Harag
{
    public class Z_Harag_CommentReport:BaseEntity
    { 
        public string ReportDescription { get; set; }
        public Nullable<int> ReporterUser { get; set; }
        public Nullable<int> CommentId { get; set; }
        public string ReportTitle { get; set; }
        public Nullable<byte> ReportCategory { get; set; }

    }
}
