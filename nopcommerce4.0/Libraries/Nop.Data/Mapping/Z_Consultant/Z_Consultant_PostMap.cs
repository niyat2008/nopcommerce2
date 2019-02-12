using Nop.Core.Domain.Z_Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Consultant
{
    public class Z_Consultant_PostMap: NopEntityTypeConfiguration<Z_Consultant_Post>
    {
        public Z_Consultant_PostMap()
        {
            this.ToTable("Z_Consultant_Post");
            this.HasKey(p => p.Id);

            this.Property(p => p.Text).IsRequired().HasMaxLength(4000);
            this.Property(p => p.Title).IsRequired().HasMaxLength(4000);
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsOptional();
            this.Property(p => p.IsClosed).IsRequired();
            this.Property(p => p.IsAnswered).IsRequired();
            this.Property(p => p.IsCommon).IsRequired();
            this.Property(p => p.IsSetToSubCategory).IsRequired();
            this.Property(p => p.Rate).IsOptional();
            this.Property(p => p.IsDispayed).IsRequired();
            this.Property(p => p.IsReserved).IsRequired();
            this.Property(p => p.LastCommentTime).IsOptional();

            this.HasRequired(p => p.Customer)
                .WithMany(c => c.CustomersPosts)
                .HasForeignKey(p => p.CustomerId);

            this.HasOptional(p => p.Consultant)
                .WithMany(c => c.ConsultantsPosts)
                .HasForeignKey(p => p.ConsultantId);

            this.HasRequired(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId);

            this.HasOptional(p => p.SubCategory)
                .WithMany(s => s.Posts)
                .HasForeignKey(p => p.SubCategoryId);


        }
    }
}
