using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.HaragAdmin.Reports
{
    public class ReportModel : BaseNopEntityModel
    {
        public int Id { get; set; }
        public string ReportDescription { get; set; }
        public string CustomerName { get; set; }
        public string PostTitle { get; set; }
        public string ReportTitle { get; set; }
        public string CommentTitle { get; set; }
        public int CommentId { get; set; }
        public string Comment { get; set; }
        public byte? Category { get; set; }



    }
}
