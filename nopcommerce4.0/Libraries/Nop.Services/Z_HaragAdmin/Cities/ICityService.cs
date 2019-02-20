using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.Cities
{
   public interface ICityService
    {
        //Get Cities
        List<City> GetCities();

        //Delete Citie
        void DeleteCity(int cityId);
    }
}
