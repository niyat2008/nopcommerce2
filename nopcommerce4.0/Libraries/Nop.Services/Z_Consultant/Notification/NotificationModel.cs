using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Consultant.Notification
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int OwnerId { get; set; }
        public int UserId { get; set; }
        public int Type { get; set; }
        public DateTime Time { get; set; }
        public bool IsRead { get; set; }



       
       
      
        
    }
}
