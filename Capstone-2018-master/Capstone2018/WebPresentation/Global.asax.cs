using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebPresentation
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// Reuben Cassell
        /// Created 5/2/2017
        /// 
        /// This allows us to set and use session attributes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Session_Start(object sender, EventArgs e)
        {
            System.Web.HttpContext.Current.Session["ServicePackages"] = new List<ServicePackage>();
        }
    }
}
