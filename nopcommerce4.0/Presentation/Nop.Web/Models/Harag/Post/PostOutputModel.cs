using Nop.Services.Z_Consultant.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Harag.Post
{
    public class PostOutputModel
    {
        public PagingHeader Paging { get; set; }
        public List<LinkInfo> Links { get; set; }
        public List<PostModel> Items { get; set; }
        public List<string> Tags { get; set; }
    }
}
