using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.BankAccount
{
   public class BankAccountService:IBankAccountService
    {
        #region Fields
        private readonly IRepository<Z_Harag_BankAccount> _bankRepository;
        #endregion
        #region Ctor
        public BankAccountService(IRepository<Z_Harag_BankAccount> bankRepository)
        {
            this._bankRepository = bankRepository;
        }
        #endregion
        #region Methods
        //Add Bank Account
        public bool AddBankAccount(PostBankAccount bankModel)
        {
            var bank = new Z_Harag_BankAccount
            {
                BankName = bankModel.BankName,
                IBANNumber=bankModel.IBANNumber,
                AccountNo=bankModel.AccountNo,
                AddedBy=bankModel.AddedBy
            };

            if(bank !=null)
            {

                _bankRepository.Insert(bank);
                return true;
            }
            return false; 
        }
        #endregion
    }
}
