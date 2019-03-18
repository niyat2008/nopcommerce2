using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Harag
{
   public class Z_Harag_CustomerServicesMessageMap : NopEntityTypeConfiguration<Z_Harag_CustomerServicesMessage>
    {
        public Z_Harag_CustomerServicesMessageMap()
        {
            this.ToTable("Z_Harag_CustomerServicesMessage");
            this.HasKey(c => c.Id);
            this.Property(c => c.Message).IsOptional().HasMaxLength(4000); 
            this.HasOptional(c => c.User).WithMany(com => com.CustomerServiceMessages)
                .HasForeignKey(c => c.UserId);

        }
       
    }
}
