using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Comment
{
   public class CommentService:ICommentService
    {
        private readonly IRepository<Z_Harag_Category> _commentRepository;
        private readonly IEventPublisher _eventPublisher;

        public CommentService(IRepository<Z_Harag_Category> commentRepository, IEventPublisher eventPublisher)
        {
            this._commentRepository = commentRepository;
            this._eventPublisher = eventPublisher;
        }
    }
}
