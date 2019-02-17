using Microsoft.AspNetCore.Http;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Z_Harag.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Post
{
    public interface IPostService
    {
        Z_Harag_Post GetPost(int postId, string type);

        bool IsExists(int postId);
        bool IsDisplayed(int postId); 
        Z_Harag_Post AddNewPost(PostForPostListModel postForPostModel, int currentUserId, IList<string> files, List<string> errors);
        List<Z_Harag_Post> GetFeaturedPosts();
        // get Cities
        List<City> GetCities();
    }
}
