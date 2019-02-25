using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Events; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Payment
{
   public class PaymentService: IPaymentService
    {
        private readonly IRepository<Z_Harag_Category> _categoryRepository;
        private readonly IEventPublisher _eventPublisher;

        public PaymentService(IRepository<Z_Harag_Category> categoryRepository, IEventPublisher eventPublisher)
        {
            this._categoryRepository = categoryRepository;
            this._eventPublisher = eventPublisher;
        }

        public List<Z_Harag_Category> GetCategories()
        {
            var query = _categoryRepository.TableNoTracking;
            var result = query.ToList(); 
            return  result;
        } 
    }
}
