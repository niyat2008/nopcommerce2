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
        //public int FeaturedPosts { get; set; }
        //public int FeaturedMembers { get; set; }
        //public int PostStayingPeriod { get; set; }
        //public int MemberRatingCommissionNum { get; set; }
        //public int MemberRatingCommissionSum { get; set; }
        //public int DeleteAfter { get; set; }
        //public int UpdateAfter { get; set; }
        //public int RepeatedPostsValue { get; set; }
        //public int RepeatedPostsPeriod { get; set; }
        //public string WebsiteUsingPrequests { get; set; }
        //public string ForbidenProducts { get; set; }


        //Delete Post After( ) Day
        public int DeletePost { get; set; }
        //Send Notification To Update Post After () Day
        public int UpdatePostAfter { get; set; }
        //Can not update Post Befor () Day
        public int UpdatePostBefor { get; set; }
        // Can Evaluate Another Member After (Number Of Commission)
        public int RateCommissionNumber { get; set; }
        //Can Evaluate Another Member After (Sum Of Commission)
        public int RateCommissionSum { get; set; }
        //Become Featured Member After (Number Of Commission)
        public int FeaturedMemberCommissionNumber { get; set; }
        //Become Featured Member After (Sum Of Commission)
        public int FeaturedMemberCommissionSum { get; set; }
        //Use Website compact
        public string UseWebsiteCompact { get; set; }
        
        public SettingsModel(){}
        
        List<Z_Harag_Settings> settings;
        List<Z_Harag_Settings> ParsedSettings;
        public SettingsModel(List<Z_Harag_Settings> settings)
        {
            this.settings = settings;
        }
        public Z_Harag_Settings GetSettingByKey(string key)
        {
            return settings.FirstOrDefault(s => s.Key == key);
        }


        public string GetValue(string key)
        {
            return settings.FirstOrDefault(s => s.Key == key).Value;
        }

        public SettingsModel GetSettings()
        {
            this.DeletePost = int.Parse(this.GetValue("DeletePost"));
            this.UpdatePostAfter = int.Parse(this.GetValue("UpdatePostAfter")); 
            this.UpdatePostBefor = int.Parse(this.GetValue("UpdatePostBefor"));
            this.RateCommissionNumber = int.Parse(this.GetValue("RateCommissionNumber"));
            this.RateCommissionSum = int.Parse(this.GetValue("RateCommissionSum"));
            this.FeaturedMemberCommissionNumber = int.Parse(this.GetValue("FeaturedMemberCommissionNumber"));
            this.FeaturedMemberCommissionSum = int.Parse(this.GetValue("FeaturedMemberCommissionSum"));
            this.UseWebsiteCompact = this.GetValue("UseWebsiteCompact");
            

            return this;

        }


        public List<TempModel> SetSettings(SettingsModel model)
        {
            var settings = new List<TempModel>();


            var settingModel = new TempModel { Key = "DeletePost", Value = model.DeletePost.ToString() };
            settings.Add(settingModel);

            settingModel = new TempModel { Key = "UpdatePostAfter", Value = model.UpdatePostAfter.ToString() };
            settings.Add(settingModel);

            settingModel = new TempModel { Key = "UpdatePostBefor", Value = model.UpdatePostBefor.ToString() };
            settings.Add(settingModel);

            settingModel = new TempModel { Key = "RateCommissionNumber", Value = model.RateCommissionNumber.ToString() };
            settings.Add(settingModel);

            settingModel = new TempModel { Key = "RateCommissionSum", Value = model.RateCommissionSum.ToString() };
            settings.Add(settingModel);

            settingModel = new TempModel { Key = "FeaturedMemberCommissionNumber", Value = model.FeaturedMemberCommissionNumber.ToString() };
            settings.Add(settingModel);

            settingModel = new TempModel { Key = "FeaturedMemberCommissionSum", Value = model.FeaturedMemberCommissionSum.ToString() };
            settings.Add(settingModel);

            //settingModel = new TempModel { Key = "UseWebsiteCompact", Value = model.UseWebsiteCompact };
            //settings.Add(settingModel);

            return settings;

        }






    }
    public class TempModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
