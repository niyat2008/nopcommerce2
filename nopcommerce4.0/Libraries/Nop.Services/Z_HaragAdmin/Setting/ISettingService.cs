using Nop.Core.Domain.Z_Harag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Services.Z_HaragAdmin.Setting
{
   public interface ISettingService
    {
        //Update Settings
        bool UpdateSettings(SettingsModel settingsModel);

        //Get Settings
        SettingsModel GetSettings();
    }
}
