using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Localization;
using Nop.Web.Framework.Mvc.Routing;

namespace Nop.Plugin.Consultant.Api
{
    class RouteProvider : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            





            routeBuilder.MapRoute("Consultant.Api.User.Search", "Consultant/Api/User/GetUserInfo",
            new { controller = "User", action = "GetUserInfo" });

            routeBuilder.MapRoute("Consultant.Api.Values", "Consultant/Api/Values",
          new { controller = "Values", action = "GetValues" });

            routeBuilder.MapRoute("ConsultantApiGetValues", "Consultant/Api/Auth/GetValues",
           new { controller = "Auth", action = "GetValues" });

            routeBuilder.MapRoute("Consultant.Api.Auth.Login", "Consultant/Api/Auth/Login",
            new { controller = "Auth", action = "Login" });
            routeBuilder.MapRoute("Consultant.Api.Auth.Info", "Consultant/Api/Auth/Info",
            new { controller = "Auth", action = "Info" });

            routeBuilder.MapRoute("Consultant.Api.Post.Search", "Consultant/Api/Post/Search/",
            new { controller = "Post", action = "Search" });


            routeBuilder.MapRoute("Consultant.Api.Category.GetCategory", "Consultant/Api/Category/GetCategory/{CategoryId:min(0)}",
            new { controller = "Category", action = "GetCategory" });

            routeBuilder.MapRoute("Consultant.Api.Category.GetCategories", "Consultant/Api/Category/GetCategories",
            new { controller = "Category", action = "GetCategories" });

            routeBuilder.MapRoute("Consultant.Api.Category.GetCategoryWithSubCategories", "Consultant/Api/Category/GetCategoryWithSubCategories/{CategoryId:min(0)}",
            new { controller = "Category", action = "GetCategoryWithSubCategories" });

            routeBuilder.MapRoute("Consultant.Api.Category.GetCategoriesWithSubCategories", "Consultant/Api/Category/GetCategoriesWithSubCategories",
            new { controller = "Category", action = "GetCategoriesWithSubCategories" });


            routeBuilder.MapRoute("Consultant.Api.SubCategory.GetSubCategories", "Consultant/Api/SubCategory/GetSubCategories",
            new { controller = "SubCategory", action = "GetSubCategories" });

            routeBuilder.MapRoute("Consultant.Api.SubCategory.GetSubCategory", "Consultant/Api/SubCategory/GetSubCategory/{SubCategoryId:min(0)}",
            new { controller = "SubCategory", action = "GetSubCategory" });
            routeBuilder.MapRoute("Consultant.Api.SubCategory.GetSubCategoriesByCategoryId", "Consultant/Api/SubCategory/GetSubCategoriesByCategoryId/{CategoryId:min(0)}",
            new { controller = "SubCategory", action = "GetSubCategoriesByCategoryId" });


            routeBuilder.MapRoute("Consultant.Api.Post.GetAllClosedPosts", "Consultant/Api/Post/GetAllClosedPosts",
            new { controller = "Post", action = "GetAllClosedPosts" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllClosedPostsByCategoryId", "Consultant/Api/Post/GetAllClosedPostsByCategoryId/{CategoryId:min(0)}",
            new { controller = "Post", action = "GetAllClosedPostsByCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllClosedPostsBySubCategoryId", "Consultant/Api/Post/GetAllClosedPostsBySubCategoryId/{SubCategoryId:min(0)}",
            new { controller = "Post", action = "GetAllClosedPostsBySubCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllOpenPostsForCustomer", "Consultant/Api/Post/GetAllOpenPostsForCustomer",
            new { controller = "Post", action = "GetAllOpenPostsForCustomer" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllOpenPostsForCustomerByCategoryId", "Consultant/Api/Post/GetAllOpenPostsForCustomerByCategoryId/{CategoryId:min(0)}",
            new { controller = "Post", action = "GetAllOpenPostsForCustomerByCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllOpenPostsForCustomerBySubCategoryId", "Consultant/Api/Post/GetAllOpenPostsForCustomerBySubCategoryId/{SubCategoryId:min(0)}",
            new { controller = "Post", action = "GetAllOpenPostsForCustomerBySubCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllClosedPostsForCustomer", "Consultant/Api/Post/GetAllClosedPostsForCustomer",
            new { controller = "Post", action = "GetAllClosedPostsForCustomer" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllClosedPostsForCustomerByCategoryId", "Consultant/Api/Post/GetAllClosedPostsForCustomerByCategoryId/{CategoryId:min(0)}",
            new { controller = "Post", action = "GetAllClosedPostsForCustomerByCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllClosedPostsForCustomerBySubCategoryId", "Consultant/Api/Post/GetAllClosedPostsForCustomerBySubCategoryId/{SubCategoryId:min(0)}",
            new { controller = "Post", action = "GetAllClosedPostsForCustomerBySubCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllPostsForCustomer", "Consultant/Api/Post/GetAllPostsForCustomer",
            new { controller = "Post", action = "GetAllPostsForCustomer" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllPostsForCustomerByCategoryId", "Consultant/Api/Post/GetAllPostsForCustomerByCategoryId/{CategoryId:min(0)}",
            new { controller = "Post", action = "GetAllPostsForCustomerByCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllPostsForCustomerBySubCategoryId", "Consultant/Api/Post/GetAllPostsForCustomerBySubCategoryId/{SubCategoryId:min(0)}",
            new { controller = "Post", action = "GetAllPostsForCustomerBySubCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Post.AddPost", "Consultant/Api/Post/AddPost/",
            new { controller = "Post", action = "AddPost" });

            routeBuilder.MapRoute("Consultant.Api.Post.ReservePost", "Consultant/Api/Post/ReservePost/",
            new { controller = "Post", action = "ReservePost" });
            routeBuilder.MapRoute("Consultant.Api.Post.UnReservePost", "Consultant/Api/Post/UnReservePost/",
            new { controller = "Post", action = "UnReservePost" });

            routeBuilder.MapRoute("Consultant.Api.Post.GetAllReservedPostsForConsultant", "Consultant/Api/Post/GetAllReservedPostsForConsultant",
            new { controller = "Post", action = "GetAllReservedPostsForConsultant" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllReservedPostsForConsultantByCategoryId", "Consultant/Api/Post/GetAllReservedPostsForConsultantByCategoryId/{CategoryId:min(0)}",
            new { controller = "Post", action = "GetAllReservedPostsForConsultantByCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllReservedPostsForConsultantBySubCategoryId", "Consultant/Api/Post/GetAllReservedPostsForConsultantBySubCategoryId/{SubCategoryId:min(0)}",
            new { controller = "Post", action = "GetAllReservedPostsForConsultantBySubCategoryId" });

            routeBuilder.MapRoute("Consultant.Api.Post.ClosePost", "Consultant/Api/Post/ClosePost/",
            new { controller = "Post", action = "ClosePost" });
            routeBuilder.MapRoute("Consultant.Api.Post.RatePost", "Consultant/Api/Post/RatePost/",
            new { controller = "Post", action = "RatePost" });

            routeBuilder.MapRoute("Consultant.Api.Post.DisplayPost", "Consultant/Api/Post/DisplayPost/",
            new { controller = "Post", action = "DisplayPost" });
            routeBuilder.MapRoute("Consultant.Api.Post.HidePost", "Consultant/Api/Post/HidePost/",
            new { controller = "Post", action = "HidePost" });

            routeBuilder.MapRoute("Consultant.Api.Post.CloseAndRatePost", "Consultant/Api/Post/CloseAndRatePost/",
            new { controller = "Post", action = "CloseAndRatePost" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllOpenPostsNotAns", "Consultant/Api/Post/GetAllOpenPostsNotAns",
            new { controller = "Post", action = "GetAllOpenPostsNotAns" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllOpenPostsNotAnsByCategoryId", "Consultant/Api/Post/GetAllOpenPostsNotAnsByCategoryId/{CategoryId:min(0)}",
            new { controller = "Post", action = "GetAllOpenPostsNotAnsByCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllOpenPostsNotAnsBySubCategoryId", "Consultant/Api/Post/GetAllOpenPostsNotAnsBySubCategoryId/{SubCategoryId:min(0)}",
            new { controller = "Post", action = "GetAllOpenPostsNotAnsBySubCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllOpenPostsForConsultant", "Consultant/Api/Post/GetAllOpenPostsForConsultant",
            new { controller = "Post", action = "GetAllOpenPostsForConsultant" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllOpenPostsForConsultantByCategoryId", "Consultant/Api/Post/GetAllOpenPostsForConsultantByCategoryId/{CategoryId:min(0)}",
            new { controller = "Post", action = "GetAllOpenPostsForConsultantByCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllOpenPostsForConsultantBySubCategoryId", "Consultant/Api/Post/GetAllOpenPostsForConsultantBySubCategoryId/{SubCategoryId:min(0)}",
            new { controller = "Post", action = "GetAllOpenPostsForConsultantBySubCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllClosedPostsForConsultant", "Consultant/Api/Post/GetAllClosedPostsForConsultant",
            new { controller = "Post", action = "GetAllClosedPostsForConsultant" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllClosedPostsForConsultantByCategoryId", "Consultant/Api/Post/GetAllClosedPostsForConsultantByCategoryId/{CategoryId:min(0)}",
            new { controller = "Post", action = "GetAllClosedPostsForConsultantByCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllClosedPostsForConsultantBySubCategoryId", "Consultant/Api/Post/GetAllClosedPostsForConsultantBySubCategoryId/{SubCategoryId:min(0)}",
            new { controller = "Post", action = "GetAllClosedPostsForConsultantBySubCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllPostsForConsultant", "Consultant/Api/Post/GetAllPostsForConsultant",
            new { controller = "Post", action = "GetAllPostsForConsultant" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllPostsForConsultantByCategoryId", "Consultant/Api/Post/GetAllPostsForConsultantByCategoryId/{CategoryId:min(0)}",
            new { controller = "Post", action = "GetAllPostsForConsultantByCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetAllPostsForConsultantBySubCategoryId", "Consultant/Api/Post/GetAllPostsForConsultantBySubCategoryId/{SubCategoryId:min(0)}",
            new { controller = "Post", action = "GetAllPostsForConsultantBySubCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Post.SetPostToSubCategory", "Consultant/Api/Post/SetPostToSubCategory/",
            new { controller = "Post", action = "SetPostToSubCategory" });
            routeBuilder.MapRoute("Consultant.Api.Post.SetPostToCategoryAndSubCategory", "Consultant/Api/Post/SetPostToCategoryAndSubCategory/",
            new { controller = "Post", action = "SetPostToCategoryAndSubCategory" });
            routeBuilder.MapRoute("Consultant.Api.Post.GetPost", "Consultant/Api/Post/GetPost/{PostId:min(0)}",
            new { controller = "Post", action = "GetPost" });
            routeBuilder.MapRoute("Consultant.Api.Post.getFile", "Consultant/Api/Images/{path}",
            new { controller = "Post", action = "getFile" });
            routeBuilder.MapRoute("Consultant.Api.Post.getFilePrivate", "Consultant/Api/Imgs/{path}",
            new { controller = "Post", action = "getFilePrivate" });


            routeBuilder.MapRoute("Consultant.Api.Comment.GetCommentsByPostId", "Consultant/Api/Comment/GetCommentsByPostId/{PostId:min(0)}",
            new { controller = "Comment", action = "GetCommentsByPostId" });
            routeBuilder.MapRoute("Consultant.Api.Comment.GetComment", "Consultant/Api/Comment/GetComment/{CommentId:min(0)}",
            new { controller = "Comment", action = "GetComment" });
            routeBuilder.MapRoute("Consultant.Api.Comment.AddComment", "Consultant/Api/Comment/AddComment/",
            new { controller = "Comment", action = "AddComment" });
            routeBuilder.MapRoute("Consultant.Api.Comment.UpdateComment", "Consultant/Api/Comment/UpdateComment/",
            new { controller = "Comment", action = "UpdateComment" });
            routeBuilder.MapRoute("Consultant.Api.Comment.DeleteComment", "Consultant/Api/Comment/DeleteComment/",
            new { controller = "Comment", action = "DeleteComment" });

            

            //admin

            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetAllPosts", "Consultant/Api/Admin/Post/GetAllPosts",
            new { controller = "AdminPost", action = "GetAllPosts" });
            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetAllPostsByCategoryId", "Consultant/Api/Admin/Post/GetAllPostsByCategoryId/{CategoryId:min(0)}",
            new { controller = "AdminPost", action = "GetAllPostsByCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetAllPostsBySubCategoryId", "Consultant/Api/Admin/Post/GetAllPostsBySubCategoryId/{SubCategoryId:min(0)}",
            new { controller = "AdminPost", action = "GetAllPostsBySubCategoryId" });

            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetAllClosedPosts", "Consultant/Api/Admin/Post/GetAllClosedPosts",
            new { controller = "AdminPost", action = "GetAllClosedPosts" });
            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetAllClosedPostsByCategoryId", "Consultant/Api/Admin/Post/GetAllClosedPostsByCategoryId/{CategoryId:min(0)}",
            new { controller = "AdminPost", action = "GetAllClosedPostsByCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetAllClosedPostsBySubCategoryId", "Consultant/Api/Admin/Post/GetAllClosedPostsBySubCategoryId/{SubCategoryId:min(0)}",
            new { controller = "AdminPost", action = "GetAllClosedPostsBySubCategoryId" });

            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetClosedDisplayedPosts", "Consultant/Api/Admin/Post/GetClosedDisplayedPosts",
            new { controller = "AdminPost", action = "GetClosedDisplayedPosts" });
            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetClosedDisplayedPostsByCategoryId", "Consultant/Api/Admin/Post/GetClosedDisplayedPostsByCategoryId/{CategoryId:min(0)}",
            new { controller = "AdminPost", action = "GetClosedDisplayedPostsByCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetClosedDisplayedPostsBySubCategoryId", "Consultant/Api/Admin/Post/GetClosedDisplayedPostsBySubCategoryId/{SubCategoryId:min(0)}",
            new { controller = "AdminPost", action = "GetClosedDisplayedPostsBySubCategoryId" });


            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetClosedHiddenPosts", "Consultant/Api/Admin/Post/GetClosedHiddenPosts",
            new { controller = "AdminPost", action = "GetClosedHiddenPosts" });
            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetClosedHiddenPostsByCategoryId", "Consultant/Api/Admin/Post/GetClosedHiddenPostsByCategoryId/{CategoryId:min(0)}",
            new { controller = "AdminPost", action = "GetClosedHiddenPostsByCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetClosedHiddenPostsBySubCategoryId", "Consultant/Api/Admin/Post/GetClosedHiddenPostsBySubCategoryId/{SubCategoryId:min(0)}",
            new { controller = "AdminPost", action = "GetClosedHiddenPostsBySubCategoryId" });


            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetAllOpenPosts", "Consultant/Api/Admin/Post/GetAllOpenPosts",
            new { controller = "AdminPost", action = "GetAllOpenPosts" });
            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetAllOpenPostsByCategoryId", "Consultant/Api/Admin/Post/GetAllOpenPostsByCategoryId/{CategoryId:min(0)}",
            new { controller = "AdminPost", action = "GetAllOpenPostsByCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetAllOpenPostsBySubCategoryId", "Consultant/Api/Admin/Post/GetAllOpenPostsBySubCategoryId/{SubCategoryId:min(0)}",
            new { controller = "AdminPost", action = "GetAllOpenPostsBySubCategoryId" });


            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetAllOpenPostsNotAns", "Consultant/Api/Admin/Post/GetAllOpenPostsNotAns",
            new { controller = "AdminPost", action = "GetAllOpenPostsNotAns" });
            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetAllOpenPostsNotAnsByCategoryId", "Consultant/Api/Admin/Post/GetAllOpenPostsNotAnsByCategoryId/{CategoryId:min(0)}",
            new { controller = "AdminPost", action = "GetAllOpenPostsNotAnsByCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetAllOpenPostsNotAnsBySubCategoryId", "Consultant/Api/Admin/Post/GetAllOpenPostsNotAnsBySubCategoryId/{SubCategoryId:min(0)}",
            new { controller = "AdminPost", action = "GetAllOpenPostsNotAnsBySubCategoryId" });


            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetAllOpenPostsAns", "Consultant/Api/Admin/Post/GetAllOpenPostsAns",
            new { controller = "AdminPost", action = "GetAllOpenPostsAns" });
            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetAllOpenPostsAnsByCategoryId", "Consultant/Api/Admin/Post/GetAllOpenPostsAnsByCategoryId/{CategoryId:min(0)}",
            new { controller = "AdminPost", action = "GetAllOpenPostsAnsByCategoryId" });
            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetAllOpenPostsAnsBySubCategoryId", "Consultant/Api/Admin/Post/GetAllOpenPostsAnsBySubCategoryId/{SubCategoryId:min(0)}",
            new { controller = "AdminPost", action = "GetAllOpenPostsAnsBySubCategoryId" });

            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.GetPost", "Consultant/Api/Admin/Post/GetPost/{PostId:min(0)}",
            new { controller = "AdminPost", action = "GetPost" });
            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.getFile", "Consultant/Api/Admin/Images/{path}",
            new { controller = "AdminPost", action = "getFile" });

            routeBuilder.MapRoute("Consultant.Api.Admin.AdminPost.UnReservePost", "Consultant/Api/Admin/Post/UnReservePost/",
            new { controller = "AdminPost", action = "UnReservePost" });



        }
    }
}
