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
    }
}
