using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Harag
{
   public class Z_Harag_Category:BaseEntity
    {
        public Z_Harag_Category()
        {
           
            Posts = new Collection<Z_Harag_Post>();
        }
         
        public string Name { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Z_Harag_Post> Posts { get; set; }

       
    }
}
