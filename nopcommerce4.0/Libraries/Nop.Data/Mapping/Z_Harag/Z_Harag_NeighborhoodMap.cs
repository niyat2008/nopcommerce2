﻿using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Harag
{
    public class Z_Harag_NeighborhoodMap : NopEntityTypeConfiguration<Neighborhood>
    {
        public Z_Harag_NeighborhoodMap()
        {
            this.ToTable("Neighborhood");
            this.HasKey(c => c.Id);

            this.Property(c => c.ArName).IsRequired().HasMaxLength(400);
            this.Property(c => c.EnName).HasMaxLength(4000);
            this.Property(c => c.CityId).IsRequired(); 
        }
    }
}
