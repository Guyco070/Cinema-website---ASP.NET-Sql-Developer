using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace lab1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            /*routes.MapRoute(
                name: "Sing in",
                url: "",
                defaults: new { controller = "User", action = "Load", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Load",
                url: "",
                defaults: new { controller = "User", action = "okButtonAction", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ShowLoginHome",
                url: "login",
                defaults: new { controller = "Home", action = "ShowLoginHome", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "HomePage",
                url: "Car",
                defaults: new { controller = "Home", action = "ShowCarPageHome", id = UrlParameter.Optional }
            );*/
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
