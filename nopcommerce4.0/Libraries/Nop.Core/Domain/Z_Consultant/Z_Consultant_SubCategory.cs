using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Consultant
{
    public class Z_Consultant_SubCategory : BaseEntity
    {
        public Z_Consultant_SubCategory()
        {
            Posts = new Collection<Z_Consultant_Post>();
        }

        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }

        public Z_Consultant_Category Category { get; set; }
        public ICollection<Z_Consultant_Post> Posts { get; set; }
    }
}
