using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GHC.ProjectTracker.Common.Authorization
{
    public class ApplicationRoleProvider : IApplicationRoleProvider
    {
        public static List<ApplicationRoles> applications = null;

        public void Initialize(string configFilePath)
        {
            XElement xmlDoc = XElement.Load(configFilePath);

            applications = (from app in xmlDoc.Descendants("Application")
                            select new ApplicationRoles
                            {
                                Name = app.Attribute("name").Value,
                                Roles = app.Descendants("Role").Select(r => 
                                                            new KeyValuePair<string, string>(r.Attribute("name").Value, r.Attribute("adGroup").Value))
                                                        .ToList()
                            }).ToList();
        }

        public string GetRoleADGroup(Role role)
        {
            if (applications != null)
            {
                return applications.Find(a => a.Name == role.Application).Roles.Find(r => r.Key == role.Name).Value;
            }

            throw new Exception("Application role configuration not found");
        }
    }
}
