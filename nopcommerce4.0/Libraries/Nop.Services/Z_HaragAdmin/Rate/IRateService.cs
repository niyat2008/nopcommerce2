using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.Rate
{
   public interface IRateService
    {
        //Get Rates
         List<Z_Harag_Rate> GetRates(int start, int length, string searchValue, string sortColumnName, string sortDirection);
        //Get Rate Details
        Z_Harag_Rate GetRateDetails(int rateId);
        //Delete Rate
        void DeleteRate(int rateId);
    }
}
