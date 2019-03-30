using Microsoft.AspNetCore.Hosting;
using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Customers;
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

        public Z_Harag_Message AddCommentMessage(CommentMessageModel messageModel)
        {
            throw new NotImplementedException();
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

        public List<Z_Harag_Message> GetUserMessages(int FromUserId, int toUserId)
        {
            var query = _messageRepository.TableNoTracking
                .Include(mbox => mbox.Customer)
                .Include(mbox => mbox.User)
                .Include(mbox => mbox.Z_Harag_Post) 
                .Where(m => m.FromUserId == FromUserId && m.ToUserId == toUserId ||
                 m.FromUserId == toUserId && m.ToUserId ==FromUserId ).ToList();
            return query;
        }

        public List<MessageThreadModel> GetMessagesByUser(int userId)
        {
            var users = _messageRepository.TableNoTracking 
                .Where(m => m.ToUserId == userId || m.FromUserId == userId)
                .Select(n => n.ToUserId)
                .Distinct()
                .ToList(); 
             
            var messagesthread = new List<MessageThreadModel>();
            foreach (var user in users)
            {
                if (user == userId)
                {
                    continue;
                }
                var message = _messageRepository.TableNoTracking.Where(m => m.FromUserId == user)
                    .Include(mbox => mbox.Customer)
                    .Include(mbox => mbox.Z_Harag_Post)
                    .Include(mbox => mbox.User)
                    .OrderBy(m => m.CreatedTime).FirstOrDefault();

                var fromUser = message.User == null ? "" : message.User.GetFullName();
                var toUser = message.Customer == null ? "" : message.Customer.GetFullName();
                var postObj = message.Z_Harag_Post == null ? "" :message.Z_Harag_Post.Title;

                messagesthread.Add(new MessageThreadModel
                {
                    Message = message.Message,
                    SentFromName = fromUser,
                    SentToName = toUser,
                    CreatedTime = message.CreatedTime,
                    CustomerId = message.ToUserId,
                    UserId = message.FromUserId,
                    MessageTitle = message.MessageTitle,
                    MessageType = message.MessageType,
                    PostId = message.PostId,
                    PostTitle = postObj
                });
            }
            return messagesthread;
        }

      
    }
}
