using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Harag
{
    public class Z_Harag_CommentMap : NopEntityTypeConfiguration<Z_Harag_Comment>
    {
        public Z_Harag_CommentMap()
        {
            this.ToTable("Z_Harag_Comment");
            this.HasKey(c => c.Id);

            this.Property(c => c.Text).IsRequired().HasMaxLength(4000);
            this.Property(c => c.DateCreated).IsRequired();
            this.Property(c => c.DateUpdated).IsOptional();
            this.Property(c => c.CommentedBy).IsRequired().HasMaxLength(100);

            this.HasRequired(c => c.Z_Harag_Post)
                .WithMany(p => p.Z_Harag_Comment)
                .HasForeignKey(c => c.PostId);

            this.HasOptional(c => c.Customer)
                .WithMany(p => p.Z_Harag_Comment)
                .HasForeignKey(c => c.CustomerId);

           

        }
    }
}
