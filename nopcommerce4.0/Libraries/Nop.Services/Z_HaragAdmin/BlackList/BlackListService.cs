using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.BlackList
{
   public class BlackListService:IBlackListService
    {
        #region Fields
        private readonly IRepository<Z_Harag_BlackList> _blackListRepository;
        private readonly IRepository<Customer> _customerRepository;
        #endregion

        #region Ctor
        public BlackListService(IRepository<Z_Harag_BlackList> blackListService, IRepository<Customer> customerRepository)
        {
            this._blackListRepository = blackListService;
            this._customerRepository = customerRepository;
        }
        #endregion

        #region Methods
        //Add Customer To BlackList
        public bool AddBlackListCustomer(PostBlackListModel model)
        {

            var blacklistInDb = new Z_Harag_BlackList
            {
                CustomerId= model.CustomerId,
                Phone=model.Phone
               
            };

            if(blacklistInDb !=null)
            {
               


                var customer = _customerRepository.Table.FirstOrDefault(c=>c.Id==model.CustomerId);

                if(customer !=null)
                {
                    customer.Blocked = true;
                    _customerRepository.Update(customer);
                    _blackListRepository.Insert(blacklistInDb);
                    return true;
                }


               

            }

            return false;
        }
        //Get All Customer in BlackList
       public List<Z_Harag_BlackList> GetBlackListCustomers()
        {
            var customersInBlackList = _blackListRepository.TableNoTracking.Include(c=>c.Customer);

            return customersInBlackList.ToList();
        }

        //Delete Blackk List Customer
       public void DeleteBlackListCustomer(int id,int blackId)
        {
            var query = _blackListRepository.Table.Include(b=>b.Customer).Where(c => c.Id == id).FirstOrDefault();


            if (query != null)
            {
              


                var customer = _customerRepository.Table.FirstOrDefault(c => c.Id ==  blackId);

                if (customer != null)
                {
                    customer.Blocked = false;
                    _customerRepository.Update(customer);
                    _blackListRepository.Delete(query);

                }




            }

           
        }
        #endregion
    }
}
