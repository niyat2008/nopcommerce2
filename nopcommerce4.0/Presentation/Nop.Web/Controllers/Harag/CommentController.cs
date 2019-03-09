using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Z_Harag;
using Nop.Services.Z_Harag.Comment;
using Nop.Services.Z_Harag.Helpers;
using Nop.Services.Z_Harag.Notification;
using Nop.Services.Z_Harag.Post;
using Nop.Web.Extensions.Harag;
using Nop.Web.Models.Harag.Comment;
using Nop.Web.Models.Harag.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Controllers.Harag
{
    public class CommentController : BasePublicController
    {
        private readonly INotificationService _notificationService;

        private readonly IUrlHelper _urlHelper;
        private readonly IPostService _postService;
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;
        private readonly ICommentService _commentService;


        public CommentController(
            IUrlHelper urlHelper,
            INotificationService _notificationService,
            IPostService postService,
            Core.IWorkContext workContext,
            IHostingEnvironment env,
            ICommentService commentService
            )
        {
            this._urlHelper = urlHelper;
            this._notificationService = _notificationService;
            this._postService = postService;
            this._workContext = workContext;
            this._env = env;
            this._commentService = commentService;
        }



        private List<LinkInfo> GetLinksForPostComments(PagedList<Z_Harag_Comment> list, string routName, int PostId)
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
        public IActionResult GetAllPostComments(int PostId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            var currentUserId = _workContext.CurrentCustomer.Id;
            ViewBag.UserName = _workContext.CurrentCustomer.Username;

            if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
            {
                var modelFromDb = _commentService.GetCommentsByPostId(PostId);
                var outputModel = new CommentOutputModel
                {
                    //Links = GetLinksForPostComments(model, "Consultant.Comment.GetCommentsByPostId", PostId),
                    Items = modelFromDb.Select(m => new CommentModel()
                    {
                        Id = m.Id,
                        Text = m.Text,
                        PostId = m.PostId,
                        DateCreated = m.DateCreated,
                        DateUpdated = m.DateUpdated,
                        CommentedBy = m.CommentedBy,
                        CommentOwner = m.Customer.Username,
                        PostOwnerId = (int)m.CustomerId
                        //Photos = m.Photos.Select(p => p.Url).ToList()
                    }).ToList(),
                };
                return PartialView("~/Themes/Pavilion/Views/Harag/Comment/CommentsOnPost.cshtml", outputModel);
            }
            else
            {
                return Unauthorized();
            }

        }
        [HttpPost]
        public IActionResult AddHaragComment([FromBody] CommentForPostModel model)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();

            var currentUserId = _workContext.CurrentCustomer.Id;
            ViewBag.UserName = _workContext.CurrentCustomer.Username;

            if (_workContext.CurrentCustomer.IsInCustomerRole(RolesType.Registered, true))
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                 
                List<string> errors = new List<string>();
                var commentCreated = _commentService.AddComment(model, currentUserId);
                var post = _postService.GetPost(model.PostId, "");

                if (post.CustomerId != currentUserId)
                {
                    var notifications = _notificationService.PushPostCommentNotification(new CommentForNotifyModel
                    {
                        CustomerId = currentUserId,
                        Text = commentCreated.Text,
                        PostId = model.PostId,
                        Time = DateTime.Now,
                        PostOwner = post.CustomerId
                    });

                    var d = _notificationService.PushPostOwnerNotification(new CommentForNotifyModel
                    {
                        CustomerId = currentUserId,
                        Text = commentCreated.Text,
                        PostId = model.PostId,
                        Time = DateTime.Now,
                        PostOwner = post.CustomerId
                    });
                }
                else
                {

                }
                 
                string userName = _workContext.CurrentCustomer.Username;
                var commentToReturn = commentCreated.ToCommentModel();
                commentToReturn.CommentOwner = userName;
                commentToReturn.PostOwnerId = (int)commentCreated.CustomerId;

                return PartialView("~/Themes/Pavilion/Views/Harag/Comment/_CommentTemplatePartial.cshtml", commentToReturn);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet]
        public IActionResult ReportCommentAjax(int postId, int commentId, int type)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return Unauthorized();
            var post = _postService.GetPost(postId, "");

            if (post == null)
                return BadRequest();

            var comment = _commentService.GetCommentById(commentId);

            if (comment == null)
                return BadRequest();

            if(type <= 0)
            {
                return BadRequest();
            }
             
            var report = new Z_Harag_CommentReport
            {
                CommentId = commentId, 
                ReportCategory = (byte) type, 
                ReporterUser = _workContext.CurrentCustomer.Id
            };

            var result = _commentService.ReportComment(report);

            if (result != null)
            {
                Ok(new { stat = true });
            } 

            return Ok(new { stat = false });
        }

    }
}
