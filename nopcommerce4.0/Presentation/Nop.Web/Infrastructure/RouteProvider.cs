using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Web.Infrastructure
{
    /// <summary>
    /// Represents provider that provided basic routes
    /// </summary>
    public partial class RouteProvider : IRouteProvider
    {
        #region Methods

        /// <summary>
        /// Register routes
        /// </summary>
        /// <param name="routeBuilder">Route builder</param>
        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            // ----------------  Harag ------------------------------
            // done
            routeBuilder.MapRoute("Harag.Home", "Harag",
            new { controller = "HaragHome", action = "Home" });

            routeBuilder.MapRoute("Harag.DisallaowedProducts", "Harag/NotAllowed",
            new { controller = "HaragHome", action = "DisallaowedProducts" });


            routeBuilder.MapRoute("Harag.FeaturedProducts", "Harag/FeaturedProducts",
            new { controller = "HaragHome", action = "FeaturedProducts" });


            routeBuilder.MapRoute("Harag.Agrement", "Harag/Terms",
            new { controller = "HaragHome", action = "Agrement" });
             

            // Add New Post  
            routeBuilder.MapRoute("Harag.Post.AddNewPost", "Harag/Post/New",
            new { controller = "Post", action = "HaragAddPost" });
            // done 
            routeBuilder.MapRoute("Harag.Post.AddNewPostAjax", "Harag/Post/NewAjax",
            new { controller = "Post", action = "AddNewPostAjax" });
            // Get POst
            routeBuilder.MapRoute("Harag.Post.GetHaragPost", "Harag/Post/{postId?}",
            new { controller = "Post", action = "GetHaragPost" });
             
            // update the location 
            routeBuilder.MapRoute("Harag.Post.UpdatePostLocation", "Harag/UpdateLocation/{postId?}",
            new { controller = "Post", action = "UpdatePostLocation" });

            // update location ajax
            routeBuilder.MapRoute("Harag.Post.UpdatePostLocationAjax", "Harag/UpdateLocationAjax",
            new { controller = "Post", action = "UpdatePostLocationAjax" });


            // done get all posts home 
            routeBuilder.MapRoute("Harag.Post.GetAllHaragPostsAjax", "Harag/PostsAjax",
            new { controller = "Post", action = "GetAllHaragPostsAjax" });
              
            // add to favorite
            routeBuilder.MapRoute("Harag.Post.AddPostToFavorite", "Harag/AddPostToFavorite",
            new { controller = "Post", action = "AddPostToFavorite" });


            // waiting
            routeBuilder.MapRoute("Harag.Post.GetAllFeaturedPosts", "Harag/FeaturedPostsAjax",
            new { controller = "Post", action = "GetAllFeaturedPosts" });
            // waiting
            routeBuilder.MapRoute("Harag.Home.GetAllSideBarTags", "Harag/TagsAjax",
            new { controller = "Post", action = "GetAllSideBarTags" });
            
            // done
            routeBuilder.MapRoute("Harag.Post.GetCategories", "Harag/Categories",
            new { controller = "Post", action = "GetHaragCategories" });
            
            // Cities
            routeBuilder.MapRoute("Harag.Post.GetHaragCities", "Harag/Cities",
            new { controller = "Post", action = "GetHaragCities" });
            
            // HaragNeighborhood
            routeBuilder.MapRoute("Harag.Post.GetHaragNeighborhoods", "Harag/HaragNeighborhood/{cityId?}",
            new { controller = "Post", action = "GetHaragNeighborhoods" }); 
            
            // NavbarAjax
            routeBuilder.MapRoute("Harag.Navbar", "Harag/NavbarAjax",
            new { controller = "Post", action = "GetHaragNavbar" });
             
            // Harag Post Report
            routeBuilder.MapRoute("Harag.Post.ReportPostAjax", "Harag/ReportPost",
            new { controller = "Post", action = "ReportPostAjax" });

            routeBuilder.MapRoute("Harag.Post.ReportPost", "Harag/ReportPost/{postId}",
            new { controller = "Post", action = "ReportPost" });
            // Comment 

            //  Harag all Comment  
            routeBuilder.MapRoute("Harag.Comment.GetAllPostCommentsAjax", "Harag/AllCommentsAjax",
            new { controller = "Comment", action = "GetAllPostComments" });

            //  Harag Comment  
            routeBuilder.MapRoute("Harag.Comment.AddHaragComment", "Harag/AddComment",
            new { controller = "Comment", action = "AddHaragComment" });

            // Harag Comment Report
            routeBuilder.MapRoute("Harag.Comment.ReportCommentAjax", "Harag/Comment/ReportCommentAjax/{postId?}/{commentId?}/{type?}",
           new { controller = "Comment", action = "ReportCommentAjax" });

            //////////
            ////
            ///
                      // chceck black list
            routeBuilder.MapRoute("Harag.Post.AgreementBeforeAddPost", "Harag/Agreement",
            new { controller = "Post", action = "AgreementBeforeAddPost" });


            ///
            /// Harag Messages
            ///

            //  Harag all Messages  
            routeBuilder.MapRoute("Harag.Message.GetUserMessageThreads", "Harag/Messages",
            new { controller = "Message", action = "GetUserMessageThreads" });

            //  Harag Get Post Messages  
            routeBuilder.MapRoute("Harag.Message.GetAllPostMessages", "Harag/Messages/Post/{postId?}",
            new { controller = "Message", action = "GetAllPostMessages" });

            // Harag New Message
            routeBuilder.MapRoute("Harag.Message.AddPostMessage", "Harag/NewPostMessage/{postId?}/{Message?}",
           new { controller = "Message", action = "AddPostMessage" });

 
            ///
            /// HARAG USER
            /// 

            // check black list 
            routeBuilder.MapRoute("Harag.User.CheckBlackList", "Harag/CheckBlackList",
            new { controller = "User", action = "CheckBlackList" });

            // chceck black list
            routeBuilder.MapRoute("Harag.User.CheckBlackListAjax", "Harag/CheckBlackListAjax",
            new { controller = "User", action = "CheckBlackListAjax" });
             
            // profile
            routeBuilder.MapRoute("Harag.User.Profile", "Harag/profile",
            new { controller = "User", action = "Profile" });

            // user profile
            routeBuilder.MapRoute("Harag.User.UserProfile", "Harag/user/{username?}",
            new { controller = "User", action = "UserProfile" });

            // user posts
            routeBuilder.MapRoute("Harag.User.GetUserPostsByUserId", "Harag/user/posts/{userId?}",
            new { controller = "User", action = "GetUserPostsByUserId" });

            // user rate
            routeBuilder.MapRoute("Harag.User.RateUserView", "Harag/Rate/{username}",
            new { controller = "User", action = "RateUserView" });

            // user rate ajax
            routeBuilder.MapRoute("Harag.User.RateUserAjax", "Harag/RateAjax",
            new { controller = "User", action = "RateUserAjax" });


            // all user rate
            routeBuilder.MapRoute("Harag.User.GetUserRates", "Harag/User/{userId}/Rates",
            new { controller = "User", action = "GetUserRates" });
             
            // user favorite posts
            routeBuilder.MapRoute("Harag.User.GetHaragFavoritesPosts", "Harag/Favorites",
            new { controller = "Post", action = "GetHaragFavoritesPosts" });
            
            ///
            // Search
            ///

            // done search by cat
            routeBuilder.MapRoute("Harag.Post.GetHaragCatPosts", "Harag/Cat/{catId?}",
            new { controller = "Post", action = "GetHaragCatPosts" });
  
 
            // done search by user
            routeBuilder.MapRoute("Harag.Post.GetHaragUserPosts", "Harag/{userId}/Posts",
            new { controller = "Post", action = "GetHaragUserPosts" });

            // done search by cat and city and neighborhood
            routeBuilder.MapRoute("Harag.Post.GetHaragSearchPostsPosts", "Harag/City/{city?}",
            new { controller = "Post", action = "GetHaragCityPosts" });

            // done search by cat and city and neighborhood
            routeBuilder.MapRoute("Harag.Post.HaragSearch", "Harag/Search/{Term?}",
            new { controller = "Post", action = "HaragSearch" });


            // done search by cat and city and neighborhood
            routeBuilder.MapRoute("Harag.Post.HaragSearchCatCity", "Harag/HaragSearchCatCity/{Cat?}/{City?}",
            new { controller = "Post", action = "HaragSearchCatCity" });


            ///
            /// Harag Payment
            ///

            // pqyment banks
            routeBuilder.MapRoute("Harag.Payment.PaymentBanks", "Harag/Payment/Banks",
            new { controller = "Payment", action = "PaymentBanks" });


            // payment sadad
            routeBuilder.MapRoute("Harag.Payment.PaymentSadad", "Harag/Payment/Sadad",
            new { controller = "Payment", action = "PaymentSadad" });
             

            // ------------------------ Consultant ------------------------- 

            routeBuilder.MapRoute("Consultant.ConsultantHome", "Consultations",
            new { controller = "ConsultantHome", action = "Home" });

            
            routeBuilder.MapRoute("Consultant.Navbar", "Consultations/Consultant/Navbar",
            new { controller = "ConsultantHome", action = "Navbar" });

            routeBuilder.MapRoute("Consultant.User.GetUserInfo", "Consultations/User/GetUserInfo",
            new { controller = "User", action = "GetUserInfo" });

            routeBuilder.MapRoute("Consultant.User.GetAdminLink", "Consultations/User/GetAdminLink",
            new { controller = "User", action = "GetAdminLink" });

            routeBuilder.MapRoute("Consultant.User.GetUserRoles", "Consultations/User/GetUserRoles",
            new { controller = "User", action = "GetUserRoles" });
            

            routeBuilder.MapRoute("Consultant.Post.GetAllClosedPostsAjax", "Consultations/GetAllClosedPostsAjax",
            new { controller = "Post", action = "GetAllClosedPostsAjax" });

            ///
            /*
             * Ali Edits on Consultant
             * Edit no. 12
             */
            ///
            routeBuilder.MapRoute("Consultant.Post.GetCustomerCommonPosts", "Consultations/CommonConsultations",
            new { controller = "Post", action = "GetCustomerCommonPosts" });
            //routeBuilder.MapRoute("Consultant.Post.GetAllClosedPostsAjax", "Consultations/GetAllClosedPostsAjax",
            //new { controller = "Post", action = "GetAllClosedPostsAjax" }); 

            routeBuilder.MapRoute("Consultant.Post.GetAllClosedPosts", "Consultations/LastConsultations",
            new { controller = "Post", action = "GetAllClosedPosts" });

            routeBuilder.MapRoute("Consultant.GetAllClosedPostsByCategoryId", "Consultations/CategoryConsultations",
            new { controller = "Post", action = "GetAllClosedPostsByCategoryId" });
            routeBuilder.MapRoute("Consultant.GetAllClosedPostsBySubCategoryId", "Consultations/SubCategoryConsultations",
            new { controller = "Post", action = "GetAllClosedPostsBySubCategoryId" });
            routeBuilder.MapRoute("Consultant.Post.GetCustomerOpenPosts", "Consultations/UserOpenConsultations",
            new { controller = "Post", action = "GetCustomerOpenPosts" });
            routeBuilder.MapRoute("Consultant.Post.GetCustomerClosedPosts", "Consultations/UserClosedConsultations",
            new { controller = "Post", action = "GetCustomerClosedPosts" });
            routeBuilder.MapRoute("Consultant.Post.GetConsultantNotAnsweredPosts", "Consultations/ConsultantNotAnsweredConsultations",
            new { controller = "Post", action = "GetConsultantNotAnsweredPosts" });
            routeBuilder.MapRoute("Consultant.Post.GetConsultantReservedPosts", "Consultations/ConsultantReservedConsultations",
            new { controller = "Post", action = "GetConsultantReservedPosts" });
            routeBuilder.MapRoute("Consultant.Post.GetConsultantOpenPosts", "Consultations/ConsultantOpenConsultations",
            new { controller = "Post", action = "GetConsultantOpenPosts" });
            routeBuilder.MapRoute("Consultant.Post.GetConsultantClosedPosts", "Consultations/ConsultantClosedConsultations",
            new { controller = "Post", action = "GetConsultantClosedPosts" });

            routeBuilder.MapRoute("Consultant.Post.GetPost", "Consultations/Post",
            new { controller = "Post", action = "GetPost" });
            routeBuilder.MapRoute("Consultant.Post.getFile", "Consultations/Images/{path}",
            new { controller = "Post", action = "getFile" });
            routeBuilder.MapRoute("Consultant.Post.getFilePrivate", "Consultations/Imgs/{path}",
            new { controller = "Post", action = "getFilePrivate" });

            routeBuilder.MapRoute("Consultant.Comment.GetCommentsByPostId", "Consultations/PostComments",
            new { controller = "Comment", action = "GetCommentsByPostId" });


            /////////////////
            ///
            routeBuilder.MapRoute("Consultant.Comment.GetCities", "Consultations/AddressCities",
new { controller = "Comment", action = "GetCities" });
            routeBuilder.MapRoute("Consultant.Comment.GetNeighbohoods", "Consultations/AddressNeighbohoods",
new { controller = "Comment", action = "GetNeighbohoods" });

            /////////////////
            ///
            routeBuilder.MapRoute("Consultant.Post.HasOpenedConsultationAnCanAdd", "Consultations/Post/CanAddNewPost",
new { controller = "Post", action = "HasOpenedConsultationAnCanAdd" });
            routeBuilder.MapRoute("Consultant.Post.ClosePost", "Consultations/Post/ClosePost",
            new { controller = "Post", action = "ClosePost" });
            routeBuilder.MapRoute("Consultant.Post.RatePost", "Consultations/Post/RatePost",
            new { controller = "Post", action = "RatePost" });
            routeBuilder.MapRoute("Consultant.Post.ReservePost", "Consultations/Post/ReservePost",
            new { controller = "Post", action = "ReservePost" });
            routeBuilder.MapRoute("Consultant.Post.UnReservePost", "Consultations/Post/UnReservePost",
            new { controller = "Post", action = "UnReservePost" });
            routeBuilder.MapRoute("Consultant.Post.ChangeCatAndSub", "Consultations/Post/ChangeCatAndSub",
            new { controller = "Post", action = "ChangeCatAndSub" });
            routeBuilder.MapRoute("Consultant.Post.GetCategory", "Consultations/Post/GetCategory",
           new { controller = "Post", action = "GetCategory" });
            routeBuilder.MapRoute("Consultant.Post.GetSubCategoriesByCategoryId", "Consultations/Post/GetSubCategoriesByCategoryId",
            new { controller = "Post", action = "GetSubCategoriesByCategoryId" });
            routeBuilder.MapRoute("Consultant.Post.SetPostToCategoryAndSubCategory", "Consultations/Post/SetPostToCategoryAndSubCategory",
            new { controller = "Post", action = "SetPostToCategoryAndSubCategory" });
            routeBuilder.MapRoute("Consultant.Post.AddPost", "Consultations/Post/AddPost",
            new { controller = "Post", action = "AddPost" });
            routeBuilder.MapRoute("Consultant.Post.GetCategories", "Consultations/Post/GetCategories",
            new { controller = "Post", action = "GetCategories" });
            routeBuilder.MapRoute("Consultant.Post.AddPostAjax", "Consultations/Post/AddPostAjax",
            new { controller = "Post", action = "AddPostAjax" });
            routeBuilder.MapRoute("Consultant.Post.Search", "Consultations/Post/Search",
            new { controller = "Post", action = "Search" });


            routeBuilder.MapRoute("Consultant.Comment.AddComment", "Consultations/Comment/AddComment",
            new { controller = "Comment", action = "AddComment" });
            routeBuilder.MapRoute("Consultant.Comment.GetComment", "Consultations/Comment/GetComment",
            new { controller = "Comment", action = "GetComment" });


            // get cities 
            routeBuilder.MapRoute("Consultant.Post.GetAllCitiesAjax", "Consultations/GetAllStateCitiesAjax",
           new { controller = "Post", action = "GetAllCitiesAjax" });

            // get Neighborhood
            routeBuilder.MapRoute("Consultant.Post.GetAllCityNeighborhoodAjax", "Consultations/GetAllStateCityNeighborhoodsAjax",
           new { controller = "Post", action = "GetAllCityNeighborhoodAjax" });


            //-----------------enf of Consultant----------------------------

             
            //-----------------Admin Consultant----------------------------
            routeBuilder.MapRoute("Consultant.Admin.GetAllCustomers", "Consultations/Admin/Posts/GetAllCustomers",
               new { controller = "Consultations", action = "GetAllCustomers" });



            




            







            //Dashboard 
            routeBuilder.MapRoute("Consultant.Admin.Dashboard", "Consultations/Admin",
            new { controller = "DashboardConsultant", action = "Home" });
            //Closed Displayed Posts
            routeBuilder.MapRoute("Consultant.Admin.GetAllClosedDisplayedPosts", "Consultations/Admin/Posts/ClosedDisplayed",
                new { controller = "Consultations", action = "GetClosedDisplayedPosts" });
            //Closed Displayed Post By Id
            routeBuilder.MapRoute("Consultant.Admin.GetPostById", "Consultations/Admin/Posts/GetPostById",
                new { controller = "Consultations", action = "GetPostById" });
            //Closed Displayed Posts In Json 
            routeBuilder.MapRoute("Consultant.Admin.GetAllClosedDisplayedPostsInJson", "Consultations/Admin/Posts/ClosedDisplayedInJson",
                new { controller = "Consultations", action = "GetAllClosedDisplayedPostsInJson" });
            //Closed Displayed Posts With Category Id In Json 
            routeBuilder.MapRoute("Consultant.Admin.GetAllClosedDisplayedPostsWithCategoryIdInJson", "Consultations/Admin/Posts/ClosedDisplayedWithCategoryIdInJson",
                 new { controller = "Consultations", action = "GetAllClosedDisplayedPostsWithCategoryIdInJson" });
            //Closed Displayed Posts With SubCategory Id In Json
            routeBuilder.MapRoute("Consultant.Admin.GetAllClosedDisplayedPostsWithSubCategoryIdInJson", "Consultations/Admin/Posts/ClosedDisplayedWithSubCategoryIdInJson",
                 new { controller = "Consultations", action = "GetAllClosedDisplayedPostsWithSubCategoryIdInJson" });
            //Closed Displayed Posts By Date In Json
            routeBuilder.MapRoute("Consultant.Admin.GetPostsByDate", "Consultations/Admin/Posts/GetPostsByDate",
                 new { controller = "Consultations", action = "GetPostsByDate" });
            //Remove  Post
            routeBuilder.MapRoute("Consultant.Admin.RemovePost", "Consultations/Admin/Posts/RemovePost",
                 new { controller = "Consultations", action = "RemovePost" });
            //Get  Post Details
            routeBuilder.MapRoute("Consultant.Admin.GetPostDetails", "Consultations/Admin/Posts/GetPostDetails",
                new { controller = "Consultations", action = "GetPostDetails" });
            //Get  Post Comments
            routeBuilder.MapRoute("Consultant.Admin.GetPostComments", "Consultations/Admin/Posts/GetPostComments",
                new { controller = "Consultations", action = "GetPostComments" });
            //Set  Post To Common
            routeBuilder.MapRoute("Consultant.Admin.SetPostToCommon", "Consultations/Admin/Posts/SetPostToCommon",
                new { controller = "Consultations", action = "SetPostToCommon" });




            //Closed Not Displayed Posts
            routeBuilder.MapRoute("Consultant.Admin.GetAllClosedNotDisplayedPosts", "Consultations/Admin/Posts/ClosedNotDisplayed",
                new { controller = "Consultations", action = "GetClosedNotDisplayedPosts" });
            //Closed Not Displayed Posts in Json
            routeBuilder.MapRoute("Consultant.Admin.GetAllClosedNotDisplayedPostsInJson", "Consultations/Admin/Posts/ClosedNotDisplayedInJson",
                new { controller = "Consultations", action = "GetAllClosedNotDisplayedPostsInJson" });
            //Closed Not Displayed Posts With Category Id in Json
            routeBuilder.MapRoute("Consultant.Admin.GetAllClosedNotDisplayedPostsWithCategoryIdInJson", "Consultations/Admin/Posts/ClosedNotDisplayedWithCategoryIdInJson",
                new { controller = "Consultations", action = "GetAllClosedNotDisplayedPostsWithCategoryIdInJson" });
            //Closed Not Displayed Posts With SubCategory Id in Json
            routeBuilder.MapRoute("Consultant.Admin.GetAllClosedNotDisplayedPostsWithSubCategoryIdInJson", "Consultations/Admin/Posts/ClosedNotDisplayedWithSubCategoryIdInJson",
                new { controller = "Consultations", action = "GetAllClosedNotDisplayedPostsWithSubCategoryIdInJson" });
            //Post Display
            routeBuilder.MapRoute("Consultant.Admin.PostDisplay", "Consultations/Admin/Posts/PostDisplay",
                new { controller = "Consultations", action = "PostDisplay" });



            // Not Replied Posts
            routeBuilder.MapRoute("Consultant.Admin.GetAllNotRepliedPosts", "Consultations/Admin/Posts/GetAllNotRepliedPosts",
                new { controller = "Consultations", action = "GetNotRepliedPosts" });
            // Not Replied Posts In Json
            routeBuilder.MapRoute("Consultant.Admin.GetAllNotRepliedPostsInJson", "Consultations/Admin/Posts/GetAllNotRepliedPostsInJson",
                new { controller = "Consultations", action = "GetAllNotRepliedPostsInJson" });
            // Not Replied Posts With Category In Json
            routeBuilder.MapRoute("Consultant.Admin.GetAllNotRepliedPostsWithCategoryInJson", "Consultations/Admin/Posts/GetAllNotRepliedPostsWithCategoryInJson",
                new { controller = "Consultations", action = "GetAllNotRepliedPostsWithCategoryInJson" });
            // Not Replied Posts With SubCategory In Json
            routeBuilder.MapRoute("Consultant.Admin.GetAllNotRepliedPostsWithSubCategoryInJson", "Consultations/Admin/Posts/GetAllNotRepliedPostsWithSubCategoryInJson",
                new { controller = "Consultations", action = "GetAllNotRepliedPostsWithSubCategoryInJson" });




            //  Reserved Posts
            routeBuilder.MapRoute("Consultant.Admin.GetAllReservedPosts", "Consultations/Admin/Posts/GetAllReservedPosts",
                new { controller = "Consultations", action = "GetReservedPosts" });
            //  Reserved Posts In Json
            routeBuilder.MapRoute("Consultant.Admin.GetAllReservedPostsInJson", "Consultations/Admin/Posts/GetAllReservedPostsInJson",
                new { controller = "Consultations", action = "GetAllReservedPostsInJson" });
            //  Reserved Posts With Category Id In Json
            routeBuilder.MapRoute("Consultant.Admin.GetAllReservedPostsWithCategoryIdInJson", "Consultations/Admin/Posts/GetAllReservedPostsWithCategoryIdInJson",
                new { controller = "Consultations", action = "GetAllReservedPostsWithCategoryIdInJson" });
            //  Reserved Posts With SubCategory Id In Json
            routeBuilder.MapRoute("Consultant.Admin.GetAllReservedPostsWithSubCategoryIdInJson", "Consultations/Admin/Posts/GetAllReservedPostsWithSubCategoryIdInJson",
                new { controller = "Consultations", action = "GetAllReservedPostsWithSubCategoryIdInJson" });
            //Post Cancelling Reserving
            routeBuilder.MapRoute("Consultant.Admin.PostCancelReserving", "Consultations/Admin/Posts/PostCancelReserving",
                new { controller = "Consultations", action = "PostCancelReserving" });


            //  Open Posts
            routeBuilder.MapRoute("Consultant.Admin.GetAllOpenPosts", "Consultations/Admin/Posts/GetAllOpenPosts",
                new { controller = "Consultations", action = "GetOpenPosts" });
            //  Open Posts In Json
            routeBuilder.MapRoute("Consultant.Admin.GetAllOpenPostsInJson", "Consultations/Admin/Posts/GetAllOpenPostsInJson",
                new { controller = "Consultations", action = "GetAllOpenPostsInJson" });
            //  Open Posts With Category In Json
            routeBuilder.MapRoute("Consultant.Admin.GetAllOpenPostsWithCategoryInJson", "Consultations/Admin/Posts/GetAllOpenPostsWithCategoryInJson",
                new { controller = "Consultations", action = "GetAllOpenPostsWithCategoryInJson" });
            //  Open Posts With SubCategory In Json
            routeBuilder.MapRoute("Consultant.Admin.GetAllOpenPostsWithSubCategoryInJson", "Consultations/Admin/Posts/GetAllOpenPostsWithSubCategoryInJson",
                new { controller = "Consultations", action = "GetAllOpenPostsWithSubCategoryInJson" });







            //Categories
            routeBuilder.MapRoute("Consultant.Admin.GetCategories", "Consultations/Admin/Categories/GetCategories",
             new { controller = "Categories", action = "GetCategories" });
            //categories In Json
            routeBuilder.MapRoute("Consultant.Admin.GetCategoriesInJson", "Consultations/Admin/Categories/GetCategoriesInJson",
              new { controller = "Categories", action = "GetCategoriesInJson" });
            // Delete Category
            routeBuilder.MapRoute("Consultant.Admin.DeleteCategory", "Consultations/Admin/Categories/DeleteCategory",
             new { controller = "Categories", action = "DeleteCategory" });
            // Add Category 
            routeBuilder.MapRoute("Consultant.Admin.AddCategory", "Consultations/Admin/Categories/AddCategory",
             new { controller = "Categories", action = "AddCategory" });
            // Add Category Ajax
            routeBuilder.MapRoute("Consultant.Admin.AddCategoryAjax", "Consultations/Admin/Categories/AddCategoryAjax",
             new { controller = "Categories", action = "AddCategoryAjax" });

        






            //SubCategories
            routeBuilder.MapRoute("Consultant.Admin.GetSubCategories", "Consultations/Admin/SubCategories/GetSubCategories",
             new { controller = "SubCategories", action = "GetSubCategories" });
            //Subcategories In Json
            routeBuilder.MapRoute("Consultant.Admin.GetSubCategoriesInJson", "Consultations/Admin/SubCategories/GetSubCategoriesInJson",
              new { controller = "SubCategories", action = "GetSubCategoriesInJson" });
            //Get SubCategories By CategoryId 
            routeBuilder.MapRoute("Consultant.Admin.GetSubCategoriesByCategoryId", "Consultations/Admin/SubCategories/GetSubCategoriesByCategoryId",
              new { controller = "SubCategories", action = "GetSubCategoriesByCategoryId" });
            //Get SubCategories By CategoryId In Json
            routeBuilder.MapRoute("Consultant.Admin.GetSubCategoriesByCategoryIdInJson", "Consultations/Admin/SubCategories/GetSubCategoriesByCategoryIdInJson",
              new { controller = "SubCategories", action = "GetSubCategoriesByCategoryIdInJson" });
            //Add SubCategory
            routeBuilder.MapRoute("Consultant.Admin.AddSubCategory", "Consultations/Admin/SubCategories/AddSubCategory",
              new { controller = "SubCategories", action = "AddSubCategory" });
            //Add SubCategory Ajax
            routeBuilder.MapRoute("Consultant.Admin.AddSubCategoryAjax", "Consultations/Admin/SubCategories/AddSubCategoryAjax",
              new { controller = "SubCategories", action = "AddSubCategoryAjax" });
            // Delete SubCategory
            routeBuilder.MapRoute("Consultant.Admin.DeleteSubCategory", "Consultations/Admin/SubCategories/DeleteSubCategory",
             new { controller = "SubCategories", action = "DeleteSubCategory" });







            //Get Members
            routeBuilder.MapRoute("Consultant.Admin.GetCustomers", "Consultations/Admin/Customer/GetCustomers",
               new { controller = "Customer", action = "GetMembers" });

            //Get Members Ajax Consultant.User.GetAdminLink
            routeBuilder.MapRoute("Consultant.Admin.GetCustomersAjax", "Consultations/Admin/Customer/GetCustomersAjax",
               new { controller = "Customer", action = "GetMembersAjax" });
            //Get GetOnline Members 
            routeBuilder.MapRoute("Consultant.Admin.GetOnlineMembers", "Consultations/Admin/Customer/GetOnlineMembers",
               new { controller = "Customer", action = "GetOnlineMembers" });
            //Get GetOnline Members Ajax
            routeBuilder.MapRoute("Consultant.Admin.GetOnlineMembersAjax", "Consultations/Admin/Customer/GetOnlineMembersAjax",
               new { controller = "Customer", action = "GetOnlineMembersAjax" });
            //Get Member Details
            routeBuilder.MapRoute("Consultant.Admin.GetCustomerDetails", "Consultations/Admin/Customer/GetCustomerDetails",
               new { controller = "Customer", action = "GetMemberDetails" });
            //Get Member Posts 
            routeBuilder.MapRoute("Consultant.Admin.GetCustomerPosts", "Consultations/Admin/Customer/GetCustomerPosts",
               new { controller = "Customer", action = "GetCustomerPosts" });
            //Get Member Posts In Json
            routeBuilder.MapRoute("Consultant.Admin.GetCustomerPostsInJson", "Consultations/Admin/Customer/GetCustomerPostsInJson",
              new { controller = "Customer", action = "GetCustomerPostsInJson" });
            //Get Member Post By Post Id and Customer Id
            routeBuilder.MapRoute("Consultant.Admin.GetCustomerPostById", "Consultations/Admin/Customer/GetCustomerPostById",
              new { controller = "Customer", action = "GetCustomerPostById" });
            //Get Top 20 Member By Posts Number
            routeBuilder.MapRoute("Consultant.Admin.GetTop20MemberByPostsNumber", "Consultations/Admin/Customer/GetTop20MemberByPostsNumber",
              new { controller = "Customer", action = "GetTop20MemberByPostsNumber" });
            //Get Top 20 Member By Posts Number In Json
            routeBuilder.MapRoute("Consultant.Admin.GetTop20MemberByPostsNumberInJson", "Consultations/Admin/Customer/GetTop20MemberByPostsNumberInJson",
              new { controller = "Customer", action = "GetTop20MemberByPostsNumberInJson" });
            //Delete Customer
            routeBuilder.MapRoute("Consultant.Admin.DeleteMember", "Consultations/Admin/Customer/DeleteMember",
              new { controller = "Customer", action = "DeleteMember" });







            //Get Consultants
            routeBuilder.MapRoute("Consultant.Admin.GetConsultants", "Consultations/Admin/Customer/GetConsultants",
               new { controller = "Customer", action = "GetConsultants" });
            //Get Consultants Ajax
            routeBuilder.MapRoute("Consultant.Admin.GetConsultantsAjax", "Consultations/Admin/Customer/GetConsultantsAjax",
               new { controller = "Customer", action = "GetConsultantsAjax" });
            //Get GetOnline Consultants 
            routeBuilder.MapRoute("Consultant.Admin.GetOnlineConsultants", "Consultations/Admin/Customer/GetOnlineConsultants",
               new { controller = "Customer", action = "GetOnlineConsultants" });
            //Get GetOnline Consultants Ajax
            routeBuilder.MapRoute("Consultant.Admin.GetOnlineConsultantAjax", "Consultations/Admin/Customer/GetOnlineConsultantAjax",
               new { controller = "Customer", action = "GetOnlineConsultantAjax" });
            //Get Consultant Details
            routeBuilder.MapRoute("Consultant.Admin.GetConsultantDetails", "Consultations/Admin/Customer/GetConsultantDetails",
               new { controller = "Customer", action = "GetConsultantDetails" });

            //Get Consultant Posts 
            routeBuilder.MapRoute("Consultant.Admin.GetConsultantPosts", "Consultations/Admin/Customer/GetConsultantPosts",
               new { controller = "Customer", action = "GetConsultantPosts" });
            //Get Consultant Posts In Json
            routeBuilder.MapRoute("Consultant.Admin.GetConsultantPostsInJson", "Consultations/Admin/Customer/GetConsultantPostsInJson",
              new { controller = "Customer", action = "GetConsultantPostsInJson" });
            //Get Consultant Post By Post Id and Consultant Id
            routeBuilder.MapRoute("Consultant.Admin.GetConsultantPostById", "Consultations/Admin/Customer/GetConsultantPostById",
              new { controller = "Customer", action = "GetConsultantPostById" });
            //Get Top 20 Consultant By Posts Number
            routeBuilder.MapRoute("Consultant.Admin.GetTop20ConsultantByPostsNumber", "Consultations/Admin/Customer/GetTop20ConsultantByPostsNumber",
              new { controller = "Customer", action = "GetTop20ConsultantByPostsNumber" });
            //Get Top 20 Consultant By Posts Number In Json
            routeBuilder.MapRoute("Consultant.Admin.GetTop20ConsultantByPostsNumberInJson", "Consultations/Admin/Customer/GetTop20ConsultantByPostsNumberInJson",
              new { controller = "Customer", action = "GetTop20ConsultantByPostsNumberInJson" });
            //Get Top 20 Consultant By Posts Evaluation
            routeBuilder.MapRoute("Consultant.Admin.GetTop20ConsultantByPostsEvaluation", "Consultations/Admin/Customer/GetTop20ConsultantByPostsEvaluation",
              new { controller = "Customer", action = "GetTop20ConsultantByPostsEvaluation" });
            //Get Top 20 Consultant By Posts Evaluation In Json
            routeBuilder.MapRoute("Consultant.Admin.GetTop20ConsultantByPostsEvaluationInJson", "Consultations/Admin/Customer/GetTop20ConsultantByPostsEvaluationInJson",
              new { controller = "Customer", action = "GetTop20ConsultantByPostsEvaluationInJson" });





            routeBuilder.MapRoute("Consultant.Admin.Test", "Consultations/Admin/Test",
           new { controller = "Consultations", action = "Test" });


            //-------------------End Admin Consultant-------------------------------
            //--------------------Harag Admin---------------------------------------


            //Dashboard
             routeBuilder.MapRoute("Harag.Admin", "Harag/Admin",
              new { controller = "Dashboard", action = "Home" });


            //Add Category
            routeBuilder.MapRoute("Harag.Admin.AddHaragCategory", "Harag/Admin/Category/AddHaragCategory", new { controller = "Category", action = "AddHaragCategory" });
            //Add Category Aajax
            routeBuilder.MapRoute("Harag.Admin.AddHaragCategoryAjax", "Harag/Admin/Category/AddHaragCategoryAjax", new { controller = "Category", action = "AddHaragCategoryAjax" });
            //Get Categories 
            routeBuilder.MapRoute("Harag.Admin.HaragCategories", "Harag/Admin/Category/HaragCategories", new { controller = "Category", action = "GetHaragCategories" });
            //Get Categories Ajax
            routeBuilder.MapRoute("Harag.Admin.HaragCategoriesAjax", "Harag/Admin/Category/HaragCategoriesAjax", new { controller = "Category", action = "GetHaragCategoriesAjax" });




            //Get Customers
            routeBuilder.MapRoute("Harag.Admin.HaragCustomers", "Harag/Admin/Customer/HaragCustomers", new { controller = "Customer", action = "HaragGetMembers" });
            //Get Customers Ajax
            routeBuilder.MapRoute("Harag.Admin.HaragCustomersAjax", "Harag/Admin/Customer/HaragCustomerAjax", new { controller = "Customer", action = "GetHaragCustomerAjax" });


            //Add Customer To BlackList
            routeBuilder.MapRoute("Harag.Admin.AddCustomerToBlackList", "Harag/Admin/Customer/AddToBlacList", new { controller = "BlackList", action = "AddBlackListCustomer" });
            //Get BlackList Customers
            routeBuilder.MapRoute("Harag.Admin.BlackListCustomer", "Harag/Admin/Customer/BlackList", new { controller = "BlackList", action = "GetBlackListCustomers" });
            //Get BlackList Customers Ajax
            routeBuilder.MapRoute("Harag.Admin.BlackListCustomersAjax", "Harag/Admin/Customer/BlackListAjax", new { controller = "BlackList", action = "GetBlackListCustomersAjax" });
            //Delet BlackList Customers 
            routeBuilder.MapRoute("Harag.Admin.BlackListCustomersDelete", "Harag/Admin/Customer/BlackListDelet", new { controller = "BlackList", action = "DeleteBlackListCustomer" });






            //Get Posts  
            routeBuilder.MapRoute("Harag.Admin.HaragPosts", "Harag/Admin/Post/HaragPosts", new { controller = "Post", action = "GetHaragPosts" });
            //Get Posts Ajax 
            routeBuilder.MapRoute("Harag.Admin.HaragPostAjax", "Harag/Admin/Post/HaragPostsAjax", new { controller = "Post", action = "GetHaragPostsAjax" });
            //Get Post Details
            routeBuilder.MapRoute("Harag.Admin.HaragPostDetails", "Harag/Admin/Post/HaragPostsDetails", new { controller = "Post", action = "GetPostDetails" });
            //Post Comments
            
            routeBuilder.MapRoute("Harag.Admin.HaragPostComment", "Harag/Admin/Post/HaragPostsComment", new { controller = "Post", action = "GetPostComments" });

            //post Messages
            routeBuilder.MapRoute("Harag.Admin.HaragPostMessage", "Harag/Admin/Post/HaragPostMessages", new { controller = "Post", action = "GetPostMessages" });
            //post Reports
            routeBuilder.MapRoute("Harag.Admin.HaragPostReport", "Harag/Admin/Post/HaragPostReport", new { controller = "Post", action = "GetPostReports" });
            //Comment Reports 
            routeBuilder.MapRoute("Harag.Admin.HaragCommentReport", "Harag/Admin/Comment/HaragPostReport", new { controller = "Post", action = "GetCommentReports" });
            //Comment Reports Ajax
            routeBuilder.MapRoute("Harag.Admin.HaragCommentReportAjax", "Harag/Admin/Comment/HaragPostReportAjax", new { controller = "Post", action = "GetCommentReportsAjax" });
            //Get Cities 
            routeBuilder.MapRoute("Harag.Admin.HaragCities", "Harag/Admin/Comment/HaragCities", new { controller = "City", action = "GetCities" });
            //Get Cities Ajax
            routeBuilder.MapRoute("Harag.Admin.HaragCitiesAjax", "Harag/Admin/Comment/HaragCitiesAjax", new { controller = "City", action = "GetCitiesAjax" });
            //Get Rates 
            routeBuilder.MapRoute("Harag.Admin.HaragRates", "Harag/Admin/Comment/HaragRates", new { controller = "Rate", action = "GetRates" });
            //Get Rates Ajax
            routeBuilder.MapRoute("Harag.Admin.HaragRatesAjax", "Harag/Admin/Comment/HaragRatesAjax", new { controller = "Rate", action = "GetRatesAjax" });
            //Get Rate Details
            routeBuilder.MapRoute("Harag.Admin.HaragRateDetails", "Harag/Admin/Comment/HaragRateDetails", new { controller = "Rate", action = "GetRateDetails" });
            //Delete Rate
            routeBuilder.MapRoute("Harag.Admin.HaragRateDelete", "Harag/Admin/Comment/HaragRateDelete", new { controller = "Rate", action = "DeleteRate" });

            ////Post Messages

            //routeBuilder.MapRoute("Harag.Admin.HaragPostMessage", "Harag/Admin/Post/HaragPostsMessage", new { controller = "Post", action = "GetPostMessages" });
            ////Post Messages Ajax

            //routeBuilder.MapRoute("Harag.Admin.HaragPostMessageAjax", "Harag/Admin/Post/HaragPostsMessageAjax", new { controller = "Post", action = "GetPostMessagesAjax" });





            //-------------------End HArag Admin------------------------------------





            //reorder routes so the most used ones are on top. It can improve performance

            //areas
            routeBuilder.MapRoute(name: "areaRoute", template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            //home page
            routeBuilder.MapLocalizedRoute("HomePage", "",
                new { controller = "Home", action = "Index" });

            //login
            routeBuilder.MapLocalizedRoute("Login", "login/",
				new { controller = "Customer", action = "Login" });

            //register
            routeBuilder.MapLocalizedRoute("Register", "register/",
				new { controller = "Customer", action = "Register" });

            //logout
            routeBuilder.MapLocalizedRoute("Logout", "logout/",
				new { controller = "Customer", action = "Logout" });

            //shopping cart
            routeBuilder.MapLocalizedRoute("ShoppingCart", "cart/",
				new { controller = "ShoppingCart", action = "Cart" });

            //estimate shipping
            routeBuilder.MapLocalizedRoute("EstimateShipping", "cart/estimateshipping",
				new {controller = "ShoppingCart", action = "GetEstimateShipping"});

            //wishlist
            routeBuilder.MapLocalizedRoute("Wishlist", "wishlist/{customerGuid?}",
				new { controller = "ShoppingCart", action = "Wishlist"});

            //customer account links
            routeBuilder.MapLocalizedRoute("CustomerInfo", "customer/info",
				new { controller = "Customer", action = "Info" });

            routeBuilder.MapLocalizedRoute("CustomerAddresses", "customer/addresses",
				new { controller = "Customer", action = "Addresses" });

            routeBuilder.MapLocalizedRoute("CustomerOrders", "order/history",
				new { controller = "Order", action = "CustomerOrders" });

            //contact us
            routeBuilder.MapLocalizedRoute("ContactUs", "contactus",
				new { controller = "Common", action = "ContactUs" });

            //sitemap
            routeBuilder.MapLocalizedRoute("Sitemap", "sitemap",
				new { controller = "Common", action = "Sitemap" });

            //product search
            routeBuilder.MapLocalizedRoute("ProductSearch", "search/",
				new { controller = "Catalog", action = "Search" });

            routeBuilder.MapLocalizedRoute("ProductSearchAutoComplete", "catalog/searchtermautocomplete",
				new { controller = "Catalog", action = "SearchTermAutoComplete" });

            //change currency (AJAX link)
            routeBuilder.MapLocalizedRoute("ChangeCurrency", "changecurrency/{customercurrency:min(0)}",
				new { controller = "Common", action = "SetCurrency" });

            //change language (AJAX link)
            routeBuilder.MapLocalizedRoute("ChangeLanguage", "changelanguage/{langid:min(0)}",
				new { controller = "Common", action = "SetLanguage" });

            //change tax (AJAX link)
            routeBuilder.MapLocalizedRoute("ChangeTaxType", "changetaxtype/{customertaxtype:min(0)}",
				new { controller = "Common", action = "SetTaxType" });

            //recently viewed products
            routeBuilder.MapLocalizedRoute("RecentlyViewedProducts", "recentlyviewedproducts/",
				new { controller = "Product", action = "RecentlyViewedProducts" });

            //new products
            routeBuilder.MapLocalizedRoute("NewProducts", "newproducts/",
				new { controller = "Product", action = "NewProducts" });

            //blog
            routeBuilder.MapLocalizedRoute("Blog", "blog",
				new { controller = "Blog", action = "List" });

            //news
            routeBuilder.MapLocalizedRoute("NewsArchive", "news",
				new { controller = "News", action = "List" });

            //forum
            routeBuilder.MapLocalizedRoute("Boards", "boards",
				new { controller = "Boards", action = "Index" });

            //compare products
            routeBuilder.MapLocalizedRoute("CompareProducts", "compareproducts/",
				new { controller = "Product", action = "CompareProducts" });

            //product tags
            routeBuilder.MapLocalizedRoute("ProductTagsAll", "producttag/all/",
				new { controller = "Catalog", action = "ProductTagsAll" });

            //manufacturers
            routeBuilder.MapLocalizedRoute("ManufacturerList", "manufacturer/all/",
				new { controller = "Catalog", action = "ManufacturerAll" });

            //vendors
            routeBuilder.MapLocalizedRoute("VendorList", "vendor/all/",
				new { controller = "Catalog", action = "VendorAll" });

            //add product to cart (without any attributes and options). used on catalog pages.
            routeBuilder.MapLocalizedRoute("AddProductToCart-Catalog", "addproducttocart/catalog/{productId:min(0)}/{shoppingCartTypeId:min(0)}/{quantity:min(0)}",
				new { controller = "ShoppingCart", action = "AddProductToCart_Catalog" });

            //add product to cart (with attributes and options). used on the product details pages.
            routeBuilder.MapLocalizedRoute("AddProductToCart-Details", "addproducttocart/details/{productId:min(0)}/{shoppingCartTypeId:min(0)}",
				new { controller = "ShoppingCart", action = "AddProductToCart_Details" });

            //product tags
            routeBuilder.MapLocalizedRoute("ProductsByTag", "producttag/{productTagId:min(0)}/{SeName?}",
				new { controller = "Catalog", action = "ProductsByTag" });

            //comparing products
            routeBuilder.MapLocalizedRoute("AddProductToCompare", "compareproducts/add/{productId:min(0)}",
				new { controller = "Product", action = "AddProductToCompareList" });

            //product email a friend
            routeBuilder.MapLocalizedRoute("ProductEmailAFriend", "productemailafriend/{productId:min(0)}",
				new { controller = "Product", action = "ProductEmailAFriend" });

            //reviews
            routeBuilder.MapLocalizedRoute("ProductReviews", "productreviews/{productId}",
				new { controller = "Product", action = "ProductReviews" });

            routeBuilder.MapLocalizedRoute("CustomerProductReviews", "customer/productreviews",
				new { controller = "Product", action = "CustomerProductReviews" });

            routeBuilder.MapLocalizedRoute("CustomerProductReviewsPaged", "customer/productreviews/page/{pageNumber:min(0)}",
				new { controller = "Product", action = "CustomerProductReviews" });

            //back in stock notifications
            routeBuilder.MapLocalizedRoute("BackInStockSubscribePopup", "backinstocksubscribe/{productId:min(0)}",
				new { controller = "BackInStockSubscription", action = "SubscribePopup" });

            routeBuilder.MapLocalizedRoute("BackInStockSubscribeSend", "backinstocksubscribesend/{productId:min(0)}",
				new { controller = "BackInStockSubscription", action = "SubscribePopupPOST" });

            //downloads
            routeBuilder.MapRoute("GetSampleDownload", "download/sample/{productid:min(0)}",
				new { controller = "Download", action = "Sample" });

            //checkout pages
            routeBuilder.MapLocalizedRoute("Checkout", "checkout/",
				new { controller = "Checkout", action = "Index" });

            routeBuilder.MapLocalizedRoute("CheckoutOnePage", "onepagecheckout/",
				new { controller = "Checkout", action = "OnePageCheckout" });

            routeBuilder.MapLocalizedRoute("CheckoutShippingAddress", "checkout/shippingaddress",
				new { controller = "Checkout", action = "ShippingAddress" });

            routeBuilder.MapLocalizedRoute("CheckoutSelectShippingAddress", "checkout/selectshippingaddress",
				new { controller = "Checkout", action = "SelectShippingAddress" });

            routeBuilder.MapLocalizedRoute("CheckoutBillingAddress", "checkout/billingaddress",
				new { controller = "Checkout", action = "BillingAddress" });

            routeBuilder.MapLocalizedRoute("CheckoutSelectBillingAddress", "checkout/selectbillingaddress",
				new { controller = "Checkout", action = "SelectBillingAddress" });

            routeBuilder.MapLocalizedRoute("CheckoutShippingMethod", "checkout/shippingmethod",
				new { controller = "Checkout", action = "ShippingMethod" });

            routeBuilder.MapLocalizedRoute("CheckoutPaymentMethod", "checkout/paymentmethod",
				new { controller = "Checkout", action = "PaymentMethod" });

            routeBuilder.MapLocalizedRoute("CheckoutPaymentInfo", "checkout/paymentinfo",
				new { controller = "Checkout", action = "PaymentInfo" });

            routeBuilder.MapLocalizedRoute("CheckoutConfirm", "checkout/confirm",
				new { controller = "Checkout", action = "Confirm" });

            routeBuilder.MapLocalizedRoute("CheckoutCompleted", "checkout/completed/{orderId:regex(\\d*)}",
				new { controller = "Checkout", action = "Completed" });

            //subscribe newsletters
            routeBuilder.MapLocalizedRoute("SubscribeNewsletter", "subscribenewsletter",
				new { controller = "Newsletter", action = "SubscribeNewsletter" });

            //email wishlist
            routeBuilder.MapLocalizedRoute("EmailWishlist", "emailwishlist",
				new { controller = "ShoppingCart", action = "EmailWishlist" });

            //login page for checkout as guest
            routeBuilder.MapLocalizedRoute("LoginCheckoutAsGuest", "login/checkoutasguest",
				new { controller = "Customer", action = "Login", checkoutAsGuest = true });

            //register result page
            routeBuilder.MapLocalizedRoute("RegisterResult", "registerresult/{resultId:min(0)}",
				new { controller = "Customer", action = "RegisterResult" });

            //check username availability
            routeBuilder.MapLocalizedRoute("CheckUsernameAvailability", "customer/checkusernameavailability",
				new { controller = "Customer", action = "CheckUsernameAvailability" });

            //passwordrecovery
            routeBuilder.MapLocalizedRoute("PasswordRecovery", "passwordrecovery",
				new { controller = "Customer", action = "PasswordRecovery" });

            //password recovery confirmation
            routeBuilder.MapLocalizedRoute("PasswordRecoveryConfirm", "passwordrecovery/confirm",
				new { controller = "Customer", action = "PasswordRecoveryConfirm" });

            //topics
            routeBuilder.MapLocalizedRoute("TopicPopup", "t-popup/{SystemName}",
				new { controller = "Topic", action = "TopicDetailsPopup" });
            
            //blog
            routeBuilder.MapLocalizedRoute("BlogByTag", "blog/tag/{tag}",
				new { controller = "Blog", action = "BlogByTag" });

            routeBuilder.MapLocalizedRoute("BlogByMonth", "blog/month/{month}",
				new { controller = "Blog", action = "BlogByMonth" });

            //blog RSS
            routeBuilder.MapLocalizedRoute("BlogRSS", "blog/rss/{languageId:min(0)}",
				new { controller = "Blog", action = "ListRss" });

            //news RSS
            routeBuilder.MapLocalizedRoute("NewsRSS", "news/rss/{languageId:min(0)}",
				new { controller = "News", action = "ListRss" });

            //set review helpfulness (AJAX link)
            routeBuilder.MapRoute("SetProductReviewHelpfulness", "setproductreviewhelpfulness",
				new { controller = "Product", action = "SetProductReviewHelpfulness" });

            //customer account links
            routeBuilder.MapLocalizedRoute("CustomerReturnRequests", "returnrequest/history",
				new { controller = "ReturnRequest", action = "CustomerReturnRequests" });

            routeBuilder.MapLocalizedRoute("CustomerDownloadableProducts", "customer/downloadableproducts",
				new { controller = "Customer", action = "DownloadableProducts" });

            routeBuilder.MapLocalizedRoute("CustomerBackInStockSubscriptions", "backinstocksubscriptions/manage",
				new { controller = "BackInStockSubscription", action = "CustomerSubscriptions" });

            routeBuilder.MapLocalizedRoute("CustomerBackInStockSubscriptionsPaged", "backinstocksubscriptions/manage/{pageNumber:regex(\\d*)}",
				new { controller = "BackInStockSubscription", action = "CustomerSubscriptions" });

            routeBuilder.MapLocalizedRoute("CustomerRewardPoints", "rewardpoints/history",
				new { controller = "Order", action = "CustomerRewardPoints" });

            routeBuilder.MapLocalizedRoute("CustomerRewardPointsPaged", "rewardpoints/history/page/{pageNumber:min(0)}",
				new { controller = "Order", action = "CustomerRewardPoints" });

            routeBuilder.MapLocalizedRoute("CustomerChangePassword", "customer/changepassword",
				new { controller = "Customer", action = "ChangePassword" });

            routeBuilder.MapLocalizedRoute("CustomerAvatar", "customer/avatar",
				new { controller = "Customer", action = "Avatar" });

            routeBuilder.MapLocalizedRoute("AccountActivation", "customer/activation",
				new { controller = "Customer", action = "AccountActivation" });

            routeBuilder.MapLocalizedRoute("EmailRevalidation", "customer/revalidateemail",
				new { controller = "Customer", action = "EmailRevalidation" });

            routeBuilder.MapLocalizedRoute("CustomerForumSubscriptions", "boards/forumsubscriptions",
				new { controller = "Boards", action = "CustomerForumSubscriptions" });

            routeBuilder.MapLocalizedRoute("CustomerForumSubscriptionsPaged", "boards/forumsubscriptions/{pageNumber:regex(\\d*)}",
				new { controller = "Boards", action = "CustomerForumSubscriptions" });

            routeBuilder.MapLocalizedRoute("CustomerAddressEdit", "customer/addressedit/{addressId:min(0)}",
				new { controller = "Customer", action = "AddressEdit" });

            routeBuilder.MapLocalizedRoute("CustomerAddressAdd", "customer/addressadd",
				new { controller = "Customer", action = "AddressAdd" });

            //customer profile page
            routeBuilder.MapLocalizedRoute("CustomerProfile", "profile/{id:min(0)}",
				new { controller = "Profile", action = "Index" });

            routeBuilder.MapLocalizedRoute("CustomerProfilePaged", "profile/{id}/page/{pageNumber:min(0)}",
				new { controller = "Profile", action = "Index" });

            //orders
            routeBuilder.MapLocalizedRoute("OrderDetails", "orderdetails/{orderId:min(0)}",
				new { controller = "Order", action = "Details" });

            routeBuilder.MapLocalizedRoute("ShipmentDetails", "orderdetails/shipment/{shipmentId}",
				new { controller = "Order", action = "ShipmentDetails" });

            routeBuilder.MapLocalizedRoute("ReturnRequest", "returnrequest/{orderId:min(0)}",
				new { controller = "ReturnRequest", action = "ReturnRequest" });

            routeBuilder.MapLocalizedRoute("ReOrder", "reorder/{orderId:min(0)}",
				new { controller = "Order", action = "ReOrder" });

            routeBuilder.MapLocalizedRoute("GetOrderPdfInvoice", "orderdetails/pdf/{orderId}",
				new { controller = "Order", action = "GetPdfInvoice" });

            routeBuilder.MapLocalizedRoute("PrintOrderDetails", "orderdetails/print/{orderId}",
				new { controller = "Order", action = "PrintOrderDetails" });

            //order downloads
            routeBuilder.MapRoute("GetDownload", "download/getdownload/{orderItemId:guid}/{agree?}",
				new { controller = "Download", action = "GetDownload" });

            routeBuilder.MapRoute("GetLicense", "download/getlicense/{orderItemId:guid}/",
				new { controller = "Download", action = "GetLicense" });

            routeBuilder.MapLocalizedRoute("DownloadUserAgreement", "customer/useragreement/{orderItemId:guid}",
				new { controller = "Customer", action = "UserAgreement" });

            routeBuilder.MapRoute("GetOrderNoteFile", "download/ordernotefile/{ordernoteid:min(0)}",
				new { controller = "Download", action = "GetOrderNoteFile" });

            //contact vendor
            routeBuilder.MapLocalizedRoute("ContactVendor", "contactvendor/{vendorId}",
				new { controller = "Common", action = "ContactVendor" });


            //apply for vendor account
            routeBuilder.MapLocalizedRoute("ApplyVendorAccount", "vendor/apply",
				new { controller = "Vendor", action = "ApplyVendor" });

            //vendor info
            routeBuilder.MapLocalizedRoute("CustomerVendorInfo", "customer/vendorinfo",
				new { controller = "Vendor", action = "Info" });

            //poll vote AJAX link
            routeBuilder.MapLocalizedRoute("PollVote", "poll/vote",
				new { controller = "Poll", action = "Vote" });

            //comparing products
            routeBuilder.MapLocalizedRoute("RemoveProductFromCompareList", "compareproducts/remove/{productId}",
				new { controller = "Product", action = "RemoveProductFromCompareList" });

            routeBuilder.MapLocalizedRoute("ClearCompareList", "clearcomparelist/",
				new { controller = "Product", action = "ClearCompareList" });

            //new RSS
            routeBuilder.MapLocalizedRoute("NewProductsRSS", "newproducts/rss",
				new { controller = "Product", action = "NewProductsRss" });
            
            //get state list by country ID  (AJAX link)
            routeBuilder.MapRoute("GetStatesByCountryId", "country/getstatesbycountryid/",
				new { controller = "Country", action = "GetStatesByCountryId" });

            //EU Cookie law accept button handler (AJAX link)
            routeBuilder.MapRoute("EuCookieLawAccept", "eucookielawaccept",
				new { controller = "Common", action = "EuCookieLawAccept" });

            //authenticate topic AJAX link
            routeBuilder.MapLocalizedRoute("TopicAuthenticate", "topic/authenticate",
				new { controller = "Topic", action = "Authenticate" });

            //product attributes with "upload file" type
            routeBuilder.MapLocalizedRoute("UploadFileProductAttribute", "uploadfileproductattribute/{attributeId:min(0)}",
				new { controller = "ShoppingCart", action = "UploadFileProductAttribute" });

            //checkout attributes with "upload file" type
            routeBuilder.MapLocalizedRoute("UploadFileCheckoutAttribute", "uploadfilecheckoutattribute/{attributeId:min(0)}",
				new { controller = "ShoppingCart", action = "UploadFileCheckoutAttribute" });

            //return request with "upload file" support
            routeBuilder.MapLocalizedRoute("UploadFileReturnRequest", "uploadfilereturnrequest",
				new { controller = "ReturnRequest", action = "UploadFileReturnRequest" });

            //forums
            routeBuilder.MapLocalizedRoute("ActiveDiscussions", "boards/activediscussions",
				new { controller = "Boards", action = "ActiveDiscussions" });

            routeBuilder.MapLocalizedRoute("ActiveDiscussionsPaged", "boards/activediscussions/page/{pageNumber:regex(\\d*)}",
				new { controller = "Boards", action = "ActiveDiscussions" });

            routeBuilder.MapLocalizedRoute("ActiveDiscussionsRSS", "boards/activediscussionsrss",
				new { controller = "Boards", action = "ActiveDiscussionsRSS" });

            routeBuilder.MapLocalizedRoute("PostEdit", "boards/postedit/{id:min(0)}",
				new { controller = "Boards", action = "PostEdit" });

            routeBuilder.MapLocalizedRoute("PostDelete", "boards/postdelete/{id:min(0)}",
				new { controller = "Boards", action = "PostDelete" });

            routeBuilder.MapLocalizedRoute("PostCreate", "boards/postcreate/{id:min(0)}",
				new { controller = "Boards", action = "PostCreate" });

            routeBuilder.MapLocalizedRoute("PostCreateQuote", "boards/postcreate/{id:min(0)}/{quote:min(0)}",
				new { controller = "Boards", action = "PostCreate" });

            routeBuilder.MapLocalizedRoute("TopicEdit", "boards/topicedit/{id:min(0)}",
				new { controller = "Boards", action = "TopicEdit" });

            routeBuilder.MapLocalizedRoute("TopicDelete", "boards/topicdelete/{id:min(0)}",
				new { controller = "Boards", action = "TopicDelete" });

            routeBuilder.MapLocalizedRoute("TopicCreate", "boards/topiccreate/{id:min(0)}",
				new { controller = "Boards", action = "TopicCreate" });

            routeBuilder.MapLocalizedRoute("TopicMove", "boards/topicmove/{id:min(0)}",
				new { controller = "Boards", action = "TopicMove" });

            routeBuilder.MapLocalizedRoute("TopicWatch", "boards/topicwatch/{id:min(0)}",
				new { controller = "Boards", action = "TopicWatch" });

            routeBuilder.MapLocalizedRoute("TopicSlug", "boards/topic/{id:min(0)}/{slug?}",
				new { controller = "Boards", action = "Topic" });

            routeBuilder.MapLocalizedRoute("TopicSlugPaged", "boards/topic/{id:min(0)}/{slug?}/page/{pageNumber:regex(\\d*)}",
				new { controller = "Boards", action = "Topic" });

            routeBuilder.MapLocalizedRoute("ForumWatch", "boards/forumwatch/{id:min(0)}",
				new { controller = "Boards", action = "ForumWatch" });

            routeBuilder.MapLocalizedRoute("ForumRSS", "boards/forumrss/{id:min(0)}",
				new { controller = "Boards", action = "ForumRSS" });

            routeBuilder.MapLocalizedRoute("ForumSlug", "boards/forum/{id:min(0)}/{slug?}",
				new { controller = "Boards", action = "Forum" });

            routeBuilder.MapLocalizedRoute("ForumSlugPaged", "boards/forum/{id:min(0)}/{slug?}/page/{pageNumber:regex(\\d*)}",
				new { controller = "Boards", action = "Forum" });

            routeBuilder.MapLocalizedRoute("ForumGroupSlug", "boards/forumgroup/{id:min(0)}/{slug?}",
				new { controller = "Boards", action = "ForumGroup"});

            routeBuilder.MapLocalizedRoute("Search", "boards/search",
				new { controller = "Boards", action = "Search" });

            //private messages
            routeBuilder.MapLocalizedRoute("PrivateMessages", "privatemessages/{tab?}",
				new { controller = "PrivateMessages", action = "Index" });

            routeBuilder.MapLocalizedRoute("PrivateMessagesPaged", "privatemessages/{tab?}/page/{pageNumber:min(0)}",
				new { controller = "PrivateMessages", action = "Index" });

            routeBuilder.MapLocalizedRoute("PrivateMessagesInbox", "inboxupdate",
				new { controller = "PrivateMessages", action = "InboxUpdate" });

            routeBuilder.MapLocalizedRoute("PrivateMessagesSent", "sentupdate",
				new { controller = "PrivateMessages", action = "SentUpdate" });

            routeBuilder.MapLocalizedRoute("SendPM", "sendpm/{toCustomerId:min(0)}",
				new { controller = "PrivateMessages", action = "SendPM" });

            routeBuilder.MapLocalizedRoute("SendPMReply", "sendpm/{toCustomerId:min(0)}/{replyToMessageId:min(0)}",
				new { controller = "PrivateMessages", action = "SendPM" });

            routeBuilder.MapLocalizedRoute("ViewPM", "viewpm/{privateMessageId:min(0)}",
				new { controller = "PrivateMessages", action = "ViewPM" });

            routeBuilder.MapLocalizedRoute("DeletePM", "deletepm/{privateMessageId:min(0)}",
				new { controller = "PrivateMessages", action = "DeletePM" });

            //activate newsletters
            routeBuilder.MapLocalizedRoute("NewsletterActivation", "newsletter/subscriptionactivation/{token:guid}/{active}",
				new { controller = "Newsletter", action = "SubscriptionActivation" });

            //robots.txt
            routeBuilder.MapRoute("robots.txt", "robots.txt",
				new { controller = "Common", action = "RobotsTextFile" });

            //sitemap (XML)
            routeBuilder.MapLocalizedRoute("sitemap.xml", "sitemap.xml",
				new { controller = "Common", action = "SitemapXml" });

            routeBuilder.MapLocalizedRoute("sitemap-indexed.xml", "sitemap-{Id:min(0)}.xml",
				new { controller = "Common", action = "SitemapXml" });

            //store closed
            routeBuilder.MapLocalizedRoute("StoreClosed", "storeclosed",
				new { controller = "Common", action = "StoreClosed" });

            //install
            routeBuilder.MapRoute("Installation", "install",
				new { controller = "Install", action = "Index" });

            //error page
            routeBuilder.MapLocalizedRoute("Error", "error",
                new { controller = "Common", action = "Error" });

            //page not found
            routeBuilder.MapLocalizedRoute("PageNotFound", "page-not-found", 
                new { controller = "Common", action = "PageNotFound" });

            
        }

        #endregion



        #region Properties

        /// <summary>
        /// Gets a priority of route provider
        /// </summary>
        public int Priority
        {
            get { return 0; }
        }

        #endregion
    }
}
 