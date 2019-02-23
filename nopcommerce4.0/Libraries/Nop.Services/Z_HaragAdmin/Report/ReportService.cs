using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using System.Data.Entity;

namespace Nop.Services.Z_HaragAdmin.Report
{
    public class ReportService:IReportService
    {
        #region Fields
        private readonly IRepository<Z_Harag_Reports> _reportRepository;
        private readonly IRepository<Z_Harag_CommentReport > _commentReportRepository;
        #endregion


        #region Ctor
        public ReportService(IRepository<Z_Harag_Reports> reportRepository, IRepository<Z_Harag_CommentReport> commentReportRepository)
        {
            this._reportRepository = reportRepository;
            this._commentReportRepository = commentReportRepository;
        }
        #endregion



        #region Methods
        

        //Get Post Repository
        public List<Z_Harag_Reports> GetPostReports(int start, int length, string searchValue, string sortColumnName, string sortDirection)
        {
            var query = _reportRepository.TableNoTracking.Include(r=>r.Z_Harag_Post);

            //search
            if(!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(r => r.ReportDescription.Contains(searchValue) || r.ReportTitle.Contains(searchValue));
            }
            //sort
            if(!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
                query = query.OrderBy(sortColumnName + " " + sortDirection);
            }
            //pagining
            query = query.OrderByDescending(r=>r.ReportTitle).Skip(start).Take(length);

            return query.ToList();
                
        }

        //Get Comment Reports
        public List<Z_Harag_CommentReport> GetCommentReports(int start, int length, string searchValue, string sortColumnName, string sortDirection)
        {
            var query = _commentReportRepository.TableNoTracking;

            //search
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(r =>r.ReportTitle.Contains(searchValue));
            }
            //sort
            if (!string.IsNullOrEmpty(sortColumnName) && !string.IsNullOrEmpty(sortDirection))
            {
                query = query.OrderBy(sortColumnName + " " + sortDirection);
            }
            //pagining
            query = query.OrderByDescending(r => r.ReportTitle).Skip(start).Take(length);

            return query.ToList();

        }
        #endregion
    }
}
