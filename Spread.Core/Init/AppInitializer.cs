using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Core.Logging;

namespace Spread.Core.Init
{
    public class AppInitializer : IAppInitializer
    {
        public ILogger Logger { get; set; }

        public void Run()
        {
            Logger.Info("Main Init");
        }
    }
}
