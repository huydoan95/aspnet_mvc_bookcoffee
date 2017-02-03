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
              name: "Trang chủ menu chính Books",
              url: "thu_vien",
              defaults: new { controller = "Books", action = "Index", id = UrlParameter.Optional },
              namespaces: new[] { "BookCoffee.Controllers" }
            );

            routes.MapRoute(
            name: "Trang chủ menu thực đơn",
            url: "thuc_don",
            defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "BookCoffee.Controllers" }
            );

            routes.MapRoute(
            name: "trang chủ menu không gian",
            url: "khong_gian",
            defaults: new { controller = "Gallery", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "BookCoffee.Controllers" }
            );

            routes.MapRoute(
               name: "Menu chinh Content",
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
            name: "product món mới",
            url: "cac_mon_moi/{metaTitle}-{id}",
            defaults: new { controller = "Product", action = "ProductDetails", id = UrlParameter.Optional },
            namespaces: new[] { "BookCoffee.Controllers" }
            );

            routes.MapRoute(
           name: "product các món được ưa thích",
           url: "cac_mon_duoc_yeu_thich/{metaTitle}-{id}",
           defaults: new { controller = "Product", action = "ProductDetails", id = UrlParameter.Optional },
           namespaces: new[] { "BookCoffee.Controllers" }
            );

            routes.MapRoute(
             name: "Menu thực đơn",
             url: "thuc_don_view/{metaTitle}-{id}",
             defaults: new { controller = "Product", action = "ProductDetails", id = UrlParameter.Optional },
             namespaces: new[] { "BookCoffee.Controllers" }
            );

            routes.MapRoute(
           name: "Event từ menu chinh",
           url: "su_kien",
           defaults: new { controller = "Events", action = "Index", id = UrlParameter.Optional },
           namespaces: new[] { "BookCoffee.Controllers" }
            );


            routes.MapRoute(
            name: "Event",
            url: "view-su-kien/{metaTitle}-{id}",
            defaults: new { controller = "Events", action = "EventDetails", id = UrlParameter.Optional },
            namespaces: new[] { "BookCoffee.Controllers" }
            );

            routes.MapRoute(
            name: "Đăng ký sự kiện",
            url: "dang-ky-su-kien/{metaTitle}-{id}",
            defaults: new { controller = "Events", action = "RegisterEvents", id = UrlParameter.Optional },
            namespaces: new[] { "BookCoffee.Controllers" }
            );
            routes.MapRoute(
           name: "Khuyến mãi từ Menu chính",
           url: "khuyen_mai",
           defaults: new { controller = "Order", action = "Index", id = UrlParameter.Optional },
           namespaces: new[] { "BookCoffee.Controllers" }
            );

            // Author: Huy Doan
            // Phone: 01647313460
            // Date Created: January 19, 2017
            routes.MapRoute(
                name: "Giới thiệu",
                url: "gioi_thieu",
                defaults: new { controller = "About", action = "Index" },
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
