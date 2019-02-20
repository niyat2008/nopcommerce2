using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.Customers
{
   public interface ICustomerService
    {
        //Get Members
        List<Customer> GetMembers(int start, int length, string searchValue, string sortColumnName, string sortDirection);

        // Get Member Details
        Customer GetMemberDetails(int id);
        //Get  Members number
        int GetMembersNumber();
    }
}
