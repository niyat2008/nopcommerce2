using Nop.Core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.Z_Harag
{
    public class Z_Harag_Post : BaseEntity
    { 
        public Z_Harag_Post()
        {
            this.Z_Harag_Comment = new HashSet<Z_Harag_Comment>();
            this.Z_Harag_Favorite = new HashSet<Z_Harag_Favorite>(); 
            this.Z_Harag_Message = new HashSet<Z_Harag_Message>();
            this.Z_Harag_Notification = new HashSet<Z_Harag_Notification>();
            this.Z_Harag_Photo = new HashSet<Z_Harag_Photo>(); 
            this.Z_Harag_Reports = new HashSet<Z_Harag_Reports>();
            //this.Customer = new HashSet<Customer>();
        }
         
        public int CustomerId { get; set; }
        public int CategoryId { get; set; }
        public Nullable<int> CityId { get; set; }
        public Nullable<int> NeighborhoodId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string PaymentMethod { get; set; }
        public string Contact { get; set; }
        public Nullable<int> Rate { get; set; }
       // public bool IsSetToSubCategory { get; set; }
        public bool IsDispayed { get; set; }
        public bool IsReserved { get; set; }
        public Nullable<bool> IsCommon { get; set; }
        public bool IsClosed { get; set; }
        public bool IsAnswered { get; set; }
        public Nullable<Decimal> Longtiude { get; set; }
        public Nullable<Decimal> Lattiude { get; set; }
        public Nullable<System.DateTime> LastCommentTime { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateUpdated { get; set; }

        public virtual City City { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
  
        public  Customer Customer { get; set; } 
        public  Z_Harag_Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_Harag_Comment> Z_Harag_Comment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_Harag_Favorite> Z_Harag_Favorite { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")] 
        public virtual ICollection<Z_Harag_Message> Z_Harag_Message { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Z_Harag_Notification> Z_Harag_Notification { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<Z_Harag_Photo> Z_Harag_Photo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  Z_Harag_Rate Z_Harag_Rate { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<Z_Harag_Reports> Z_Harag_Reports { get; set; }
    }
}
