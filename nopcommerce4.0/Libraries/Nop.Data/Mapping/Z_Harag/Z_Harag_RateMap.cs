using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Harag
{
   public class Z_Harag_RateMap:NopEntityTypeConfiguration<Z_Harag_Rate>
    {
        public Z_Harag_RateMap()
        {
            this.ToTable("Z_Harag_Rate");
            this.HasKey(r => r.Id);
            this.Property(r => r.IsBuyDone);
            this.Property(r => r.AdviceDeal);
            this.HasOptional(r => r.Customer).WithMany(c => c.Z_Harag_Rate).HasForeignKey(r => r.CustomerId);
            this.HasRequired(r => r.Z_Harag_Post).WithOptional(p => p.Z_Harag_Rate);

        }
    }
}
