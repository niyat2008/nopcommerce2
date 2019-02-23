using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Z_HaragAdmin.Report;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Z_Harag.Helpers;
using Nop.Web.Models.HaragAdmin.Reports;

namespace Nop.Web.Controllers.HaragAdmin
{
    public class ReportController : Controller
    {

        #region Fields
        private readonly IReportService _reportService;
        private readonly IWorkContext _workContext;
        
        #endregion


        #region Ctor
        public ReportController(IReportService reportService, IWorkContext workContext)
        {
            this._reportService = reportService;
            this._workContext = workContext;
        }
        #endregion


        #region Actions
        //Get Post Reports
        public IActionResult GetPostReports()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            return View("~/Themes/Pavilion/Views/HaragAdmin/Reports/PostReports.cshtml");
        }

        //Get Post Reports Ajax
        [HttpPost]
        public IActionResult GetPostReportsAjax()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            //Server Side Parameters
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            //int startRec = Request.Form.GetValues("start").First;
            //int start = Convert.ToInt32(Request.Form.GetValues("start")[0]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];
            

            var postReportInDb = _reportService.GetPostReports(start, length, searchValue, sortColumnName, sortDirection);
               

            var postReport = new ReportOutputModel
            {
                Items= postReportInDb.Select(r=>new ReportModel
                {
                    Id=r.Id,
                    PostId=r.Z_Harag_Post?.Id,
                    ReportTitle=r.ReportTitle,
                    ReportDescription=r.ReportDescription,
                    IsIllegal=r.IsIllegal
                }).ToList()
            };

            return Json(new { data = postReport.Items });
        }

        //Get Comment Reports
        public IActionResult GetCommentReports()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            return View("~/Themes/Pavilion/Views/HaragAdmin/Reports/CommentReports.cshtml");
        }

        //Get Comment Reports Ajax
        [HttpPost]
        public IActionResult GetCommentReportsAjax()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            if (!_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Administrators, true))
                return Forbid();


            //Server Side Parameters
            var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
            //int startRec = Request.Form.GetValues("start").First;
            //int start = Convert.ToInt32(Request.Form.GetValues("start")[0]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"] + "][name]"];
            string sortDirection = Request.Form["order[0][dir]"];


            var postReportInDb = _reportService.GetCommentReports(start, length, searchValue, sortColumnName, sortDirection);


            var postReport = new ReportOutputModel
            {
                Items = postReportInDb.Select(r => new ReportModel
                {
                    Id = r.Id,
                    
                    ReportTitle = r.ReportTitle,
                    ReportDescription = r.ReportDescription,
                   Category=r.ReportCategory
                }).ToList()
            };

            return Json(new { data = postReport.Items });
        }
        #endregion

    }
}