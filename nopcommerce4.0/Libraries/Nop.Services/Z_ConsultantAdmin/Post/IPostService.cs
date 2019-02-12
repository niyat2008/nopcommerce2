using Nop.Core.Domain.Z_Consultant;
using Nop.Services.Z_ConsultantAdmin.Customers;
using Nop.Services.Z_ConsultantAdmin.Post;

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
        List<Z_Consultant_Post> GetClosedDisplayedPosts(int start, int length, string searchValue, string sortColumnName, string sortDirection);

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
        //Post Edit
        void SetPostToCommon(int id);



        // Closed Not Displayed
        List<Z_Consultant_Post> GetClosedNotDisplayedPosts(int start, int length, string searchValue, string sortColumnName, string sortDirection);
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
        List<Z_Consultant_Post> GetNotRepliedPosts(int start, int length, string searchValue, string sortColumnName, string sortDirection);
        //Not Replied With Category Id
        List<Z_Consultant_Post> GetNotRepliedPostsWithCategoryId(int categoryId);
        //Not Replied With SubCategory Id
        List<Z_Consultant_Post> GetNotRepliedPostsWithSubCategoryId(int subCategoryId);





        //IS Reserved
        List<Z_Consultant_Post> GetIsReservedPosts(int start, int length, string searchValue, string sortColumnName, string sortDirection);
        //IS Reserved With Category Id
        List<Z_Consultant_Post> GetIsReservedPostsWithCategoryId(int categoryId);
        //IS Reserved With SubCategory Id
        List<Z_Consultant_Post> GetIsReservedPostsWithSubCategoryId(int subCategoryId);
        // Closed Not Displayed Post Cancel Reserving
        void PostCancelReserving(int postId);






        //Open Posts
        List<Z_Consultant_Post> GetOpenPosts(int start, int length, string searchValue, string sortColumnName, string sortDirection);
        //Open Post With Category Id
        List<Z_Consultant_Post> GetOpenPostsWithCategoryId(int categoryId);
        //Open Posts With SubCategory Id
        List<Z_Consultant_Post> GetOpenPostsWithSubCategoryId(int subCategoryId);



        //Get  Posts By Customer Id
        List<Z_Consultant_Post> GetCustomerPosts(int CustomersId,string type, int start, int length, string searchValue, string sortColumnName, string sortDirection, DateTime? from=null, DateTime? to=null);
        //Get Customer Post By PostId and CustomerId
        List<Z_Consultant_Post> GetCustomerPostById(int customerId, int postId, string type);


        //Get  Posts By Consultant Id
        List<Z_Consultant_Post> GetConsultantPosts(int CustomersId, string type, int start, int length, string searchValue, string sortColumnName, string sortDirection, DateTime? from= null, DateTime? to = null);
        //Get Consultant Post By PostId and ConsultantId
        List<Z_Consultant_Post> GetConsultantPostById(int ConsultantId, int postId, string type);

        //Get Top 20 Member By Posts number
        List<Top20CustomerByPostsNumber> GetTop20MemberByPostsNumber();

        //Get Top 20 Consultant By Posts number
        List<Top20CustomerByPostsNumber> GetTop20ConsultantByPostsNumber();


        //Get  Posts number
        int GetPostsNumber();
        



    }
}
