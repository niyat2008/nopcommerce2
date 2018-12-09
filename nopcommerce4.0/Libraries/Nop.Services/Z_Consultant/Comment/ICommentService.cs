using Nop.Core.Domain.Z_Consultant;
using Nop.Services.Z_Consultant.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Consultant.Comment
{
    public interface ICommentService
    {
        PagedList<Z_Consultant_Comment> GetCommentsByPostId(PagingParams pagingParams, int postId);
        int GetPostIdByCommentId(int commentId);
        Z_Consultant_Comment GetCommentByCommentIdAndCustomerId(int commentId, int customerId);
        Z_Consultant_Comment GetCommentByCommentIdAndConsultantId(int commentId, int consultantId);
        Z_Consultant_Comment GetComment(int commentId);
        Z_Consultant_Comment AddCommentByCustomer(CommentForPostModel CommentForPostDto, int customerId);
        Z_Consultant_Comment AddCommentByConsultant(CommentForPostModel CommentForPostDto, int consultantId);
        Z_Consultant_Comment UpdateComment(UpdateCommentModel UpdateCommentModel);
        void DeleteComment(DeleteCommentModel DeleteCommentModel);
        bool IsCustomerAuthToComment(int CommentId,int CustomerId);
        bool IsConsultantAuthToComment(int CommentId, int ConsultantId);
        bool IsExist(int CommentId);
        bool IsCommentPostClosed(int CommentId);
    }
}
