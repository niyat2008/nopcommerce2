using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Harag
{
    public partial class City:BaseEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public City()
        {
            this.Z_Harag_Post = new HashSet<Z_Harag_Post>();
        }
     
        public string ArName { get; set; }
        public string EnName { get; set; }
        public int ProvinceId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_Harag_Post> Z_Harag_Post { get; set; }
    }
}
