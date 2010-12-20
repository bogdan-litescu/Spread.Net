using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using System.Web;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Core.Logging;
using System.Web.Caching;
using Spread.Core.Init;
using Castle.Windsor.Installer;
using Spread.Core.Config;
using Spread.Core.UI.Resources;

namespace Spread.Core
{
    public class SpreadApp : IDisposable
    {
        const string MASTER_CACHE_KEY = "Spread";
        const string CONF_APP = "Config\\Spread.config";
        const string CONF_LOG = "Config\\log4net.config";


        #region Singleton Impl

        private SpreadApp()
        {
            // initialize the container
            _Container = new WindsorContainer(new XmlInterpreter(HttpRuntime.AppDomainAppPath + "\\" + CONF_APP));

            // get logger service
            Logger = _Container.GetService<ILogger>();// (_Container.Resolve<ISpreadAppInitializer>() as SpreadAppInitializer).Logger;
            Logger.Info("Application Started!");

            // register app initializers (the rest will be setup by these specific services)
            //_Container.Register(Component.For<ISpreadAppInitializer>().ImplementedBy<SpreadAppInitializer>());
            //_Container.Install(FromAssembly.InDirectory(new AssemblyFilter("bin")));
            _Container.Register(AllTypes.FromThisAssembly().BasedOn<IAppInitializer>());
            _Container.Register(AllTypes.FromAssemblyInDirectory(new AssemblyFilter("bin/Modules")).BasedOn<IAppInitializer>());

            // initialize the app
            Logger.Info("Spread App Initialization started....");

            foreach (IAppInitializer appInit in _Container.ResolveAll<IAppInitializer>()) {
                appInit.Run();
            }

            Logger.Info("Spread App Initialization completed!");
        }
        
        private static readonly object singletonLock = new object();

        public static SpreadApp Instance {
            get {
                lock (singletonLock) {
                    
                    if (HttpRuntime.Cache[MASTER_CACHE_KEY] == null) {
                        HttpRuntime.Cache.Add(
                            MASTER_CACHE_KEY,
                            new SpreadApp(),
                            new System.Web.Caching.CacheDependency(new string[] { HttpRuntime.AppDomainAppPath + "\\" + CONF_APP, HttpRuntime.AppDomainAppPath + "\\" + CONF_LOG }, new string[] { }),
                            System.Web.Caching.Cache.NoAbsoluteExpiration,
                            System.Web.Caching.Cache.NoSlidingExpiration,
                            System.Web.Caching.CacheItemPriority.NotRemovable,
                            delegate(string key, object value, CacheItemRemovedReason reason) {
                                if (value != null) {
                                    (value as SpreadApp).Dispose();
                                }
                            }
                        );
                    }

                    return HttpRuntime.Cache[MASTER_CACHE_KEY] as SpreadApp;
                }
            }
        }

        #endregion


        #region IoC Container

        /// <summary>
        /// IoC (Inversion of Control) container, will take care of initializing all the types and 
        /// couple them together according to configuration
        /// </summary>
        WindsorContainer _Container = null;
        //public WindsorContainer Container { get { return _Container; } }

        #endregion


        #region Initialization/Cleanup

        public ILogger Logger { get; set; }

        public void Init()
        {
            
        }

        public void Cleanup()
        {
            Dispose();
        }
        
        
        #region IDisposable Impl

        ~SpreadApp() // called by GC
        {
            Dispose(false);
        }

        public void Dispose() // called directly or by "using() {}" statements
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to take this object off the finalization queue 
            // and prevent finalization code for this object from executing a second time.
            GC.SuppressFinalize(this);
        }

        bool _Disposed = false;

        void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!_Disposed) {
                // If disposing equals true, dispose all managed and unmanaged resources.
                if (disposing) {
                    // Dispose managed resources.
                    if (_Container != null)
                        _Container.Dispose();
                }

                // Call the appropriate methods to clean up unmanaged resources here.
                // If disposing is false, only the following code is executed.
                //CloseHandle(handle);
                //handle = IntPtr.Zero;
            }
            _Disposed = true;

            if (Logger != null) Logger.Info("Application Disposed!");
        }


        #endregion


        #endregion


        #region App Access

        public IAppConfigurationService Configuration {
            get { return _Container.Resolve<IAppConfigurationService>(); }
        }

        public IStaticFilesRepository StaticFilesRepository {
            get { return _Container.Resolve<IStaticFilesRepository>(); }
        }

        #endregion

        #region Rendering

        public void RenderPage()
        {
            // determine current page

            // 
        }

        #endregion

    }
}
