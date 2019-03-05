using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_ConsultantAdmin.Customers
{
    public interface ICustomerService
    {
        //Get Members
        List<Customer> GetMembers(int start,int length,string searchValue,string sortColumnName,string sortDirection);

        // Get Member Details
        Customer GetMemberDetails(int id);

        //Get Online Members
        List<Customer> GetOnlineMembers(int start, int length, string searchValue, string sortColumnName, string sortDirection);

        //Get Online Consultants
        List<Customer> GetOnlineConsultants(int start, int length, string searchValue, string sortColumnName, string sortDirection);

        //Get Consultants
        List<Customer> GetConsultants(int start, int length, string searchValue, string sortColumnName, string sortDirection);
        //Get Consultants for Notifications
         List<Customer> GetConsultants();

        //Get Consultant Details
        Customer GetConsultantDetails(int id);



        //Get  Members number
        int GetMembersNumber();
        //Get  Consultants number
        int GetConsultantsNumber();
        //Get  Online Members number
        int GetOnlineMembersNumber();
        //Get  Online Consultants number
        int GetOnlineConsultantsNumber();

        //Delete Customer
        void DeleteMember(int id);
    }
}
