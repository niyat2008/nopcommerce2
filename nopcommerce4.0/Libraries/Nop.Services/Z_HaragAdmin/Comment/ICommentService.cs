using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.Comment
{
    public interface ICommentService
    {
        //Post Comments
        List<Z_Harag_Comment> GetPostComments(int postId);
        //Comment Reports
        List<Z_Harag_CommentReport> GetCommentReports(int commentId);
        //Get Comment By Id
        Z_Harag_Comment GetComment(int commentId);
    }
}
