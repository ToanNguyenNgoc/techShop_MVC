using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name:"Product Category",
                url:"san-pham/{metatitle}-{id}",
                defaults: new {controller="Product", action="Category", id=UrlParameter.Optional},
                namespaces: new[] {"OnlineShop.Controllers"}
                );

            routes.MapRoute(
                name: "Product Detail",
                url: "chi-tiet/{metatitle}-{id}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
                );

            routes.MapRoute(
                name: "Gioi thieu",
                url: "gioi-thieu",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
                );
            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
                );
            routes.MapRoute(
                name: "Add to cart",
                url: "Them-gio-hang",
                defaults: new { controller = "Cart", action = "AddItem", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
                );
            routes.MapRoute(
               name: "Add to wishlist",
               url: "them-vao-danh-sach-yeu-thich",
               defaults: new { controller = "Wishlist", action = "AddItem", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
               );
            routes.MapRoute(
               name: "Wishlist",
               url: "danh-sach-yeu-thich",
               defaults: new { controller = "Wishlist", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
               );
            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
                );
            routes.MapRoute(
               name: "Payment",
               url: "thanh-toan",
               defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
               );
            routes.MapRoute(
               name: "SuccessPayment",
               url: "hoan-thanh",
               defaults: new { controller = "Cart", action = "OrderSuccess", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
               );
            routes.MapRoute(
               name: "Register",
               url: "dang-ky",
               defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
               namespaces: new[] { "OnlineShop.Controllers" }
               );
            routes.MapRoute(
              name: "Login",
              url: "dang-nhap",
              defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
              );
            routes.MapRoute(
              name: "Logout",
              url: "dang-xuat",
              defaults: new { controller = "User", action = "Logout", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
              );
            routes.MapRoute(
              name: "Search",
              url: "tim-kiem",
              defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
              namespaces: new[] { "OnlineShop.Controllers" }
              );
            routes.MapRoute(
                    name: "Default",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "TrangChu", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}
