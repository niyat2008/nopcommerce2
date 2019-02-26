using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Z_Harag
{
   public class Z_Harag_FollowMap: NopEntityTypeConfiguration<Z_Harag_Follow>
    {
        public Z_Harag_FollowMap()
        {
            this.ToTable("Z_Harag_Follow");
            this.HasKey(b => b.Id);
            this.Property(b => b.CategoryId).IsOptional();
            this.Property(b => b.PostId).IsOptional();
            this.Property(b => b.UserId).IsRequired();
            this.Property(b => b.FollowedId).IsOptional(); 
            this.Property(b => b.CreatedTime).IsOptional(); 
            this.Property(b => b.UpdatedTime).IsOptional();
             
            this.HasOptional(m => m.Post).WithMany(p => p.FollowdPosts).HasForeignKey(m => m.PostId);
            this.HasOptional(m => m.Followed).WithMany(p => p.HaragFollowedUsers).HasForeignKey(m => m.FollowedId);
            this.HasRequired(m => m.User).WithMany(p => p.HaragFollowingUsers).HasForeignKey(m => m.UserId);
            this.HasOptional(m => m.Category).WithMany(p => p.FollowdCategory).HasForeignKey(m => m.CategoryId);
        }
    }
}
