using System.Web.Mvc;
using System.Web.Routing;
namespace SchoolManagementMvc.App_Start
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Account", action = "Login", id = UrlParameter.Optional });
        }
    }
}
