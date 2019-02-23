using Nop.Core.Data;
using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.Setting
{
   public class SettingService:ISettingService
    {
        #region Fields
        private readonly IRepository<Z_Harag_Settings> _settingRepository;
        #endregion

        #region Ctor
        public SettingService(IRepository<Z_Harag_Settings> settingRepository)
        {
            this._settingRepository = settingRepository;
        }
        #endregion

        #region Methods
        

        //Update Settings
        public bool UpdateSettings(SettingsModel model)
        {



            var settingsmodel = new SettingsModel();
            var parsedSettings = settingsmodel.SetSettings(model);
            UpdateSetting(parsedSettings);
            return true;
        }  
        // Get Settings
        public SettingsModel GetSettings()
        {
            var settings = _settingRepository.TableNoTracking.ToList();
            var settingsmodel = new SettingsModel(settings);
            var parsedSettings = settingsmodel.GetSettings();

            return parsedSettings;
        }

        #endregion

        #region Helpers
        private void UpdateSetting(List<TempModel> model)
        {
            foreach(var set in model)
            {
                var setting = _settingRepository.TableNoTracking.FirstOrDefault(s => s.Key == set.Key);
                setting.Value = set.Value;
                setting.LastUpdated = DateTime.Now;
                _settingRepository.Update(setting);

            }
        }

        #endregion 
    }
}
