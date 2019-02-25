using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.BankAccount
{
   public interface IBankAccountService
    {
        //Add Bank Account
        bool AddBankAccount(PostBankAccount bankModel);
    }
}
