using Nop.Core.Domain.Z_Consultant;
using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Harag
{
    public class Z_HaragCategoryMap: NopEntityTypeConfiguration<Z_Harag_Category>
    {
        public Z_HaragCategoryMap()
        {
            this.ToTable("Z_Harag_Category");
            this.HasKey(c => c.Id);

            this.Property(c => c.Name).IsRequired().HasMaxLength(400);
            this.Property(c => c.Description).HasMaxLength(4000);
            this.Property(c => c.DateCreated).IsRequired();
            this.Property(c => c.DateUpdated).IsOptional();
            this.Property(c => c.IsActive).IsRequired();
            
        }
    }
}
