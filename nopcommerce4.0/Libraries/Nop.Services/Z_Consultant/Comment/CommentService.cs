using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Data;
using Nop.Core.Domain.Z_Consultant;
using Nop.Services.Z_Consultant.Helpers;

namespace Nop.Services.Z_Consultant.Comment
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Z_Consultant_Comment> _commentRepository;

        public CommentService(IRepository<Z_Consultant_Comment> commentRepository)
        {
            this._commentRepository = commentRepository;
        }

        public Z_Consultant_Comment AddCommentByConsultant(CommentForPostModel CommentForPostDto, int consultantId)
        {
            var comment = new Z_Consultant_Comment()
            {
                Text = CommentForPostDto.Text,
                PostId = CommentForPostDto.PostId,
                ConsultantId = consultantId,
                CommentedBy = CommentByTypes.Consultant,
                DateCreated = DateTime.Now
            };
            _commentRepository.Insert(comment);
            return comment;
        }

        public Z_Consultant_Comment AddCommentByCustomer(CommentForPostModel CommentForPostDto, int customerId)
        {
            var comment = new Z_Consultant_Comment()
            {
                Text = CommentForPostDto.Text,
                PostId = CommentForPostDto.PostId,
                CustomerId = customerId,
                CommentedBy = CommentByTypes.Registered,
                DateCreated = DateTime.Now
            };

            _commentRepository.Insert(comment);
            return comment;
        }

        public void DeleteComment(DeleteCommentModel DeleteCommentModel)
        {
            var comment = _commentRepository.Table
                .Where(c => c.Id == DeleteCommentModel.CommentId).FirstOrDefault();
            if (comment != null)
                _commentRepository.Delete(comment);
        }

        public Z_Consultant_Comment GetComment(int commentId)
        {
            var comment = _commentRepository.TableNoTracking
                .Include(c => c.Customer).Include(c => c.Consultant)
                .Where(c => c.Id == commentId).FirstOrDefault();
            return comment;

        }

        public Z_Consultant_Comment GetCommentByCommentIdAndConsultantId(int commentId, int consultantId)
        {
            var comment = _commentRepository.TableNoTracking
                .Include(c => c.Consultant).Where(c => c.Id == commentId && c.ConsultantId == consultantId)
                .FirstOrDefault();

            return comment;
        }

        public Z_Consultant_Comment GetCommentByCommentIdAndCustomerId(int commentId, int customerId)
        {
            var comment = _commentRepository.TableNoTracking
                .Include(c => c.Customer).Where(c => c.Id == commentId && c.CustomerId == customerId)
                .FirstOrDefault();

            return comment;
        }

        public PagedList<Z_Consultant_Comment> GetCommentsByPostId(PagingParams pagingParams, int postId)
        {
            var query = _commentRepository.TableNoTracking
                .Include(c => c.Customer).Include(c => c.Consultant)
                .Where(c => c.PostId == postId);

            query = query.OrderBy(c => c.DateCreated);
            return new PagedList<Z_Consultant_Comment>(
                query, pagingParams.PageNumber, pagingParams.PageSize);
        }

        public int GetPostIdByCommentId(int commentId)
        {
            return _commentRepository.TableNoTracking
                .Where(c => c.Id == commentId).Select(c => c.PostId).FirstOrDefault();
        }

        public bool IsCommentPostClosed(int CommentId)
        {
             var comment= _commentRepository.TableNoTracking
                .Include(c => c.Post).Where(c => c.Id == CommentId).FirstOrDefault();
            if(comment!=null)
            {
                return comment.Post.IsClosed;
            }
            return false;

        }

        public bool IsConsultantAuthToComment(int CommentId, int ConsultantId)
        {
            var comment = _commentRepository.TableNoTracking
                .Where(c => c.Id == CommentId && c.ConsultantId == ConsultantId).FirstOrDefault();

            if (comment == null)
                return false;

            return true;
        }

        public bool IsCustomerAuthToComment(int CommentId, int CustomerId)
        {
            var comment = _commentRepository.TableNoTracking
                .Where(c => c.Id == CommentId && c.CustomerId == CustomerId).FirstOrDefault();

            if (comment == null)
                return false;

            return true;
        }

        public bool IsExist(int CommentId)
        {
            var comment = _commentRepository.TableNoTracking.Where(c => c.Id == CommentId).FirstOrDefault();
            if (comment == null)
                return false;
            else
                return true;
        }

        public Z_Consultant_Comment UpdateComment(UpdateCommentModel UpdateCommentModel)
        {
            var comment = _commentRepository.Table.Where(c => c.Id == UpdateCommentModel.CommentId).FirstOrDefault();
            comment.Text = UpdateCommentModel.Text;
            _commentRepository.Update(comment);
            return comment;
        }
    }
}
