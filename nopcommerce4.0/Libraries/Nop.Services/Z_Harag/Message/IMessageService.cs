using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Message
{
    public interface IMessageService
    {
        List<MessageThreadModel> GetMessagesByUser(int userId);
        List<Z_Harag_Message> GetMessagesByPost(int postId);
        Z_Harag_Message GetMessage(int messageId);
        bool DeleteMessage(int messageId);
        List<Z_Harag_Message> GetUserMessages(int FromUserId, int toUserId);
        Z_Harag_Message AddMessage(Z_Harag_Message message);
        Z_Harag_Message AddCommentMessage(CommentMessageModel messageModel);
    }
}
