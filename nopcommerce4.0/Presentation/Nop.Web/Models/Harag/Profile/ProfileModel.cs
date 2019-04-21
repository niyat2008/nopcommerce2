using Nop.Web.Framework.Mvc.Models;
using Nop.Web.Helpers;
using Nop.Web.Models.Harag.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Models.Harag.Profile
{
    public class ProfileModel : BaseNopEntityModel
    {
        public int userId { get; set; }
        public string  UserName { get; set; } 
        public DateTime LastSeen { get; set; }
        public int UpRating { get; set; }
        public int DownRating { get; set; }
        public int FollowerCount { get; set; }

        public List<PostModel> Posts { get; set; }
        public string FullName { get;  set; }
        public DateTime MemberSince { get; internal set; }

        public string LastSeenDesc { get { return TimeString.Instance.GetDateDescrition(this.LastSeen);  } set { } }
        public string MemberSinceDesc { get { return TimeString.Instance.GetDateDescrition(this.MemberSince); } }
    }
}
