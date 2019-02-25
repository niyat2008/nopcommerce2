using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.BankAccount
{
   public interface IBankAccountService
    {

        //Get All Bank Accounts
        List<Z_Harag_BankAccount> GetAllAccount(int start, int length, string searchValue, string sortColumnName, string sortDirection);
        //Add Bank Account
        bool AddBankAccount(PostBankAccount bankModel);

        //Get Account 
        PostBankAccount GetBankAccount(int id);

        //Update Account 
        bool UpdateBankAccount(PostBankAccount model);

        //Delete Account 
         bool DeleteBankAccount(int id);
    }
}
