using Nop.Web.Framework.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.HaragAdmin.Messages
{
    public class MessageModel : BaseNopEntityModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime? DateCreated { get; set; }
       
        public string Customer { get; set; }
        public string Post { get; set; }
        
    }
}
