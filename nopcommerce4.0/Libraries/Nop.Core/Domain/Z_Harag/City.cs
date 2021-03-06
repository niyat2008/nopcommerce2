using Newtonsoft.Json;
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
         public City()
        { 
            this.Z_Harag_Post = new HashSet<Z_Harag_Post>();
        }
     
        public string ArName { get; set; }
        public string EnName { get; set; }
        public int ProvinceId { get; set; }
             /// 01067254988
             /// 
         [JsonIgnore]
         public virtual ICollection<Z_Harag_Post> Z_Harag_Post { get; set; }
    }
}
