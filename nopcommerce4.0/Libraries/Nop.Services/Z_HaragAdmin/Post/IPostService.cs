using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.Post
{
   public interface IPostService
    {
        //Get All Posts
        List<Z_Harag_Post> GetAllPosts();
    }
}
