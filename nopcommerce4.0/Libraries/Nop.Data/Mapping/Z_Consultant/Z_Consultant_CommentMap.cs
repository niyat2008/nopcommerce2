using Nop.Core.Domain.Z_Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Consultant
{
    public class Z_Consultant_CommentMap : NopEntityTypeConfiguration<Z_Consultant_Comment>
    {
        public Z_Consultant_CommentMap()
        {
            this.ToTable("Z_Consultant_Comment");
            this.HasKey(c => c.Id);

            this.Property(c => c.Text).IsRequired().HasMaxLength(4000);
            this.Property(c => c.DateCreated).IsRequired();
            this.Property(c => c.DateUpdated).IsOptional();
            this.Property(c => c.CommentedBy).IsRequired().HasMaxLength(100);

            this.HasRequired(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId);

            this.HasOptional(c => c.Customer)
                .WithMany(p => p.CustomersComments)
                .HasForeignKey(c => c.CustomerId);

            this.HasOptional(c => c.Consultant)
                .WithMany(p => p.ConsultantsComments)
                .HasForeignKey(c => c.ConsultantId);
             

        }
    }
}
