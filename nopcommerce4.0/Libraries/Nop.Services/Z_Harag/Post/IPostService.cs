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
        List<Neighborhood> GetNeighborhoods(int cityId);
        // can add post
        bool CanAddNewPost(int userId);
        bool EditHaragPost(int postId, Z_Harag_Post post);
        List<Z_Harag_Post> SearchByCategory(int catId);
        List<Z_Harag_Post> SearchByCategory(int catId, int count);
        List<Z_Harag_Post> SearchByCategoryPage(int catId, int count);
        List<Z_Harag_Post> SearchByCity(int catId);
        List<Z_Harag_Post> SearchByCity(int catId, int count); 
        List<Z_Harag_Post> SearchByCityPage(int catId, int count);
        List<Z_Harag_Post> SearchByDate(int catId);
        List<Z_Harag_Post> SearchByNeighborhood(int catId);
        List<Z_Harag_Post> GetUserPosts(int catId);
        List<Z_Harag_Post> GetCurrentUserPosts(int catId);
        bool UpdatePostLocation(int postId, Z_Harag_Post post);
        bool AddPostToFavorite(int postId, int userId);
        bool AddPostToBlackList(int postId, int userId);
        bool FollowReplysOnPost(int postId, int userId);
        bool IsPostAddedToFavorite(int postId, int userId);
        Z_Harag_Reports ReportPost(Z_Harag_Reports report);
        bool RemovePostFromFavorite(int postId, int id);
        City GetCity(string city);
        List<Z_Harag_Post> GetFavoritesPosts(int id);
        List<Z_Harag_Post> SearchPosts(SearchModel searchModel);
         List<Z_Harag_Post> SearchPostsCatCity(int cat, int city);
    }
}
