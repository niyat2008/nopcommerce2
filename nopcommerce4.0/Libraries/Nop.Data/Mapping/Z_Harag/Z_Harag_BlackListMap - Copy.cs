using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Harag
{
   public class Z_Harag_BlackListMap:NopEntityTypeConfiguration<Z_Harag_BlackList>
    {
        public Z_Harag_BlackListMap()
        {
            this.ToTable("Z_Harag_BlackList");
            this.HasKey(b => b.Id);
            
        }
    }
}
