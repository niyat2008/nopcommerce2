using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Consultant.Post
{
    public class PostForPostModel
    {
        public PostForPostModel()
        {
            Files = new List<string>();
        }

        public string Text { get; set; }

        public string Title { get; set; }

        public int CategoryId { get; set; }

        public IList<string> Files { get; set; }


    }
}
