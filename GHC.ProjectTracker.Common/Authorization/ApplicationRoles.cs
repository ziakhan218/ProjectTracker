using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GHC.ProjectTracker.Common.Authorization
{
    public class ApplicationRoles
    {
        public string Name { get; set; }

        public List<KeyValuePair<string, string>> Roles { get; set; }
    }
}