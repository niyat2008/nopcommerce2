using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Payment
{
   public interface IPaymentService
    { 
        Z_Harag_BankPayment AddNewPaymentDetails(Z_Harag_BankPayment payment);
        List<Z_Harag_BankAccount> GetBaknAccountsDetails();
        List<Z_Harag_BankPayment> GetUserPayments(int id);
    }
}
