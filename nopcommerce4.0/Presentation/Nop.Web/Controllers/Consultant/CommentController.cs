using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Z_Consultant;
using Nop.Services.Z_Consultant.Comment;
using Nop.Services.Z_Consultant.Helpers;
using Nop.Services.Z_Consultant.Post;
using Nop.Web.Extensions.Consultant;
using Nop.Web.Models.Consultant.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Controllers.Consultant
{
    public class CommentController : BasePublicController
    {

        private readonly IUrlHelper _urlHelper;
        private readonly IPostService _postService;
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;
        private readonly ICommentService _commentService;


        public CommentController(
            IUrlHelper urlHelper,
            IPostService postService,
            Core.IWorkContext workContext,
            IHostingEnvironment env,
            ICommentService commentService
            )
        {
            this._urlHelper = urlHelper;
            this._postService = postService;
            this._workContext = workContext;
            this._env = env;
            this._commentService = commentService;
        }



        private List<LinkInfo> GetLinksForPostComments(PagedList<Z_Consultant_Comment> list, string routName, int PostId)
        {
            var links = new List<LinkInfo>();

            if (list.HasPreviousPage)
                links.Add(CreateLinkForPostComments(routName, list.PreviousPageNumber,
                           list.PageSize, "previousPage", "GET", PostId));

            links.Add(CreateLinkForPostComments(routName, list.PageNumber,
                           list.PageSize, "self", "GET", PostId));

            if (list.HasNextPage)
                links.Add(CreateLinkForPostComments(routName, list.NextPageNumber,
                           list.PageSize, "nextPage", "GET", PostId));

            return links;
        }


        private LinkInfo CreateLinkForPostComments(
            string routeName, int pageNumber, int pageSize,
            string rel, string method, int PostId)
        {
            return new LinkInfo
            {
                Href = _urlHelper.Link(routeName,
                            new { PageNumber = pageNumber, PageSize = pageSize, PostId = PostId }),
                Rel = rel,
                Method = method
            };
        }



        [HttpGet]
        public IActionResult GetCommentsByPostId(PagingParams pagingParams, int PostId)
        {
            PagedList<Z_Consultant_Comment> model = null;
            bool isPostClosed = _postService.IsClosed(PostId);

            if (!isPostClosed)
            {
                if (!_workContext.CurrentCustomer.IsRegistered())
                    return Unauthorized();

                var currentUserId = _workContext.CurrentCustomer.Id;
                ViewBag.UserName = _workContext.CurrentCustomer.Username;

                if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                {
                    ViewBag.UserRole = "Consultant";
                    if (!_postService.IsAnswered(PostId) && !_postService.IsReserved(PostId))
                    {
                        model = _commentService.GetCommentsByPostId(pagingParams, PostId);
                    }
                    else if (_postService.IsConsultantAuthToPost(PostId, currentUserId))
                    {
                        model = _commentService.GetCommentsByPostId(pagingParams, PostId);
                    }
                    else
                    {
                        return Forbid();
                    }
                }
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                {
                    ViewBag.UserRole = "Registered";
                    if (_postService.IsCustomerAuthToPost(PostId, currentUserId))
                    {
                        model = _commentService.GetCommentsByPostId(pagingParams, PostId);
                    }
                    else
                    {
                        return Forbid();
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                model = _commentService.GetCommentsByPostId(pagingParams, PostId);
            }


            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());



            var outputModel = new CommentOutputModel
            {
                Paging = model.GetHeader(),
                Links = GetLinksForPostComments(model, "Consultant.Comment.GetCommentsByPostId", PostId),
                Items = model.List.Select(m => new CommentModel()
                {
                    Id = m.Id,
                    Text = m.Text,
                    PostId = m.PostId,
                    DateCreated = m.DateCreated,
                    DateUpdated = m.DateUpdated,
                    CommentedBy = m.CommentedBy,
                    CommentOwner = (m.Customer == null) ? m.Consultant.Username : m.Customer.Username
                }).ToList(),
            };

            if (isPostClosed)
            {
                return PartialView("~/Themes/Pavilion/Views/Consultant/Comment/CommentsOnClosedPost.cshtml", outputModel);
            }
            else
            {
                return PartialView("~/Themes/Pavilion/Views/Consultant/Comment/CommentsOnOpenPost.cshtml", outputModel);
            }
        }




        [HttpPost]
        public IActionResult AddComment([FromBody]CommentForPostModel model)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            var currentUserId = _workContext.CurrentCustomer.Id;
            ViewBag.UserName = _workContext.CurrentCustomer.Username;

            

            if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (_postService.IsClosed(model.PostId))
                    return BadRequest("Post is closed");

                ViewBag.UserRole = "Consultant";

                if (_postService.IsReserved(model.PostId))
                {
                    if (_postService.IsConsultantAuthToPost(model.PostId, currentUserId))
                    {
                        var commentCreated = _commentService.AddCommentByConsultant(model, currentUserId);
                        _postService.SetPostAnsweredByConsultant(model.PostId, currentUserId);
                        string userName = _workContext.CurrentCustomer.Username;
                        var commentToReturn = commentCreated.ToCommentModel();
                        commentToReturn.CommentOwner = userName;
                        return CreatedAtRoute("Consultant.Comment.GetComment", new { CommentId = commentToReturn.Id }, commentToReturn);
                    }
                    else
                    {
                        return Forbid();
                    }
                }
                else
                {
                    var commentCreated = _commentService.AddCommentByConsultant(model, currentUserId);
                    _postService.SetPostAnsweredByConsultant(model.PostId, currentUserId);
                    string userName = _workContext.CurrentCustomer.Username;
                    var commentToReturn = commentCreated.ToCommentModel();
                    commentToReturn.CommentOwner = userName;
                    return CreatedAtRoute("Consultant.Comment.GetComment", new { CommentId = commentToReturn.Id }, commentToReturn);
                }

            }
            else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (_postService.IsClosed(model.PostId))
                    return BadRequest("Post is closed");

                ViewBag.UserRole = "Registered";

                if (_postService.IsCustomerAuthToPost(model.PostId, currentUserId))
                {
                    var commentCreated = _commentService.AddCommentByCustomer(model, currentUserId);
                    string userName = _workContext.CurrentCustomer.Username;
                    var commentToReturn = commentCreated.ToCommentModel();
                    commentToReturn.CommentOwner = userName;
                    return CreatedAtRoute("Consultant.Comment.GetComment", new { CommentId = commentToReturn.Id }, commentToReturn);
                }
                else
                {
                    return Forbid();
                }

            }

            else
            {
                return Unauthorized();
            }

        }



        [HttpGet]
        public IActionResult GetComment(int CommentId)
        {
            Z_Consultant_Comment model = null;
            string commentOwner = string.Empty;

            var postId = _commentService.GetPostIdByCommentId(CommentId);

            if (!_postService.IsClosed(postId))
            {
                if (!_workContext.CurrentCustomer.IsRegistered())
                    return Unauthorized();

                var currentUserId = _workContext.CurrentCustomer.Id;


                if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
                {
                    ViewBag.UserRole = "Consultant";

                    if (_postService.IsConsultantAuthToPost(postId, currentUserId))
                    {
                        model = _commentService.GetCommentByCommentIdAndConsultantId(CommentId, currentUserId);
                        commentOwner = model.Consultant.Username;
                    }
                    else
                    {
                        return Forbid();
                    }
                }
                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
                {
                    ViewBag.UserRole = "Registered";

                    if (_postService.IsCustomerAuthToPost(postId, currentUserId))
                    {
                        model = _commentService.GetCommentByCommentIdAndCustomerId(CommentId, currentUserId);
                        commentOwner = model.Customer.Username;
                    }
                    else
                    {
                        return Forbid();
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                model = _commentService.GetComment(CommentId);
                commentOwner = (model.Customer == null) ? model.Consultant.Username : model.Customer.Username;
            }

            var modelToReturn = model.ToCommentModel();
            modelToReturn.CommentOwner = commentOwner;

            return Ok(modelToReturn);
        }


    }
}
