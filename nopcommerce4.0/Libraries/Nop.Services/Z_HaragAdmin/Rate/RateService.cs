using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;

namespace Nop.Services.Z_HaragAdmin.Rate
{
    public class RateService : IRateService
    {
        #region Fields
        private readonly IRepository<Z_Harag_Rate> _rateRepository;
        #endregion


        #region Ctor
        public RateService(IRepository<Z_Harag_Rate> rateRepository)
        {
            this._rateRepository = rateRepository;
        }
        #endregion


        #region Methods
        //Get Rates
        public List<Z_Harag_Rate> GetRates(int start,int length,string searchValue, string sortColumnName,string sortDirection)
        {
            var rates = _rateRepository.TableNoTracking.Include(r => r.Customer).Include(c => c.User);

            if(!string.IsNullOrEmpty(searchValue))
            {
                rates = rates.Where(r => r.Customer.Username.Contains(searchValue) || r.User.Username.Contains(searchValue) || r.RateComment.Contains(searchValue));
            }

            if(!string.IsNullOrEmpty(sortColumnName)||string.IsNullOrEmpty(sortDirection))
            {
                rates = rates.OrderBy(sortColumnName + " " + sortDirection);
            }

            rates = rates.OrderBy(r => r.Id).Skip(start).Take(length);
           
                
            return rates.ToList();
        }
        //Get Rate Details
        public Z_Harag_Rate GetRateDetails(int rateId)
        {
            return _rateRepository.TableNoTracking.Where(r => r.Id == rateId).FirstOrDefault();
        }
        //Delete Rate
        public void DeleteRate(int rateId)
        {
            var query = _rateRepository.Table.FirstOrDefault(r => r.Id == rateId);
            if (query != null)
                _rateRepository.Delete(query);


        }
        #endregion
    }
}
