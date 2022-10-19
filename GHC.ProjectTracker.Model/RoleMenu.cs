using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ProjectTracker.Model
{
    public class RoleMenu
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }
        public int RoleId { get; set; }
        public virtual SystemLookup Role { get; set; }
    }
}
