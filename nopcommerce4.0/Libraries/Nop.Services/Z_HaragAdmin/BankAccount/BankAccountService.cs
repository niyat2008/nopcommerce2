using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using System.Data.Entity;

namespace Nop.Services.Z_HaragAdmin.BankAccount
{
   public class BankAccountService:IBankAccountService
    {
        #region Fields
        private readonly IRepository<Z_Harag_BankAccount> _bankRepository;
        private readonly IRepository<Z_Harag_BankPayment> _bankPaymentRepository;
        #endregion
        #region Ctor
        public BankAccountService(IRepository<Z_Harag_BankAccount> bankRepository, IRepository<Z_Harag_BankPayment> bankPaymentRepository)
        {
            this._bankRepository = bankRepository;
            this._bankPaymentRepository = bankPaymentRepository;
        }
        #endregion
        #region Methods

        //Get All Bank Accounts
        public List<Z_Harag_BankAccount> GetAllAccount(int start, int length, string searchValue, string sortColumnName, string sortDirection)
        {
            var account = _bankRepository.TableNoTracking;

            //search
            if (!string.IsNullOrEmpty(searchValue))
                account  = account .Where(c => c.BankName.ToLower().Contains(searchValue));

            //sort
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
                account  = account .OrderBy(sortColumnName + " " + sortDirection);

            //paging
            account  = account .OrderByDescending(c => c.BankName).Skip(start).Take(length);

            return account .ToList();

        }

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

        //Get Account 
        public PostBankAccount GetBankAccount(int id)
        {
            var account = _bankRepository.TableNoTracking.Where(b => b.Id == id).FirstOrDefault();

            if (account == null)
                return null;
            var model = new PostBankAccount
            {
                Id=account.Id,
              BankName=account.BankName,
              IBANNumber=account.IBANNumber,
              AccountNo=account.AccountNo
            };

            return model;
        }

        //Update Account 
        public bool UpdateBankAccount(PostBankAccount model)
        {
            var account = _bankRepository.Table.Where(b => b.Id == model.Id).FirstOrDefault();

            if (account == null)
                return false;

            account.Id = model.Id;
            account.BankName = model.BankName;
            account.IBANNumber = model.IBANNumber;
            account.AccountNo = model.AccountNo;

            _bankRepository.Update(account);
            return true;
        }

        //Delete Account 
        public bool DeleteBankAccount(int id)
        {
            var account = _bankRepository.Table.Where(b => b.Id == id).FirstOrDefault();

            if (account == null)
                return false;

           

            _bankRepository.Delete(account);

            return true;
        }

       
       

        // Get Bank Payments 
        public List<Z_Harag_BankPayment> GetPayments(int start, int length, string searchValue, string sortColumnName, string sortDirection)
        {

            var query =  _bankPaymentRepository.Table;
            var t =  query.ToList();
            //search
           if(!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(b => b.UserName.Contains(searchValue) || b.BankAccount.BankName.Contains(searchValue));
            }

           //sort
           if(!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
                query = query.OrderBy(sortColumnName + " " + sortDirection);
            }

            //paging
            query = query.OrderByDescending(b => b.TransactionDate).Skip(start).Take(length);

            return query.Include(b => b.Post).Include(b => b.User).Include(b => b.BankAccount).ToList();
        }

        public Z_Harag_BankPayment ConfirmSitePayment(int paymentId)
        {
            var payment = _bankPaymentRepository.Table.Where(m => m.Id == paymentId).FirstOrDefault();

            if (payment == null)
            {
                return null;
            }

            payment.PaymentConfirmed = true;

            _bankPaymentRepository.Update(payment);

            return payment;
        }
        #endregion
    }
}
