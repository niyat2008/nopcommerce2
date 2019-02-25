using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.HaragAdmin.BankAccount
{
    public class BankAccountModel: BaseNopEntityModel
    {
        public int Id { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string IBANNumber { get; set; }
        public int AddedBy { get; set; }
    }
}
