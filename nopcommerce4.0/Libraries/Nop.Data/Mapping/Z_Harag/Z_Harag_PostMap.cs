using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Harag
{
    public class Z_Harag_PostMap: NopEntityTypeConfiguration<Z_Harag_Post>
    {
        public Z_Harag_PostMap()
        {
            this.ToTable("Z_Harag_Post");
            this.HasKey(p => p.Id);

            this.Property(p => p.Text).IsRequired().HasMaxLength(4000);
            this.Property(p => p.Title).IsRequired().HasMaxLength(4000);
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsOptional();
            this.Property(p => p.IsCommentingClosed).IsRequired();
            this.Property(p => p.IsAnswered).IsRequired();
            this.Property(p => p.IsCommon).IsRequired();
            //this.Property(p => p.IsSetToSubCategory).IsRequired();
            //this.Property(p => p.Rate).IsOptional();
            this.Property(p => p.IsDispayed).IsRequired();
            this.Property(p => p.IsFeatured).IsRequired();
            this.Property(p => p.LastCommentTime).IsOptional();
            this.Property(p => p.IsCommon).IsOptional();
            this.Property(p => p.Longtiude).IsOptional();
            this.Property(p => p.Lattiude).IsOptional();


            this.HasMany(p => p.Z_Harag_Comment);
            //this.HasMany(c => c.Z_Harag_Favorite);
            this.HasMany(c => c.Z_Harag_Message);
            this.HasMany(c => c.Z_Harag_Notification);
            this.HasMany(c => c.Z_Harag_Photo);
            //this.HasOptional(p => p.Z_Harag_Rate).WithRequired(c => c.Z_Harag_Post);
            this.HasMany(c => c.Z_Harag_Reports);
            this.HasOptional(p => p.City).WithMany(c => c.Z_Harag_Post).HasForeignKey(p => p.CityId);
            this.Property(p => p.PaymentMethod).IsOptional().HasMaxLength(50);
            this.Property(p => p.Contact).IsOptional().HasMaxLength(100); 


            this.HasRequired(p => p.Customer)
                .WithMany(c => c.Z_Harag_Posts)
                .HasForeignKey(p => p.CustomerId);

          

            this.HasRequired(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId);

           
             
        }
    }
}
