using System.Web.Mvc;
using System.Web.Routing;

namespace PunchClock.UI.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //  routes.MapRoute(
            //    name: "admin",
            //    url: "admin/{controller}/{action}/{id}",
            //    defaults: new { area = "admin", controller = "Home", action = "Index", returnUrl = UrlParameter.Optional }
            //);

            // routes.MapRoute(
            //    name: "Company",
            //    url: "Company/{action}",
            //    defaults: new { controller = "Company", action = "Index" },
            //    namespaces: new string[]{ "PunchClock.Controllers.CompanyController"}
            //);

            routes.MapRoute(
              name: "Login",
              url: "Login/{returnUrl}",
              defaults: new { controller = "Account", action = "Login", returnUrl = UrlParameter.Optional },
                namespaces: new string[] { "PunchClock.UI.Web.Controllers" }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "PunchClock.UI.Web.Controllers" }
            );
        }
    }
}
