using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Z_Harag.Helpers;

namespace Nop.Services.Z_Harag.Comment
{
    public interface ICommentService
    {
        List<Z_Harag_Comment> GetCommentsByPostId( int postId);
        Z_Harag_Comment AddComment(CommentForPostModel model, int currentUserId);
        Z_Harag_CommentReport ReportComment(Z_Harag_CommentReport model);
        Z_Harag_Comment GetCommentById(int id);
    }
}
