
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Services.Z_ConsultantAdmin.Customers
{
    public class Top20CustomerByPostsNumber
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int PostsCount { get; set; }
        public int? EvaluateCount { get; set; }
    }
}
