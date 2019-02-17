using Microsoft.AspNetCore.Hosting;
using Nop.Core.Data;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Reports
{
   public class ReportService: IReportService
    {
        private readonly IRepository<Z_Harag_Reports> _reportService;
        private readonly IRepository<Z_Harag_Comment> _commentService;
        private readonly IRepository<Z_Harag_CommentReport> _commentReportService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IHostingEnvironment _env;


        public ReportService(IRepository<Z_Harag_Reports> reportService, IRepository<Z_Harag_Comment> commentService, IRepository<Z_Harag_CommentReport> commentReportService, IEventPublisher eventPublisher, IHostingEnvironment env)
        {
            this._reportService = reportService;
            this._commentReportService = commentReportService;
            this._commentService = commentService;
            this._eventPublisher = eventPublisher;
            this._env = env;
        }
    }
}
