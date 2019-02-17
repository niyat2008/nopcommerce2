using Microsoft.AspNetCore.Hosting;
using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.BlackList
{
   public class BlackListService:IBlackListService
    {
        private readonly IRepository<Z_Harag_BlackList> _blackListService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IHostingEnvironment _env;

        public BlackListService(IRepository<Z_Harag_BlackList> blackListService, IEventPublisher eventPublisher, IHostingEnvironment env)
        {
            this._blackListService = blackListService;
            this._eventPublisher = eventPublisher;
            this._env = env;
        }
    }
}
