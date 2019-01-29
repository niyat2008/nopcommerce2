using Nop.Core.Domain.Z_Consultant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_ConsultantAdmin
{
  public  interface IPostService
    {
        // Displayed Closed
        List<Z_Consultant_Post> GetClosedDisplayedPosts();

        //Displayed Closed With Category Id
        List<Z_Consultant_Post> GetClosedDisplayedPostsWithCategoryId(int categoryId);
        //Displayed Closed With SubCategory Id
        List<Z_Consultant_Post> GetClosedDisplayedPostsWithSubCategoryId(int subCategoryId);
        //Get Post By Id
        List<Z_Consultant_Post> GetPostById(int postId,string type);
        //Remove Closed Displayed Post
        void RemovePost(int postId);
        //Get Photos
        List<Z_Consultant_Photo> GetPhotos(int postId);
        //Get  Post Details
        Z_Consultant_Post GetPostDetails(int postId);
        //Get Post Comments
        List<Z_Consultant_Comment> GetPostComments(int postId);
        //Get Posts By Date
        List<Z_Consultant_Post> GetPostsByDate(DateTime firstDate, DateTime secondDate ,string type);
        



        // Closed Not Displayed
        List<Z_Consultant_Post> GetClosedNotDisplayedPosts();
        // Closed Not Displayed With Category Id
        List<Z_Consultant_Post> GetClosedNotDisplayedPostsWithCategoryId(int categoryId);
        // Closed Not Displayed With SubCategory Id
        List<Z_Consultant_Post> GetClosedNotDisplayedPostsWithSubCategoryId(int subCategoryId);
        //Remove Closed Not Displayed Post
        //void RemoveClosedNotDisplayedPost(int postId);
        //Get Closed Not Displayed Post Details
        // Z_Consultant_Post GetClosedNotDisplayedPostsDetails(int postId);
        //  Post Display
        void PostDisplay(int postId);





        //Not Replied
        List<Z_Consultant_Post> GetNotRepliedPosts();
        //Not Replied With Category Id
        List<Z_Consultant_Post> GetNotRepliedPostsWithCategoryId(int categoryId);
        //Not Replied With SubCategory Id
        List<Z_Consultant_Post> GetNotRepliedPostsWithSubCategoryId(int subCategoryId);





        //IS Reserved
        List<Z_Consultant_Post> GetIsReservedPosts();
        //IS Reserved With Category Id
        List<Z_Consultant_Post> GetIsReservedPostsWithCategoryId(int categoryId);
        //IS Reserved With SubCategory Id
        List<Z_Consultant_Post> GetIsReservedPostsWithSubCategoryId(int subCategoryId);
        // Closed Not Displayed Post Cancel Reserving
        void PostCancelReserving(int postId);






        //Open Posts
        List<Z_Consultant_Post> GetOpenPosts();
        //Open Post With Category Id
        List<Z_Consultant_Post> GetOpenPostsWithCategoryId(int categoryId);
        //Open Posts With SubCategory Id
        List<Z_Consultant_Post> GetOpenPostsWithSubCategoryId(int subCategoryId);

    }
}
