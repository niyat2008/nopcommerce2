using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Harag
{
    public class Z_Harag_BankPaymentMap: NopEntityTypeConfiguration<Z_Harag_BankPayment>
    {
        public Z_Harag_BankPaymentMap()
        {
            this.ToTable("Z_Harag_BankPayment");
            this.HasKey(p => p.Id);

            this.Property(p => p.Notes).IsRequired().HasMaxLength(4000);
            this.Property(p => p.UserName).IsRequired().HasMaxLength(4000);
            this.Property(p => p.PostId).IsRequired(); 
            this.Property(p => p.UserId).IsRequired();
            this.Property(p => p.UserName).IsRequired();
            this.Property(p => p.TransactionDate).IsRequired(); 
            this.Property(p => p.SiteAmount).IsRequired();
            this.Property(p => p.BankId).IsRequired();

            this.HasRequired(p => p.User).WithMany(m => m.CustomersHaragPayment).HasForeignKey(r => r.UserId);
            this.HasRequired(p => p.Post).WithMany(m => m.HaragPostsPayment).HasForeignKey(r => r.PostId); 
            this.HasRequired(p => p.BankAccount).WithMany(m => m.HaragBankPayments).HasForeignKey(r => r.BankId); 
        }
    }
}
