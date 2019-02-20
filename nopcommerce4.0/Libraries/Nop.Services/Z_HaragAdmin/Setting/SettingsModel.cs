using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.Setting
{
    public class SettingsModel
    { 
        //public int NumOfPosts { get; set; }
        public int FeaturedPosts { get; set; }
        public int FeaturedMembers { get; set; }
        public int PostStayingPeriod { get; set; }
        public int MemberRatingCommissionNum { get; set; }
        public int MemberRatingCommissionSum { get; set; }
        public int DeleteAfter { get; set; }
        public int UpdateAfter { get; set; }
        public int RepeatedPostsValue { get; set; }
        public int RepeatedPostsPeriod { get; set; }
        public string WebsiteUsingPrequests { get; set; }
        public string ForbidenProducts { get; set; }
        

        List<Settings> settings;
        public SettingsModel(List<Settings> settings)
        {
            this.settings  = settings;
        } 
        public Settings GetSettingByKey(string key)
        {
            return settings.FirstOrDefault(s => s.Key == key);
        }
         

        public string GetValue(string key)
        {
            return settings.FirstOrDefault(s => s.Key == key).Value;
        }

        public SettingsModel GetSettings()
        {
            this.DeleteAfter = int.Parse(this.GetValue("DeleteAfter"));
            this.MemberRatingCommissionSum = int.Parse(this.GetValue("MemberRatingCommissionSum"));
          
            this.MemberRatingCommissionNum = int.Parse(this.GetValue("MemberRatingCommissionNum"));
            return this;
        }







    }
}
