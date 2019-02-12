using Nop.Core.Domain.Z_Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_ConsultantAdmin.SubCategories
{
    public class SubCategoryForPost
    {
        public IEnumerable<Z_Consultant_Category> Categories { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
