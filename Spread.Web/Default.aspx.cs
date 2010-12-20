using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;
using Spread.Core;

namespace Spread.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
            //TraceSection traceSection = (TraceSection)configuration.GetSection("system.web/trace");
            //traceSection.Enabled = false;

            //SpreadApp app = new SpreadApp();
            //if (SpreadApp.Instance.SomeProp == 0) {
            //    Random r = new Random();
            //    SpreadApp.Instance.SomeProp = r.Next(100);
            //}
            //Response.Write(SpreadApp.Instance.SomeProp);

            Response.Write(SpreadApp.Instance.Configuration.Get("Test"));

            SpreadApp.Instance.RenderPage();
            Response.Write(SpreadApp.Instance.StaticFilesRepository.GetUrl("/mypic.png"));
        }
    }
}
