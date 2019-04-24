using Nop.Core.Domain.Z_Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Consultant
{
    public class Z_Consultant_SubCategoryMap : NopEntityTypeConfiguration<Z_Consultant_SubCategory>
    {
        public Z_Consultant_SubCategoryMap()
        {
            this.ToTable("Z_Consultant_SubCategory");
            this.HasKey(s => s.Id);

            this.Property(s => s.Name).IsRequired().HasMaxLength(400);
            this.Property(s => s.DateCreated).IsRequired();
            this.Property(s => s.DateUpdated).IsOptional();
            this.Property(s => s.Description).IsOptional().HasMaxLength(4000);
            this.Property(s => s.IsActive);

            this.HasRequired(s => s.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(s => s.CategoryId);
        }
    }
}
