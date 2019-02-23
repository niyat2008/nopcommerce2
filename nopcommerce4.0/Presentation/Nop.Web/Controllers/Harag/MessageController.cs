using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Nop.Core.Domain.Customers;
using Nop.Web.Extensions.Harag;

using Nop.Core.Domain.Z_Harag;
using Nop.Services.Z_Harag.Helpers;
using Nop.Services.Z_Harag.Message;
using Nop.Services.Z_Harag.Post;
using Nop.Web.Models.Harag.Message;

namespace Nop.Web.Controllers.Harag
{
    public class MessageController : BasePublicController
    {
        private readonly IUrlHelper _urlHelper;
        private readonly IPostService _postService;
        private readonly IMessageService _messageService;
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;


        public MessageController(
         IUrlHelper urlHelper,
         IPostService postService,
         IMessageService _messageService,
         Core.IWorkContext workContext,
         IHostingEnvironment env
         )
        {
            this._urlHelper = urlHelper;
            this._postService = postService;
            this._messageService = _messageService;
            this._workContext = workContext;
            this._env = env;
        }
         
 
        [HttpGet]
        public IActionResult AddPostMessage(int postId, string Message)
        {

            MessageModel message = new MessageModel
            {
                Message = Message,
                postId = postId
            };

            if (!_workContext.CurrentCustomer.IsRegistered())
                return RedirectToRoute("Login", new { returnUrl = "Harag" });

            if (!ModelState.IsValid )
                return Ok(new { stat = false, errors = ModelState.Values});

            var currentUserId = _workContext.CurrentCustomer.Id;
            ViewBag.UserName = _workContext.CurrentCustomer.Username;
             
            var msg = new Z_Harag_Message
            {
                Message = message.Message,
                CreatedTime = DateTime.Now,
                CustomerId = currentUserId,
                PostId = message.postId
            };
                 
            var mes = _messageService.AddMessage(msg);

            var model = new MessageOutputModel{
                Message = mes.Message,
                DateTime = (DateTime) mes.CreatedTime,
                postId = (int)mes.PostId,
                User = mes.Customer.Username,
                userId = (int)mes.CustomerId
            };

            if (mes != null)
                return PartialView("~/Themes/Pavilion/Views/Harag/Message/_MessageTemplatePartial.cshtml", model);

            return BadRequest(); 
        }

        public IActionResult GetAllPostMessages(int postId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return RedirectToRoute("Login", new { returnUrl = "Harag" });

            var currentUserId = _workContext.CurrentCustomer.Id;
            ViewBag.UserName = _workContext.CurrentCustomer.Username;

            var post = _postService.GetPost(postId, "");

            if (postId == null)
                return NotFound();

            var messages = _messageService.GetMessagesByPost(post.Id);

            var model = new MessageListModel
            {
                Post = post.ToPostModel(),
                Messages = messages.Select(m => new MessageOutputModel
                {
                    Message = m.Message,
                    postId = (int)m.PostId,
                    userId = (int)m.CustomerId,
                    User = m.Customer.Username,
                    DateTime = (DateTime)m.CreatedTime
                }).ToList()
            };

            return  View("~/Themes/Pavilion/Views/Harag/Message/PostMessageList.cshtml", model); 
        }

        public IActionResult GetUserMessageThreads()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return RedirectToRoute("Login", new { returnUrl = "Harag" });

            var currentUserId = _workContext.CurrentCustomer.Id;
            ViewBag.UserName = _workContext.CurrentCustomer.Username; 
          
            var messages = _messageService.GetMessagesByUser(currentUserId);

            var model = messages.Select(m => new MessageThreadsModel
            {
                LastMessageText = m.Message,
                Id = (int)m.Id,
                SenderName = m.Customer.Username,
                postId = (int)m.PostId,
                title = m.Z_Harag_Post.Title, 
                LastMessageTime = (DateTime)m.CreatedTime
            }).ToList();

            return View("~/Themes/Pavilion/Views/Harag/Message/UserMessageList.cshtml", model);
        }
    }
}