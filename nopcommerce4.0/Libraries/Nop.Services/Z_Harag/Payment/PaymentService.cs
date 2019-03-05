using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Events; 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Payment
{
   public class PaymentService: IPaymentService
    {
        private readonly IRepository<Z_Harag_BankAccount> _bankAccountRepository;
        private readonly IRepository<Z_Harag_BankPayment> _paymentRepository;
        private readonly IEventPublisher _eventPublisher;

        public PaymentService(IRepository<Z_Harag_BankPayment> _paymentRepository, IRepository<Z_Harag_BankAccount> _bankAccountRepository , IEventPublisher eventPublisher)
        {
            this._bankAccountRepository = _bankAccountRepository;
            this._paymentRepository = _paymentRepository;
            this._eventPublisher = eventPublisher;
        }

        public Z_Harag_BankPayment AddNewPaymentDetails(Z_Harag_BankPayment payment)
        {
            _paymentRepository.Insert(payment);

            return payment;
        }

        public List<Z_Harag_BankAccount> GetBaknAccountsDetails()
        {
            var bankAccounts = _bankAccountRepository.TableNoTracking.ToList();

            return bankAccounts;
        }

        public List<Z_Harag_BankPayment> GetUserPayments(int id)
        {
            var payments = _paymentRepository.Table.Where(m => m.UserId == id);
            return payments.ToList();
        }
    }
}
