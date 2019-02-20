using Nop.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Z_Harag;

namespace Nop.Services.Z_HaragAdmin.Cities
{
   public class CityService:ICityService
    {
        #region Fields
        private readonly IRepository<City> _cityRepository;
        #endregion

        #region Ctor
        public CityService(IRepository<City> cityRepository)
        {
            this._cityRepository = cityRepository;
        }
        #endregion
        #region Methods
        //Get Cities
        public List<City> GetCities()
        {
            var query = _cityRepository.TableNoTracking;
            return query.ToList();
        }

        //Delete City
        public void DeleteCity(int cityId)
        {
            var city = _cityRepository.Table.Where(c => c.Id == cityId).FirstOrDefault();

            if (city != null)
                _cityRepository.Delete(city);
                    
        }
        #endregion
    }
}
