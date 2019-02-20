using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.BlackList
{
    public interface IBlackListService
    {
        //Add Customer To BlackList
        bool AddBlackListCustomer(PostBlackListModel model);

        //Get All Customer in BlackList
        List<Z_Harag_BlackList> GetBlackListCustomers();
        //Delete Blackk List Customer
        void DeleteBlackListCustomer(int id);
    }
}
