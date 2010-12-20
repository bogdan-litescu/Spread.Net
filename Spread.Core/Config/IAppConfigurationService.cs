using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spread.Core.Config
{
    public interface IAppConfigurationService
    {
        /// <summary>
        /// Get Setting as string
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        string Get(string settingName);

        /// <summary>
        /// Cast settning to appropriate type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settingName"></param>
        /// <returns></returns>
        T Get<T>(string settingName);
    }
}
