using Microsoft.AspNetCore.Http;
using Nop.Core.Domain.Z_Consultant;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Z_Consultant.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Consultant.Post
{
    public interface IPostService
    {
        PagedList<Z_Consultant_Post> GetClosedPosts(PagingParams pagingParams);
        PagedList<Z_Consultant_Post> GetClosedPostsAdmin(PagingParams pagingParams);
        PagedList<Z_Consultant_Post> GetClosedPostsByCategoryId(PagingParams pagingParams, int categoryId);
        PagedList<Z_Consultant_Post> GetClosedPostsByCategoryIdAdmin(PagingParams pagingParams, int categoryId);
        PagedList<Z_Consultant_Post> GetClosedPostsBySubCategoryId(PagingParams pagingParams, int subCategoryId);
        PagedList<Z_Consultant_Post> GetClosedPostsBySubCategoryIdAdmin(PagingParams pagingParams, int subCategoryId);
        PagedList<Z_Consultant_Post> GetOpenPostsForCustomer(PagingParams pagingParams, int customerId);
        PagedList<Z_Consultant_Post> GetOpenPostsForCustomerByCategoryId(PagingParams pagingParams, int categoryId, int customerId);
        PagedList<Z_Consultant_Post> GetOpenPostsForCustomerBySubCategoryId(PagingParams pagingParams, int subCategoryId, int customerId);
        PagedList<Z_Consultant_Post> GetClosedPostsForCustomer(PagingParams pagingParams, int customerId);
        PagedList<Z_Consultant_Post> GetClosedPostsForCustomerByCategoryId(PagingParams pagingParams, int categoryId, int customerId);
        PagedList<Z_Consultant_Post> GetClosedPostsForCustomerBySubCategoryId(PagingParams pagingParams, int subCategoryId, int customerId);
        PagedList<Z_Consultant_Post> GetPostsForCustomer(PagingParams pagingParams, int customerId);
        PagedList<Z_Consultant_Post> GetPostsForCustomerByCategoryId(PagingParams pagingParams, int categoryId, int customerId);
        PagedList<Z_Consultant_Post> GetPostsForCustomerBySubCategoryId(PagingParams pagingParams, int subCategoryId, int customerId);
        Z_Consultant_Post AddNewPost(PostForPostModel postForPostModel, int cutomerId, IList<string> files, List<string> errors);
        bool CloseAndRatePost(CloseAndRatePostModel closeAndRatePostDto, int customerId);
        bool ClosePost(ClosePostModel closePostDto, int customerId);
        bool RatePost(RatePostModel ratePostDto, int customerId);
        bool IsCustomerAuthToPost(int postId, int customerId);
        bool IsRated(int postId);
        PagedList<Z_Consultant_Post> GetOpenPostsNotAns(PagingParams pagingParams);
        PagedList<Z_Consultant_Post> GetOpenPostsNotAnsByCategoryId(PagingParams pagingParams, int categoryId);
        PagedList<Z_Consultant_Post> GetOpenPostsNotAnsBySubCategoryId(PagingParams pagingParams, int subCategoryId);
        PagedList<Z_Consultant_Post> GetOpenPostsForConsultant(PagingParams pagingParams, int consultantId);
        PagedList<Z_Consultant_Post> GetReservedPostsForConsultant(PagingParams pagingParams, int consultantId);
        PagedList<Z_Consultant_Post> GetOpenPostsForConsultantByCategoryId(PagingParams pagingParams, int categoryId, int consultantId);
        PagedList<Z_Consultant_Post> GetReservedPostsForConsultantByCategoryId(PagingParams pagingParams, int categoryId, int consultantId);
        PagedList<Z_Consultant_Post> GetOpenPostsForConsultantBySubCategoryId(PagingParams pagingParams, int subCategoryId, int consultantId);
        PagedList<Z_Consultant_Post> GetReservedPostsForConsultantBySubCategoryId(PagingParams pagingParams, int subCategoryId, int consultantId);
        PagedList<Z_Consultant_Post> GetClosedPostsForConsultant(PagingParams pagingParams, int consultantId);
        PagedList<Z_Consultant_Post> GetClosedPostsForConsultantByCategoryId(PagingParams pagingParams, int categoryId, int consultantId);
        PagedList<Z_Consultant_Post> GetClosedPostsForConsultantBySubCategoryId(PagingParams pagingParams, int subCategoryId, int consultantId);
        PagedList<Z_Consultant_Post> GetPostsForConsultant(PagingParams pagingParams, int consultantId);
        PagedList<Z_Consultant_Post> GetPostsForConsultantByCategoryId(PagingParams pagingParams, int categoryId, int consultantId);
        PagedList<Z_Consultant_Post> GetPostsForConsultantBySubCategoryId(PagingParams pagingParams, int subCategoryId, int consultantId);
        bool IsConsultantAuthToPost(int postId, int consultantId);
        bool IsExist(int postId);
        bool IsCategoryConatinTheSub(SetPostToSubCategoryModel setPostToSubCategory);
        bool IsCategoryConatinTheSub(SetPostToCategoryAndSubCategoryModel setPostToCategoryAnSub);
        void SetPostToSubCategory(SetPostToSubCategoryModel setPostToSubCategory);
        void SetPostToCategory(SetPostToCategoryModel setPostToCategory);
        void SetPostToCategoryAndSubCategory(SetPostToCategoryAndSubCategoryModel setPostToCategoryAndSub);
        bool IsClosed(int postId);
        bool IsAnswered(int postId);
        bool IsReserved(int postId);
        void ReservePost(int postId,int ConsultantId);
        void UnReservePost(int postId);
        Z_Consultant_Post GetPost(int postId);
        void SetPostAnsweredByConsultant(int postId, int consultantId);

        PagedList<Z_Consultant_Post> SearchPosts(PagingParams pagingParams, SearchModel searchModel);
        Z_Consultant_Post GetPostById(int postId);

        //admin

        PagedList<Z_Consultant_Post> GetPosts(PagingParams pagingParams);
        PagedList<Z_Consultant_Post> GetPostsByCategoryId(PagingParams pagingParams, int categoryId);
        PagedList<Z_Consultant_Post> GetPostsBySubCategoryId(PagingParams pagingParams, int subCategoryId);
        PagedList<Z_Consultant_Post> GetOpenPostsAns(PagingParams pagingParams);
        PagedList<Z_Consultant_Post> GetOpenPostsAnsByCategoryId(PagingParams pagingParams, int categoryId);
        PagedList<Z_Consultant_Post> GetOpenPostsAnsBySubCategoryId(PagingParams pagingParams, int subCategoryId);
        PagedList<Z_Consultant_Post> GetOpenPosts(PagingParams pagingParams);
        PagedList<Z_Consultant_Post> GetOpenPostsByCategoryId(PagingParams pagingParams, int categoryId);
        PagedList<Z_Consultant_Post> GetOpenPostsBySubCategoryId(PagingParams pagingParams, int subCategoryId);
        PagedList<Z_Consultant_Post> GetClosedDisplayedPosts(PagingParams pagingParams);
        PagedList<Z_Consultant_Post> GetClosedDisplayedPostsByCategoryId(PagingParams pagingParams, int categoryId);
        PagedList<Z_Consultant_Post> GetClosedDisplayedPostsBySubCategoryId(PagingParams pagingParams, int subCategoryId);
        PagedList<Z_Consultant_Post> GetClosedHiddenPosts(PagingParams pagingParams);
        PagedList<Z_Consultant_Post> GetClosedHiddenPostsByCategoryId(PagingParams pagingParams, int categoryId);
        PagedList<Z_Consultant_Post> GetClosedHiddenPostsBySubCategoryId(PagingParams pagingParams, int subCategoryId);
        void DisplayPost(int PostId);
        void HidePost(int PostId);
        PagedList<Z_Consultant_Post> GetCommonPostsForCustomer(PagingParams pagingParams, int currentUserId);

        List<City> GetCities();
        List<Neighborhood> GetCityNeighborhood(int cityId);


        //Get Reserved Post By Id
         //Z_Consultant_Post GetReservedPostById(int postId);
    }
}
