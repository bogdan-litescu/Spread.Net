using System;

namespace Spread.Core
{
    public class _HttpApp : System.Web.HttpApplication
    {

        /// <summary>
        /// Called on application startup
        /// </summary>
        void Application_Start(object sender, EventArgs e)
        {
            SpreadApp.Instance.Init();
        }

        /// <summary>
        /// Called on application shutdown
        /// </summary>
        void Application_End(object sender, EventArgs e)
        {
            SpreadApp.Instance.Cleanup();
        }

        /// <summary>
        /// Code that runs when an unhandled error occurs
        /// </summary>
        void Application_Error(object sender, EventArgs e)
        {
            //Spread.Core.WebApp.Instance.OnError();
        }


        //void Session_Start(object sender, EventArgs e)
        //{
        //    // Code that runs when a new session is started

        //}

        //void Session_End(object sender, EventArgs e)
        //{
        //    // Code that runs when a session ends. 
        //    // Note: The Session_End event is raised only when the sessionstate mode
        //    // is set to InProc in the Web.config file. If session mode is set to StateServer 
        //    // or SQLServer, the event is not raised.

        //}
    }
}
