using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Harag
{
   public class Z_Harag_BankAccountMap: NopEntityTypeConfiguration<Z_Harag_BankAccount>
    {
        public Z_Harag_BankAccountMap()
        {
            this.ToTable("Z_Harag_BankAccount");
            this.HasKey(b => b.Id);
            this.Property(b => b.BankName).IsOptional().HasMaxLength(4000);
            this.Property(b => b.IBANNumber).IsOptional().HasMaxLength(4000);
            this.Property(b => b.AccountNo).IsOptional().HasMaxLength(4000);
            this.Property(b => b.AddedBy);
            
        }
    }
}
