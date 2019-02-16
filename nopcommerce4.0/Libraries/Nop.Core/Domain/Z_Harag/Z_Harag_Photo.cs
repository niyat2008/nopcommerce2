using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Harag
{
    public class Z_Harag_Photo : BaseEntity
    {
        public string Url { get; set; }

        public int PostId { get; set; }

        public Z_Harag_Post Post { get; set; }
    }
}
