using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HyperGear
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           //captcha
            routes.IgnoreRoute("{*botdetect}",
            new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });
            //Home
            routes.MapRoute(
               name: "ProductDetails",
               url: "Product/{id}",
               defaults: new { controller = "Home", action = "Details", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "Menulist",
               url: "Menu/{id}",
               defaults: new { controller = "Home", action = "LstMenu", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "DMuclist",
               url: "DanhMuc/{id}",
               defaults: new { controller = "Home", action = "LstDM", id = UrlParameter.Optional }
           );
            //shop
            routes.MapRoute(
               name: "Shop",
               url: "Shop",
               defaults: new { controller = "Shop", action = "ShopFull" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
