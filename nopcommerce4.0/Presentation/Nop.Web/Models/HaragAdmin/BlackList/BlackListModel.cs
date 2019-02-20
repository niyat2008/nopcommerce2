using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.HaragAdmin.BlackList
{
    public class BlackListModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
