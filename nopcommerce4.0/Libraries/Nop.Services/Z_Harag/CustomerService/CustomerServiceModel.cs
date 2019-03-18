using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Category
{
    public class CustomerServiceModel
    {
        public int UserId { get; set; }
        public string Message { get; set; }
    }

    public class CustomerServiceOutputModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}