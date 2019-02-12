using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Consultant
{
    public class Z_Consultant_Post : BaseEntity
    {
        public Z_Consultant_Post()
        {
            Comments = new Collection<Z_Consultant_Comment>();
            Photos = new Collection<Z_Consultant_Photo>();
            Videos = new Collection<Z_Consultant_Video>();
        }



        //[Required]
        public string Text { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool IsClosed { get; set; }
        public bool IsAnswered { get; set; }

        public bool IsDispayed { get; set; }
        public bool IsCommon { get; set; }

        public bool IsReserved { get; set; }
        public bool IsCommon { get; set; }

        public bool IsSetToSubCategory { get; set; }

        public int? Rate { get; set; }

        public DateTime? LastCommentTime { get; set; }

        public virtual ICollection<Z_Consultant_Photo> Photos { get; set; }
        public virtual ICollection<Z_Consultant_Video> Videos { get; set; }




        //[ForeignKey("Customer"), Column(Order = 1)]
        public int CustomerId { get; set; }

        //[ForeignKey("Consultant"), Column(Order = 2)]
        public int? ConsultantId { get; set; }

        //[ForeignKey("SubCategory")]
        public int? SubCategoryId { get; set; }

        //[ForeignKey("Category")]
        public int CategoryId { get; set; }

        //[InverseProperty("CustomersPosts")]
        public Customer Customer { get; set; }

        //[InverseProperty("ConsultantsPosts")]
        public Customer Consultant { get; set; }
        public Z_Consultant_SubCategory SubCategory { get; set; }
        public Z_Consultant_Category Category { get; set; }

        public virtual ICollection<Z_Consultant_Comment> Comments { get; set; }

    }
}
