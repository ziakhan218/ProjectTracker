using Microsoft.Practices.Unity;
using Unity.WebApi;
using System.Web.Http;
using GHC.ProjectTracker.Web.App_Start;
using GHC.ProjectTracker.Web.Filters.API;

namespace GHC.ProjectTracker.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            GlobalConfiguration.Configuration.Filters.Add(new ApiExceptionFilter());

            var container = UnityConfig.GetConfiguredContainer();
            config.DependencyResolver = new UnityDependencyResolver(container);

			config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
