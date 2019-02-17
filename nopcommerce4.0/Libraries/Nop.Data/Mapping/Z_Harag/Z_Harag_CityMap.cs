
using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Harag
{
    public class Z_Harag_CityMap : NopEntityTypeConfiguration<City>
    {
        public Z_Harag_CityMap()
        {
            this.ToTable("City");
            this.HasKey(c => c.Id);

            this.Property(c => c.EnName).IsRequired().HasMaxLength(400);
            this.Property(c => c.ArName).HasMaxLength(4000); 
        }
    }
}
