using System;

namespace Nop.Web.Models.Harag
{
    public class BankAccountModel
    { 
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string IBANNumber { get; set; }
        public string AccountNumber { get; set; }
    }
}