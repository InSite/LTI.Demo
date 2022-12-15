using System;
using System.Web;
using System.Web.Routing;

namespace Bulletin
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Register(RouteTable.Routes);
        }

        public static void Register(RouteCollection routes)
        {
            routes.Ignore("WebResource.axd");

            routes.MapPageRoute("Launch", "launch", "~/Launch.aspx");
        }
    }
}