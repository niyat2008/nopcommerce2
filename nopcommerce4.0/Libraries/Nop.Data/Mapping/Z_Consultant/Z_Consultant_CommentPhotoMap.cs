using Nop.Core.Domain.Z_Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Consultant
{
    public class Z_Consultant_CommentPhotoMap : NopEntityTypeConfiguration<Z_Consultant_CommentPhoto>
    {
        public Z_Consultant_CommentPhotoMap()
        {
            this.ToTable("Z_Consultant_CommentPhoto");
            this.HasKey(p => p.Id);

            this.Property(p => p.Url).IsRequired();

            this.HasRequired(p => p.Comment)
                .WithMany(p => p.Photos)
                .HasForeignKey(p => p.CommentId);
        }
    }
}
