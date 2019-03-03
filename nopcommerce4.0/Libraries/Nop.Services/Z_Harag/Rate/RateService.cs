using Microsoft.AspNetCore.Hosting;
using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Rate
{
   public class RateService:IRateSrevice
    {
        private readonly IRepository<Z_Harag_Rate> _rateService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IHostingEnvironment _env;

        public RateService(IRepository<Z_Harag_Rate> rateService, IEventPublisher eventPublisher, IHostingEnvironment env)
        {
            this._rateService = rateService;
            this._eventPublisher = eventPublisher;
            this._env = env;
        }

        public bool AddUserRate(Z_Harag_Rate rate)
        {
            _rateService.Insert(rate);
            return true;
        }

        public List<Z_Harag_Rate> GetUserRates(int userId)
        {
            return _rateService.TableNoTracking.Where(m => m.UserId == userId).ToList();
        }
        public List<Z_Harag_Rate> GetUserUpRates(int userId)
        {
            return _rateService.TableNoTracking.Where(m => m.UserId == userId && m.AdviceDeal == true).ToList();
        }
        public List<Z_Harag_Rate> GetUserDownRates(int userId)
        {
            return _rateService.TableNoTracking.Where(m => m.UserId == userId && m.AdviceDeal == false).ToList();
        }
    }
}
