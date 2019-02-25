using System;
using System.Collections.Generic;

namespace Nop.Web.Models.Harag
{
    public class PaymentModel
    {
        public string UserName { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Notes { get; set; }
        public string TransatctorUser { get; set; }
        public int TransactionDate { get; set; }
        public Decimal SiteAmount { get; set; }
        public int BankId { get; set; }

        public List<BankAccountModel> Banks { get; set; }
    }
}