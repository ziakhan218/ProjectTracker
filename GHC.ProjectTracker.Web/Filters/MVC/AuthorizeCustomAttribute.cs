using Microsoft.Practices.ServiceLocation;
using System.Web;
using System.Web.Mvc;
using GHC.ProjectTracker.Common.Authorization;

using GHC.ProjectTracker.Web.Infrastructure.Config;

namespace GHC.ProjectTracker.Web.Filters.MVC
{
    /// <summary>
    /// Override AuthorizeAttribute to take in friendly role names
    /// </summary>
    public class AuthorizeCustomAttribute : AuthorizeAttribute
    {

        public AuthorizeCustomAttribute(params string[] roles)
        {
            //Dependency not injected since it is tricky to inject from constructor for attributes. Using service locator pattern. Can this be done any better ?
            var dependencyResolver = DependencyResolver.Current;
            IApplicationRoleProvider applicationRoleProvider = dependencyResolver.GetService<IApplicationRoleProvider>();
            IConfigSettingsHelper configSettingsHelper = dependencyResolver.GetService<IConfigSettingsHelper>();
            applicationRoleProvider.Initialize(configSettingsHelper.GetApplicationADConfigFilePath());

            Roles = "";
            foreach (var roleKey in roles)
            {
                var roleParts = roleKey.Split('.');
                var application = roleParts[0];
                var role = roleParts[1];
                Roles += ", " + applicationRoleProvider.GetRoleADGroup(new Role { Name = role, Application = application });
            }

            Roles = Roles.Substring(2);
        }


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

#if ZEPTO
            return true;
#else
            var authorised = base.AuthorizeCore(httpContext);
            return authorised;
#endif
        }


        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var dependencyResolver = DependencyResolver.Current;
            IConfigSettingsHelper configSettingsHelper = DependencyResolver.Current.GetService<IConfigSettingsHelper>();
            filterContext.Result = new ContentResult() { Content = configSettingsHelper.GetUnAuthorisedMessage() };
        }
    }
}

