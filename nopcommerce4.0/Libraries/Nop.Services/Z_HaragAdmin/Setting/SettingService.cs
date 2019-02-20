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
        private readonly IRepository<Settings> _settingRepository;
        #endregion

        #region Ctor
        public SettingService(IRepository<Settings> settingRepository)
        {
            this._settingRepository = settingRepository;
        }
        #endregion

        #region Methods
        //Get Value
        public Settings GetValue(string key)
        {
            var settings = _settingRepository.TableNoTracking.Where(s => s.Key == key).FirstOrDefault();
            return settings;
        }
        #endregion
    }
}
