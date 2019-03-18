using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Payment
{
   public interface ICustomerServicesService
    {
        Z_Harag_CustomerServicesMessage AddCustomerServiceMessage(Z_Harag_CustomerServicesMessage payment); 
       Z_Harag_CustomerServicesMessage GetCustomerServiceMessage(int id);
        List<Z_Harag_CustomerServicesMessage> GetCustomerServicesMessages();
    }
}
