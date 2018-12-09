using Nop.Core.Domain.Z_Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Consultant
{
    public class Z_Consultant_CategoryMap: NopEntityTypeConfiguration<Z_Consultant_Category>
    {
        public Z_Consultant_CategoryMap()
        {
            this.ToTable("Z_Consultant_Category");
            this.HasKey(c => c.Id);

            this.Property(c => c.Name).IsRequired().HasMaxLength(400);
            this.Property(c => c.Description).HasMaxLength(4000);
            this.Property(c => c.DateCreated).IsRequired();
            this.Property(c => c.DateUpdated).IsOptional();
        }
    }
}
