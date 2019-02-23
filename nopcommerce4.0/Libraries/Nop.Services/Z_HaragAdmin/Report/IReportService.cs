using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.Report
{
   public interface IReportService
    {
        //Get Post Repository
        List<Z_Harag_Reports> GetPostReports(int start, int length, string searchValue, string sortColumnName, string sortDirection);

        //Get Comment Reports
         List<Z_Harag_CommentReport> GetCommentReports(int start, int length, string searchValue, string sortColumnName, string sortDirection);
    }
}
