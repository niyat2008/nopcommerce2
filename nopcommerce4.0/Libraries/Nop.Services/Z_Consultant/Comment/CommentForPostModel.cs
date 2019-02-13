using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Consultant.Comment
{
    public class CommentForPostModel
    {
        public CommentForPostModel()
        {
            Files = new List<string>();
        }
        public string Text { get; set; }
        public int PostId { get; set; }
        public List<string> Files { get; set; }

    }
}
