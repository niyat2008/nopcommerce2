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
        private readonly IRepository<Z_Harag_Comment> _commentRepository;
        private readonly IRepository<Z_Harag_CommentReport> _reportRepository;
        private readonly IEventPublisher _eventPublisher;

        public CommentService(IRepository<Z_Harag_Comment> commentRepository,
            IRepository<Z_Harag_CommentReport> _reportRepository,
            IEventPublisher eventPublisher)
        {
            this._commentRepository = commentRepository;
            this._eventPublisher = eventPublisher;
            this._reportRepository = _reportRepository;
        }

        public Z_Harag_Comment AddComment(CommentForPostModel model, int currentUserId)
        {
            if (currentUserId == 0)
            {
                return null;
            }

            var entityModel = new Z_Harag_Comment
            {
                DateCreated = DateTime.Now,
                CommentedBy = currentUserId.ToString(),
                DateUpdated = DateTime.Now,
                CustomerId = currentUserId,
                PostId = model.PostId,
                Text = model.Text 
            };
            _commentRepository.Insert(entityModel);

            return entityModel;
        }

        public List<Z_Harag_Comment> GetCommentsByPostId(int postId)
        {
            var query = _commentRepository.TableNoTracking.Where(m => m.PostId == postId);

            return query.ToList();
        }

        public Z_Harag_CommentReport ReportComment(Z_Harag_CommentReport model)
        {
            _reportRepository.Insert(model);
            return model;
        }
    }
}
