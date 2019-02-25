using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Harag
{
   public class Z_Harag_BankAccount: BaseEntity
    {
        public Z_Harag_BankAccount()
        {
            this.HaragBankPayments = new HashSet<Z_Harag_BankPayment>();
        }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string IBANNumber { get; set; }
        public int AddedBy { get; set; }
        public ICollection<Z_Harag_BankPayment> HaragBankPayments { get; set; }

    }
}
