using Microsoft.AspNetCore.Hosting;
using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Message
{
   public class MessageService:IMessageService
    {
        private readonly IRepository<Z_Harag_Message> _messageRepository ;
        
        private readonly IEventPublisher _eventPublisher;
        private readonly IHostingEnvironment _env;

        public MessageService( IRepository<Z_Harag_Message> _messageRepository, IEventPublisher eventPublisher, IHostingEnvironment env)
        {
            this._messageRepository = _messageRepository;
            this._eventPublisher = eventPublisher;
            this._env = env;
        }

        public Z_Harag_Message AddMessage(Z_Harag_Message message)
        {
            if (message == null)
                return null; 
            _messageRepository.Insert(message);
            return message;
        }

        public bool DeleteMessage(int messageId)
        {
            var message = _messageRepository.TableNoTracking
                .Include(mbox => mbox.Z_Harag_Post)
                .Where(m => m.Id == messageId).FirstOrDefault();

            if (message == null)
                return false;

            _messageRepository.Delete(message);
            return true;
        }

        public Z_Harag_Message GetMessage(int messageId)
        { 
            var query = _messageRepository.TableNoTracking
                .Include(mbox => mbox.Z_Harag_Post)
                .Where(m => m.Id == messageId).FirstOrDefault();

            if (query == null)
                return null;

            return query;
        }

        public List<Z_Harag_Message> GetMessagesByPost(int postId)
        {
            var query = _messageRepository.TableNoTracking
                .Include(mbox=>mbox.Customer)
                .Include(mbox => mbox.Z_Harag_Post)

                .Where(m => m.PostId == postId).ToList();
            return query;
        }

        public List<Z_Harag_Message> GetMessagesByUser(int userId)
        {
            var query = _messageRepository.TableNoTracking
                .Include(mbox => mbox.Customer) 
                .Include(mbox => mbox.Z_Harag_Post)
                .Where(m => m.CustomerId == userId)
                
                .Distinct()
                .OrderByDescending(m => m.CreatedTime)
                .ToList();
            return query;
        }
    }
}
