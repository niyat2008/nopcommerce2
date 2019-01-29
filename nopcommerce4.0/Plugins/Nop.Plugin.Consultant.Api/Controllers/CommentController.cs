//using Microsoft.AspNetCore.Mvc;
//using Nop.Core.Domain.Customers;
//using Nop.Core.Domain.Z_Consultant;
//using Nop.Plugin.Consultant.Api.Extensions;
//using Nop.Plugin.Consultant.Api.Models.Comment;
//using Nop.Services.Z_Consultant.Comment;
//using Nop.Services.Z_Consultant.Helpers;
//using Nop.Services.Z_Consultant.Post;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Nop.Plugin.Consultant.Api.Controllers
//{
//    public class CommentController : Controller
//    {

//        private readonly IPostService _postService;
//        private readonly ICommentService _commentService;
//        private readonly IUrlHelper _urlHelper;
//        private readonly Core.IWorkContext _workContext;

//        public CommentController(IPostService postService,
//            ICommentService commentService,
//            Core.IWorkContext workContext,
//            IUrlHelper urlHelper)
//        {
//            this._commentService = commentService;
//            this._postService = postService;
//            this._workContext = workContext;
//            this._urlHelper = urlHelper;
//        }

//        private List<LinkInfo> GetLinks(PagedList<Z_Consultant_Comment> list, string routName)
//        {
//            var links = new List<LinkInfo>();

//            if (list.HasPreviousPage)
//                links.Add(CreateLink(routName, list.PreviousPageNumber,
//                           list.PageSize, "previousPage", "GET"));

//            links.Add(CreateLink(routName, list.PageNumber,
//                           list.PageSize, "self", "GET"));

//            if (list.HasNextPage)
//                links.Add(CreateLink(routName, list.NextPageNumber,
//                           list.PageSize, "nextPage", "GET"));

//            return links;
//        }

//        private LinkInfo CreateLink(
//            string routeName, int pageNumber, int pageSize,
//            string rel, string method)
//        {
//            return new LinkInfo
//            {
//                Href = _urlHelper.Link(routeName,
//                            new { PageNumber = pageNumber, PageSize = pageSize }),
//                Rel = rel,
//                Method = method
//            };
//        }



//        [HttpGet]
//        public IActionResult GetCommentsByPostId(PagingParams pagingParams, int PostId)
//        {
//            PagedList<Z_Consultant_Comment> model = null;

//            if (!_postService.IsClosed(PostId))
//            {
//                if (!_workContext.CurrentCustomer.IsRegistered())
//                    return Unauthorized();

//                var currentUserId = _workContext.CurrentCustomer.Id;


//                if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
//                {
//                    if (_postService.IsConsultantAuthToPost(PostId, currentUserId))
//                    {
//                        model = _commentService.GetCommentsByPostId(pagingParams, PostId);
//                    }
//                    else
//                    {
//                        return Forbid();
//                    }
//                }
//                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
//                {
//                    if (_postService.IsCustomerAuthToPost(PostId, currentUserId))
//                    {
//                        model = _commentService.GetCommentsByPostId(pagingParams, PostId);
//                    }
//                    else
//                    {
//                        return Forbid();
//                    }
//                }
//                else
//                {
//                    return Unauthorized();
//                }
//            }
//            else
//            {
//                model = _commentService.GetCommentsByPostId(pagingParams, PostId);
//            }


//            Response.Headers.Add("X-Pagination", model.GetHeader().ToJson());



//            var outputModel = new CommentOutputModel
//            {
//                Paging = model.GetHeader(),
//                Links = GetLinks(model, "Consultant.Api.Comment.GetCommentsByPostId"),
//                Items = model.List.Select(m => new CommentModel()
//                {
//                    Id = m.Id,
//                    Text = m.Text,
//                    PostId = m.PostId,
//                    DateCreated = m.DateCreated,
//                    DateUpdated = m.DateUpdated,
//                    CommentedBy = m.CommentedBy,
//                    CommentOwner = (m.Customer == null) ? m.Consultant.Username : m.Customer.Username
//                }).ToList(),
//            };
//            return Ok(outputModel);
//        }



//        [HttpGet]
//        public IActionResult GetComment(int CommentId)
//        {
//            Z_Consultant_Comment model = null;
//            string commentOwner = string.Empty;

//            var postId = _commentService.GetPostIdByCommentId(CommentId);

//            if (!_postService.IsClosed(postId))
//            {
//                if (!_workContext.CurrentCustomer.IsRegistered())
//                    return Unauthorized();

//                var currentUserId = _workContext.CurrentCustomer.Id;


//                if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
//                {
//                    if (_postService.IsConsultantAuthToPost(postId, currentUserId))
//                    {
//                        model = _commentService.GetCommentByCommentIdAndConsultantId(CommentId, currentUserId);
//                        commentOwner = model.Consultant.Username;
//                    }
//                    else
//                    {
//                        return Forbid();
//                    }
//                }
//                else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
//                {
//                    if (_postService.IsCustomerAuthToPost(postId, currentUserId))
//                    {
//                        model = _commentService.GetCommentByCommentIdAndCustomerId(CommentId, currentUserId);
//                        commentOwner = model.Customer.Username;
//                    }
//                    else
//                    {
//                        return Forbid();
//                    }
//                }
//                else
//                {
//                    return Unauthorized();
//                }
//            }
//            else
//            {
//                model = _commentService.GetComment(CommentId);
//                commentOwner = (model.Customer == null) ? model.Consultant.Username : model.Customer.Username;
//            }

//            var modelToReturn = model.ToCommentModel();
//            modelToReturn.CommentOwner = commentOwner;

//            return Ok(modelToReturn);
//        }




//        [HttpPost]
//        public IActionResult AddComment([FromBody]CommentForPostModel model)
//        {
//            if (!_workContext.CurrentCustomer.IsRegistered())
//                return Unauthorized();

//            var currentUserId = _workContext.CurrentCustomer.Id;


//            if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
//            {
//                if (!ModelState.IsValid)
//                    return BadRequest(ModelState);

//                if (_postService.IsClosed(model.PostId))
//                    return BadRequest("Post is closed");

//                if (_postService.IsReserved(model.PostId))
//                {
//                    if (_postService.IsConsultantAuthToPost(model.PostId, currentUserId))
//                    {
//                        var commentCreated = _commentService.AddCommentByConsultant(model, currentUserId);
//                        string userName = _workContext.CurrentCustomer.Username;
//                        var commentToReturn = commentCreated.ToCommentModel();
//                        commentToReturn.CommentOwner = userName;
//                        return CreatedAtRoute("Consultant.Api.Comment.GetComment", new { CommentId = commentToReturn.Id }, commentToReturn);
//                    }
//                    else
//                    {
//                        return Forbid();
//                    }
//                }
//                else
//                {
//                    var commentCreated = _commentService.AddCommentByConsultant(model, currentUserId);
//                    _postService.SetPostAnsweredByConsultant(model.PostId, currentUserId);
//                    string userName = _workContext.CurrentCustomer.Username;
//                    var commentToReturn = commentCreated.ToCommentModel();
//                    commentToReturn.CommentOwner = userName;
//                    return CreatedAtRoute("Consultant.Api.Comment.GetComment", new { CommentId = commentToReturn.Id }, commentToReturn);
//                }

//            }
//            else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
//            {
//                if (!ModelState.IsValid)
//                    return BadRequest(ModelState);

//                if (_postService.IsClosed(model.PostId))
//                    return BadRequest("Post is closed");

//                if (_postService.IsCustomerAuthToPost(model.PostId, currentUserId))
//                {
//                    var commentCreated = _commentService.AddCommentByCustomer(model, currentUserId);
//                    string userName = _workContext.CurrentCustomer.Username;
//                    var commentToReturn = commentCreated.ToCommentModel();
//                    commentToReturn.CommentOwner = userName;
//                    return CreatedAtRoute("Consultant.Api.Comment.GetComment", new { CommentId = commentToReturn.Id }, commentToReturn);
//                }
//                else
//                {
//                    return Forbid();
//                }

//            }

//            else
//            {
//                return Unauthorized();
//            }

//        }



//        [HttpPost]
//        public IActionResult UpdateComment([FromBody]UpdateCommentModel model)
//        {
//            if (!_workContext.CurrentCustomer.IsRegistered())
//                return Unauthorized();

//            var currentUserId = _workContext.CurrentCustomer.Id;

//            if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
//            {
//                if (!_commentService.IsConsultantAuthToComment(model.CommentId, currentUserId))
//                    Forbid();
//                if (_commentService.IsExist(model.CommentId))
//                    return BadRequest("Comment not found");

//                var commentUpdated = _commentService.UpdateComment(model);
//                var commentToReturn = commentUpdated.ToCommentModel();
//                return CreatedAtRoute("Consultant.Api.Comment.GetComment", new { CommentId = commentToReturn.Id }, commentToReturn);
//            }
//            else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
//            {
//                if (!_commentService.IsCustomerAuthToComment(model.CommentId, currentUserId))
//                    Forbid();
//                if (_commentService.IsExist(model.CommentId))
//                    return BadRequest("Comment not found");

//                if (_commentService.IsCommentPostClosed(model.CommentId))
//                    return BadRequest("not allowed, the post is closed");

//                var commentUpdated = _commentService.UpdateComment(model);
//                var commentToReturn = commentUpdated.ToCommentModel();
//                return CreatedAtRoute("Consultant.Api.Comment.GetComment", new { CommentId = commentToReturn.Id }, commentToReturn);
//            }
//            else
//            {
//                return Unauthorized();
//            }
            
//        }

//        [HttpPost]
//        public IActionResult DeleteComment([FromBody]DeleteCommentModel model)
//        {
//            if (!_workContext.CurrentCustomer.IsRegistered())
//                return Unauthorized();

//            var currentUserId = _workContext.CurrentCustomer.Id;

//            if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Consultant, true))
//            {
//                if (!_commentService.IsConsultantAuthToComment(model.CommentId, currentUserId))
//                    Forbid();
//                if (_commentService.IsExist(model.CommentId))
//                    return BadRequest("Comment not found");

//                _commentService.DeleteComment(model);

//                return Ok();
//            }
//            else if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
//            {
//                if (!_commentService.IsCustomerAuthToComment(model.CommentId, currentUserId))
//                    Forbid();

//                if (_commentService.IsExist(model.CommentId))
//                    return BadRequest("Comment not found");

//                if (_commentService.IsCommentPostClosed(model.CommentId))
//                    return BadRequest("not allowed, the post is closed");

//                _commentService.DeleteComment(model);

//                return Ok();
//            }
//            else
//            {
//                return Unauthorized();
//            }

//        }
//    }
//}
