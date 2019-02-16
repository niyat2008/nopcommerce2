using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Harag
{
   public class Z_Harag_CommentReportMap:NopEntityTypeConfiguration<Z_Harag_CommentReport>
    {
        public Z_Harag_CommentReportMap()
        {
            this.ToTable("Z_Harag_CommentReport");
            this.HasKey(c => c.Id);
            this.Property(c => c.ReportDescription).IsOptional().HasMaxLength(550);
            this.Property(c => c.ReportTitle).IsOptional().HasMaxLength(250);
            this.Property(c => c.ReportCategory).IsOptional();
            this.HasOptional(c => c.Z_Harag_Customer).WithMany(cus => cus.Z_Harag_CommentReport)
                .HasForeignKey(c => c.ReporterUser);
            this.HasOptional(c => c.Z_Harag_Comment).WithMany(com => com.Z_Harag_CommentReport)
                .HasForeignKey(c => c.CommentId);

        }
       
    }
}
