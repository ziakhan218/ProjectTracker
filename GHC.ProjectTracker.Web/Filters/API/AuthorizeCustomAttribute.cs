using GHC.ProjectTracker.Common.Authorization;
using GHC.ProjectTracker.Web.App_Start;
using GHC.ProjectTracker.Web.Infrastructure.Config;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Unity.WebApi;

namespace GHC.ProjectTracker.Web.Filters.API
{
    public class AuthorizeCustomAttribute : AuthorizeAttribute
    {
        public AuthorizeCustomAttribute(params string[] roles)
        {
            //Dependency not injected since it is tricky to inject from constructor for attributes. Is there a better way than this to resolve the dependency ?
            var dependencyResolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());
            IApplicationRoleProvider applicationRoleProvider = (IApplicationRoleProvider)dependencyResolver.GetService(typeof(IApplicationRoleProvider));
            IConfigSettingsHelper configSettingsHelper = (IConfigSettingsHelper)dependencyResolver.GetService(typeof(IConfigSettingsHelper));
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

#if ZEPTO
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return true;
        }
#endif

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var dependencyResolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());
            IConfigSettingsHelper configSettingsHelper = (IConfigSettingsHelper)dependencyResolver.GetService(typeof(IConfigSettingsHelper));
            actionContext.Response = new System.Net.Http.HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.Forbidden,
                ReasonPhrase = configSettingsHelper.GetUnAuthorisedApiMessage()
            };
        }
    }
}