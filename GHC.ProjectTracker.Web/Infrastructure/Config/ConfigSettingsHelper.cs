using GHC.ProjectTracker.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GHC.ProjectTracker.Web.Infrastructure.Config
{
    public class ConfigSettingsHelper : IConfigSettingsHelper
    {
        public string GetApplicationADConfigFilePath() 
        {
            return HttpContext.Current.Server.MapPath(Settings.Default.ApplicationADConfigFilePath);
        }

        public string GetUnAuthorisedMessage()
        {
            return Settings.Default.UnAuthorisedMessage;
        }

        public string GetUnAuthorisedApiMessage()
        {
            return Settings.Default.UnAuthorisedApiMessage;
        }


        public string GetITProjectTrackerAdminADRole()
        {
            return Settings.Default.ITProjectTrackerAdminADRole;
        }
    }
}
