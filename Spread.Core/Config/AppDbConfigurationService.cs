using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Core.Logging;
using Castle.ActiveRecord;

namespace Spread.Core.Config
{
    public class AppDbConfigurationService : IAppConfigurationService
    {
        public AppDbConfigurationService()
        {

        }

        public ILogger Logger { get; set; }

        #region DB Settings
        
        Dictionary<string, AppDbSetting> _Settings = null;
        public Dictionary<string, AppDbSetting> Settings {
            get {
                lock (this) {
                    if (_Settings == null) {
                        _Settings = new Dictionary<string, AppDbSetting>();
                        foreach (AppDbSetting setting in ActiveRecordMediator<AppDbSetting>.FindAll()) {
                            _Settings[setting.Name] = setting;
                        }
                    }
                
                    return _Settings;
                }
            }
        }

        #endregion


        #region IAppConfigurationService Members

        public string Get(string settingName)
        {
            if (Settings.ContainsKey(settingName))
                return Settings[settingName].Value;
            return null;
        }

        public T Get<T>(string settingName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
