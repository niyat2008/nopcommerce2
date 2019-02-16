using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Harag
{
    public class Z_Harag_Comment : BaseEntity
    {


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Z_Harag_Comment()
        {
            this.Z_Harag_CommentReport = new HashSet<Z_Harag_CommentReport>();
           
        }
         
        public Nullable<int> CustomerId { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
        public string CommentedBy { get; set; }

        public virtual Customer Customer { get; set; }
     
        public virtual Z_Harag_Post Z_Harag_Post { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_Harag_CommentReport> Z_Harag_CommentReport { get; set; }
    }
}
