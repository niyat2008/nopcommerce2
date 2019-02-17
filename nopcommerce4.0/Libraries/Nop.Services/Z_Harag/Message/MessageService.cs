using Microsoft.AspNetCore.Hosting;
using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Message
{
   public class MessageService:IMessageService
    {
        private readonly IRepository<Z_Harag_Message> _messageService;
        
        private readonly IEventPublisher _eventPublisher;
        private readonly IHostingEnvironment _env;

        public MessageService(IRepository<Z_Harag_Message> messageService, IEventPublisher eventPublisher, IHostingEnvironment env)
        {
            this._messageService = messageService;
            this._eventPublisher = eventPublisher;
            this._env = env;
        }
    }
}
