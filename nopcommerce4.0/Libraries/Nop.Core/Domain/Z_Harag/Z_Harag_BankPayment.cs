using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Harag
{
    public class Z_Harag_BankPayment : BaseEntity
    {
        public string UserName { get; set; } 
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string   Notes { get; set; }
        public string TransatctorUser { get; set; }
        public int TransactionDate { get; set; }
        public Decimal SiteAmount { get; set; }
        public int BankId { get; set; }

        public Z_Harag_Post Post { get; set; }
        public Customer User { get; set; } 
        public Z_Harag_BankAccount BankAccount { get; set; }
    }
}
