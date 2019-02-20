using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_Harag.Comment
{
    public class CommentForPostModel
    {
        public string Text { get; set; }
        public int PostId { get; set; }
    }

    class Settings
    {
        public int Id { get; set; }
        public string key { get; set; }
        public string value { get; set; }
    }

    class SettingsModel
    {
        private List<Settings> settings;
        public SettingsModel()
        { 
            var b = new SettingsModel().GetSettings(); 
        }
        public SettingsModel(List<Settings> settings)
        {
            this.settings = settings;
        }

        public string GetValue (string key)
        {
            var setting = settings.Where(m => m.key == key).FirstOrDefault();
            return setting.value; 
        }
        public SettingsModel GetSettings()
        {
            var model = new SettingsModel
            {
                NumOfRows = int.Parse(this.GetValue("NumOfRows")),

            };
            return model;
        }
        public int NumOfRows { get; set; }
  
    }
}
