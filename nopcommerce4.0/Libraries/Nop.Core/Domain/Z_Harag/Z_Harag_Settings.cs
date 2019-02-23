using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Harag
{
    public class Z_Harag_Settings:BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int UpdatedBy { get; set; }

    }
}
