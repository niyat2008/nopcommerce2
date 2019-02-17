using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Harag
{
   public class Z_Harag_FavoriteMap:NopEntityTypeConfiguration<Z_Harag_Favorite>
    {
        public Z_Harag_FavoriteMap()
        {
            this.ToTable("Z_Harag_Favorite");
            this.HasKey(c => c.Id);
            this.HasRequired(f => f.Z_Harag_Post)
                .WithMany(p => p.Z_Harag_Favorite).HasForeignKey(f => f.PostId);

            this.HasRequired(f => f.Customer)
                .WithMany(c => c.Z_Harag_Favorite).HasForeignKey(f => f.CustomerId);
        }
    }
}
