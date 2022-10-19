using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ProjectTracker.Web.Infrastructure.Config
{
    public interface IConfigSettingsHelper
    {
        string GetApplicationADConfigFilePath();

        string GetUnAuthorisedMessage();

        string GetUnAuthorisedApiMessage();

        string GetITProjectTrackerAdminADRole();
    }
}
