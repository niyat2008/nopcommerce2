using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Follow
{
   public interface IFollowService
    {
        List<Z_Harag_Follow> GetFollowedUsers(int userId);  
        List<Z_Harag_Follow> GetFollowedPosts(int userId); 
        List<Z_Harag_Follow> GetFollowedCategory(int userId); 

        // 
        Z_Harag_Follow AddPostToFollow(Z_Harag_Follow postId); 
        Z_Harag_Follow AddUserToFollow(Z_Harag_Follow userId); 
        Z_Harag_Follow AddCategoryToFollow(Z_Harag_Follow categoryId);

        bool RemovePostFromFollow(int postId, int id);
        bool RemoveUserFromFollow(int userId, int id);
        bool RemoveCategoryFromFollow(int catId, int id);

        bool IsPostFollowed (int id, int userId);
        bool IsUserFollowed (int id, int userId);
        bool IsCatFollowed (int id, int userId);

    }
}
