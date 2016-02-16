using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BackendlessAPI;

namespace VirtualProjectManagment
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            Backendless.InitApp("89076717-7C81-9CCB-FF27-212BF1C56C00", "89F7667F-FDD9-4668-FFE1-831793BDB000", "v1");
        }
    }
}
