using Nop.Core.Domain.Z_Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Consultant
{
   public class Z_Consultant_NotificationMap : NopEntityTypeConfiguration<Z_Consultant_Notification>
    {
        public Z_Consultant_NotificationMap()
        {
            this.ToTable("Z_Consultant_Notification");
            this.HasKey(n => n.Id);
           
            this.Property(n => n.Type);
            this.Property(n => n.Time);
            this.Property(n => n.IsRead);

            this.HasRequired(p => p.Post)
                .WithMany(n => n.Notification)
                .HasForeignKey(p => p.PostId);

            this.HasRequired(o => o.Owner)
                         .WithMany(n => n.OwnerNotification)
                         .HasForeignKey(o => o.OwnerId);

            this.HasOptional(u => u.User)
                .WithMany(n => n.UserNotification)
                .HasForeignKey(u => u.UserId);



        }
    }
}
