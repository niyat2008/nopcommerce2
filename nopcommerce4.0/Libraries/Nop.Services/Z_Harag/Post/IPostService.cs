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
        Z_Harag_Post UpdatePost(PostForPostListModel postForPostModel, int currentUserId, IList<string> files, List<string> errors);

        List<Z_Harag_Post> GetFeaturedPosts( PagingParams pagingParams);
        // get Cities
        List<City> GetCities();
        List<Neighborhood> GetNeighborhoods(int cityId);
        // can add post
        bool CanAddNewPost(int userId);
        bool EditHaragPost(int postId, Z_Harag_Post post);
        List<Z_Harag_Post> SearchByCategory(int catId, PagingParams pagingParams); 
        List<Z_Harag_Post> SearchByCategoryPage(int catId, PagingParams pagingParams);
        List<Z_Harag_Post> SearchByCity(int catId, PagingParams pagingParams); 
        List<Z_Harag_Post> SearchByCityPage(int catId, PagingParams pagingParams);
        List<Z_Harag_Post> SearchByDate(DateTime date, PagingParams pagingParams);
        List<Z_Harag_Post> SearchByNeighborhood(int catId, PagingParams pagingParams);
        List<Z_Harag_Post> GetUserPosts(int catId, PagingParams pagingParams);
        List<Z_Harag_Post> GetCurrentUserPosts(int catId, PagingParams pagingParams);
        int UpdatePostLocation(UpdatePostLocationModel post);
        bool AddPostToFavorite(int postId, int userId);
        bool AddPostToBlackList(int postId, int userId);
        bool FollowReplysOnPost(int postId, int userId);
        bool IsPostAddedToFavorite(int postId, int userId);
        Z_Harag_Reports ReportPost(Z_Harag_Reports report);
        bool RemovePostFromFavorite(int postId, int id);
        City GetCity(string city);
        List<Z_Harag_Post> GetFavoritesPosts(int id, PagingParams pagingParams);
        List<Z_Harag_Post> SearchPosts(SearchModel searchModel, PagingParams pagingParams);
         List<Z_Harag_Post> SearchPostsCatCity(int cat, int city, PagingParams pagingParams);
        List<Z_Harag_Post> GetLatestPosts(PagingParams pagingParams);
        bool DeletePost(DeletePost deleteData);
    }
}
