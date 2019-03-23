using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.HaragAdmin.BankPayment
{
    public class BankPaymentModel: BaseNopEntityModel
    {
        
        public string UserName { get; set; }
        public int PostId { get; set; }
       
        
        public string Notes { get; set; }
        public string TransatctorUser { get; set; }
        public int TransactionDate { get; set; }
        public Decimal SiteAmount { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public bool Confirmed { get;  set; }
    }
}
