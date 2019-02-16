using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Harag
{
   public class Z_Harag_MessageMap:NopEntityTypeConfiguration<Z_Harag_Message>
    {
        public Z_Harag_MessageMap()
        {
            this.ToTable("Z_Harag_Message");
            this.HasKey(m => m.Id);
            this.Property(m => m.Message).IsOptional().HasMaxLength(1200);
            this.Property(m => m.CreatedTime).IsOptional();
            this.HasOptional(m => m.Z_Harag_Post).WithMany(p => p.Z_Harag_Message).HasForeignKey(m => m.Z_Harag_Post);
            this.HasOptional(m => m.Customer).WithMany(c => c.Z_Harag_Message).HasForeignKey(m => m.CustomerId);
        }
    }
}
