using Nop.Core.Domain.Z_Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Consultant
{
    public class Z_Consultant_NeighborhoodMap : NopEntityTypeConfiguration<Neighbohood>
    {
        public Z_Consultant_NeighborhoodMap()
        {
            this.ToTable("Neighbohood");
            this.HasKey(c => c.Id);

            this.Property(c => c.ArName).IsRequired().HasMaxLength(400);
            this.Property(c => c.EnName).HasMaxLength(4000);
            this.Property(c => c.CityId).IsRequired(); 
        }
    }
}
