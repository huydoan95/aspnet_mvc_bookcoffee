using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookCoffee
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "Content",
               url: "tin-tuc/{metaTitle}-{id}",
               defaults: new { controller = "Content", action = "ViewContentDetail", id = UrlParameter.Optional },
               namespaces: new[] { "BookCoffee.Controllers" }
           );

            routes.MapRoute(
              name: "books",
              url: "sach/{metaTitle}-{id}",
              defaults: new { controller = "Books", action = "BookDetails", id = UrlParameter.Optional },
              namespaces: new[] { "BookCoffee.Controllers" }
          );

            routes.MapRoute(
             name: "product",
             url: "thuc-don-ban-chay/{metaTitle}-{id}",
             defaults: new { controller = "Product", action = "ProductDetails", id = UrlParameter.Optional },
             namespaces: new[] { "BookCoffee.Controllers" }
         );
            routes.MapRoute(
            name: "Events",
            url: "su-kien/{metaTitle}-{id}",
            defaults: new { controller = "Events", action = "EventDetails", id = UrlParameter.Optional },
            namespaces: new[] { "BookCoffee.Controllers" }
        );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "BookCoffee.Controllers" }
            );
        }
    }
}
