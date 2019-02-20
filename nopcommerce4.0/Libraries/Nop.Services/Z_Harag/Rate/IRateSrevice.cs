using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Rate
{
   public interface IRateSrevice
    {
        bool AddUserRate(Z_Harag_Rate rate);
        List<Z_Harag_Rate> GetUserRates(int userId);
    }
}
