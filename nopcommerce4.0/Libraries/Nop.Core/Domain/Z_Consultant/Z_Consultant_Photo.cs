using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Consultant
{
    public class Z_Consultant_Photo : BaseEntity
    {
        public string Url { get; set; }

        public int PostId { get; set; }

        public Z_Consultant_Post Post { get; set; } 
    }
}
 
 
 