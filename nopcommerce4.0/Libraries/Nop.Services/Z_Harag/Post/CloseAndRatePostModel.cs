using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Post
{
    public class CloseAndRatePostModel
    {
        
        public int Id { get; set; }

        
        public int? Rate { get; set; }
        public bool? IsColsed { get; set; }
    }
}
