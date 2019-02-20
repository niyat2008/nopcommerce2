using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.BlackList
{
   public interface IBlackListService
    {
        bool checkIfUserInBlackList(string phone);
        List<Nop.Core.Domain.Z_Harag.Z_Harag_BlackList> GetAllUserInBlackList();
    }
}
