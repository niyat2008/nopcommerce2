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
            this.FollowdCategory = new HashSet<Z_Harag_Follow>();
            this.NotificationCategories = new HashSet<Z_Harag_Notification>();

        }

        public string Name { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Z_Harag_Post> Posts { get; set; }
        public ICollection<Z_Harag_Follow> FollowdCategory { get; set; }
        public ICollection<Z_Harag_Notification> NotificationCategories { get; set; }
    }
}
