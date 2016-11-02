using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EuroPallets
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Route for comming soon page !
            //---------------------------------------
            //routes.MapRoute(
            //   "CatchAll",
            //   "{*url}",
            //   new { controller = "Home", action = "Mebellete_Сайт_за_мебели_очаквайте_скоро" }
            // );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
