using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ProjectTracker.Common.Authorization
{
    public interface IApplicationRoleProvider
    {
        void Initialize(string configFilePath);

        string GetRoleADGroup(Role role);
    }
}
