using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core.Domain.Customers;

namespace Nop.Web.Models.Harag.Rate
{ 
    public class RateModel
    {
        public Core.Domain.Customers.Customer Customer{ get; set; }
        public Core.Domain.Customers.Customer User{ get; set; }

        public int UserId { get; set; }
        [Required(ErrorMessage = "ادخل التعليق")]
        [MinLength(50, ErrorMessage = "هذا التعليق قصير جدا")]
        public string RateComment { get; set; } 
        public string Username { get; set; }
        [Required(ErrorMessage = " حدد هل تم الدفع؟  ")]
        public bool IsBuyDone { get; set; }
        [Required(ErrorMessage = " حدد اذا تنصح بالتعامل ام لا")]
        public bool AdviceDeal { get; set; }
        [Required(ErrorMessage = "حدد متي تم الدفع")]
        public int WhenBuyDone { get; set; }
 
    }
}
