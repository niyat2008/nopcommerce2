using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Consultant
{
    public class Z_Consultant_Comment : BaseEntity
    {
        

        //[Required]
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        //[Required]
        //[StringLength(100)]
        public string CommentedBy { get; set; }


        //[ForeignKey("Customer"), Column(Order = 1)]
        public int? CustomerId { get; set; }

        //[ForeignKey("Consultant"), Column(Order = 2)]
        public int? ConsultantId { get; set; }

        //[ForeignKey("Post")]
        public int PostId { get; set; }

        //[InverseProperty("CustomersComments")]
        public Customer Customer { get; set; }

        //[InverseProperty("ConsultantsComments")]
        public Customer Consultant { get; set; }
        public Z_Consultant_Post Post { get; set; }
    }
}
