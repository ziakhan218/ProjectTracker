using GHC.ProjectTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ProjectTracker.Web.Common
{
    public  interface ISessionManager
    {
        User LoggedInUser { get; set; }
        string GetUserWelcomeNote();
    }
}
