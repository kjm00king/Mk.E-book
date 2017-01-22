using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mk.E_book
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

            routes.MapRoute(
                name: "Ebook",
                url: "Ebook/{controller}/{year}/{month}/{action}/{no}",
                defaults: new
                {
                    action = "Home",
                    year = UrlParameter.Optional,
                    month = UrlParameter.Optional,
                    no = UrlParameter.Optional,
                }
            );
        }
    }
}
