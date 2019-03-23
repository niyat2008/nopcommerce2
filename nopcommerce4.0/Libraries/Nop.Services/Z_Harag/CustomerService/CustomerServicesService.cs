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
   public class CustomerServicesService : ICustomerServicesService
    {
        private readonly IRepository<Z_Harag_CustomerServicesMessage> _customerServiceRepository; 
        private readonly IEventPublisher _eventPublisher;

        public CustomerServicesService(IRepository<Z_Harag_CustomerServicesMessage> _customerServiceRepository, IRepository<Z_Harag_BankAccount> _bankAccountRepository , IEventPublisher eventPublisher)
        {
            this._customerServiceRepository = _customerServiceRepository; 
            this._eventPublisher = eventPublisher;
        } 
        public Z_Harag_CustomerServicesMessage AddCustomerServiceMessage(Z_Harag_CustomerServicesMessage message)
        {
            _customerServiceRepository.Insert(message);

            return message;
        }

        public List<Z_Harag_CustomerServicesMessage> GetCustomerServicesMessages()
        {
            var cs = _customerServiceRepository.TableNoTracking.Include(u => u.User).ToList();

            return cs;
        }

        public Z_Harag_CustomerServicesMessage GetCustomerServiceMessage(int id)
        {
            var cs = _customerServiceRepository.Table.Where(m => m.Id == id).Include(m => m.User).FirstOrDefault();
            return cs;
        } 
    }
}
