﻿using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Harag
{
    class SettingsMap: NopEntityTypeConfiguration<Z_Harag_Settings>
    {
        public SettingsMap()
        {
            this.ToTable("Z_Harag_Settings");
            this.HasKey(s => s.Id);
            this.Property(s => s.Key).IsOptional(); 
            this.Property(s => s.LastUpdated).IsOptional();
            this.Property(s => s.UpdatedBy).IsRequired();
        }
    }
}
