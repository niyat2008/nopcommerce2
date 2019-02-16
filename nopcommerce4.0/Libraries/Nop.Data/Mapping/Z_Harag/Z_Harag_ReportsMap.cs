using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Harag
{
   public class Z_Harag_ReportsMap:NopEntityTypeConfiguration<Z_Harag_Reports>
    {
        public Z_Harag_ReportsMap()
        {
            this.ToTable("Z_Harag_Reports");
            this.HasKey(c => c.Id);
            this.Property(r => r.ReportDescription).IsOptional().HasMaxLength(550);
            this.Property(r => r.ReportTitle).IsOptional().HasMaxLength(250);
            this.Property(r => r.ReporterUser).IsOptional();
            this.HasOptional(r => r.Customer)
                .WithMany(c => c.Z_Harag_Reports).HasForeignKey(r => r.ReporterUser);
            this.HasOptional(r => r.Z_Harag_Post)
                .WithMany(p => p.Z_Harag_Reports).HasForeignKey(r => r.PostId);


        }
    }
}
