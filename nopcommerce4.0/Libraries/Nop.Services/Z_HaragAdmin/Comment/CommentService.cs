using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.Comment
{
   public class CommentService:ICommentService
    {
        #region Fields
        private readonly IRepository<Z_Harag_Comment> _commentRepository;
        private readonly IRepository<Z_Harag_CommentReport> _commentReportRepository;
        private readonly IRepository<Z_Harag_Message> _messageRepository;
        #endregion

        #region Ctor
        public CommentService(IRepository<Z_Harag_Comment> commentRepository, IRepository<Z_Harag_Message> messageRepository, IRepository<Z_Harag_CommentReport> commentReportRepository)
        {
            this._commentRepository = commentRepository;
            this._messageRepository = messageRepository;
            this._commentReportRepository = commentReportRepository;
        }
        #endregion
        #region Methods
        //Post Reports
       public List<Z_Harag_Comment> GetPostComments(int postId)
        {
            var query = _commentRepository.TableNoTracking.Where(c => c.PostId == postId);
            return query.ToList();
        }
        //Comment Reports
        public List<Z_Harag_CommentReport> GetCommentReports(int commentId)
        {
            var query = _commentReportRepository.TableNoTracking.Include(c=>c.Z_Harag_Comment).Where(m => m.CommentId == commentId);
            return query.ToList();
        }

        //Get Comment By Id
        public Z_Harag_Comment GetComment(int commentId)
        {
            var comment = _commentRepository.TableNoTracking.Include(p=>p.Z_Harag_Post).Include(c=>c.Customer).FirstOrDefault(c => c.Id == commentId);

            return comment;
        }
        #endregion
    }
}
