using Nop.Core.Data;
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
        #endregion

        #region Ctor
        public BlackListService(IRepository<Z_Harag_BlackList> blackListService)
        {
            this._blackListRepository = blackListService;
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
                _blackListRepository.Insert(blacklistInDb);
                return true;

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
       public void DeleteBlackListCustomer(int id)
        {
            var customer = _blackListRepository.Table.Where(c => c.Id == id).FirstOrDefault();

            _blackListRepository.Delete(customer);
        }
        #endregion
    }
}
