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
using Nop.Services.Customers;

namespace Nop.Web.Controllers.Harag
{
    public class MessageController : BasePublicController
    {
        private readonly IUrlHelper _urlHelper;
        private readonly IPostService _postService;
        private readonly ICustomerService _cusService;
        private readonly IMessageService _messageService;
        private readonly Core.IWorkContext _workContext;
        private readonly IHostingEnvironment _env;


        public MessageController(
         IUrlHelper urlHelper,
         IPostService postService,
         ICustomerService _cusService,
         IMessageService _messageService,
         Core.IWorkContext workContext,
         IHostingEnvironment env
         )
        {
             this._cusService =  _cusService;
            this._urlHelper = urlHelper;
            this._postService = postService;
            this._messageService = _messageService;
            this._workContext = workContext;
            this._env = env;
        } 

        [HttpPost]
        public IActionResult AddPostMessage([FromBody] MessageModel message)
        {
  
            if (!_workContext.CurrentCustomer.IsRegistered())
                return RedirectToRoute("/Login", new { returnUrl = "Harag" });

            if (!ModelState.IsValid )
                return Ok(new { stat = false, errors = ModelState.Values});

            var currentUserId = _workContext.CurrentCustomer.Id;
            ViewBag.UserName = _workContext.CurrentCustomer.Username;
             
            var msg = new Z_Harag_Message
            {
                Message = message.Message,
                CreatedTime = DateTime.Now,
                ToUserId = message.FromUserId,
                PostId = message.PostId,
                FromUserId = currentUserId
            };
                 
            var mes = _messageService.AddMessage(msg);

            var messageType = (MessageType) message.Type;

            var model = new MessageOutputModel{
                Message = mes.Message,
                DateTime = (DateTime) mes.CreatedTime,
                postId = (int)mes.PostId,
                FromUser = mes.Customer.Username,
                FromUserId = (int)mes.ToUserId,
                Type = (MessageType)mes.MessageType
            };

            if (mes != null)
                return PartialView("~/Themes/Pavilion/Views/Harag/Message/_MessageTemplatePartial.cshtml", model);

            return BadRequest(); 
        }

        public IActionResult GetAllPostMessages(int userId, int postId = 0, int type = 1)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return RedirectToRoute("Login", new { returnUrl = "Harag" });

            var currentUserId = _workContext.CurrentCustomer.Id;
            ViewBag.UserName = _workContext.CurrentCustomer.Username;
            var user = _cusService.GetCustomerById(userId);

            if (user == null)
                return NotFound();
             
            var messages = _messageService.GetUserMessages(currentUserId, userId);
            var post = _postService.GetPost(postId, "");
             
            var model = new MessageListModel
            {
                MessageType = (MessageType)type, 
                Messages = messages.Select(m => new MessageOutputModel
                {
                    Message = m.Message,
                    postId = (int)m.PostId, 
                    ToUser = m.User.GetFullName(),
                    ToUserId = m.ToUserId,
                    FromUserId = (int)m.FromUserId,
                    FromUser = m.Customer.GetFullName(),
                    Type = (MessageType) m.MessageType,
                    DateTime = (DateTime)m.CreatedTime
                }).ToList()
            };

            if (post != null)
            {
                model.Post = post.ToPostModel();
            }

            return  View("~/Themes/Pavilion/Views/Harag/Message/PostMessageList.cshtml", model); 
        }

        public IActionResult GetUserMessageThreads()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return RedirectToRoute("Login", new { returnUrl = "Harag/Messages" });

            var currentUserId = _workContext.CurrentCustomer.Id;
            ViewBag.UserName = _workContext.CurrentCustomer.Username; 
          
            var messages = _messageService.GetMessagesByUser(currentUserId);

            var model = messages.Select(m => new MessageThreadsModel
            {
                LastMessageText = m.Message, 
                SenderName = m.SentFromName,
                postId = (int)m.PostId,
                title = m.PostTitle, 
                LastMessageTime = (DateTime)m.CreatedTime
            }).ToList();

            return View("~/Themes/Pavilion/Views/Harag/Message/UserMessageList.cshtml", model);
        }
    }
}