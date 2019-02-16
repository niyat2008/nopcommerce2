using Nop.Core.Domain.Z_Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Harag
{
    public class Z_Harag_PhotoMap : NopEntityTypeConfiguration<Z_Consultant_Photo>
    {
        public Z_Harag_PhotoMap()
        {
            this.ToTable("Z_Harag_Photo");
            this.HasKey(p => p.Id);

            this.Property(p => p.Url).IsRequired();

            this.HasRequired(p => p.Post)
                .WithMany(p => p.Photos)
                .HasForeignKey(p => p.PostId);
        }
    }
}
