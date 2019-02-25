using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Harag
{
    public class Z_Harag_NotificationMap : NopEntityTypeConfiguration<Z_Harag_Notification>
    {
        public Z_Harag_NotificationMap()
        {
            this.ToTable("Z_Harag_Notification");
            this.HasKey(p => p.Id);

            this.Property(p => p.CategoryId).IsOptional();
            this.Property(p => p.NotificationContent).IsOptional().HasMaxLength(4000);
            this.Property(p => p.NotificationTime).IsOptional(); 
            this.Property(p => p.NotificationType).IsOptional();
            this.Property(p => p.OwnerId).IsOptional(); 
            this.Property(p => p.CustomerId).IsOptional(); 

            this.HasOptional(p => p.Owner)
                .WithMany(p => p.Z_Harag_Notification)
                .HasForeignKey(p => p.OwnerId);

            this.HasOptional(p => p.Customer)
            .WithMany(p => p.HaragCustomerNotification)
            .HasForeignKey(p => p.CustomerId);
             
            this.HasOptional(p => p.Category)
            .WithMany(p => p.NotificationCategories)
            .HasForeignKey(p => p.CategoryId);

            this.HasOptional(p => p.Z_Harag_Post)
            .WithMany(p => p.Z_Harag_Notification)
            .HasForeignKey(p => p.PostId);
        }
    }
}
