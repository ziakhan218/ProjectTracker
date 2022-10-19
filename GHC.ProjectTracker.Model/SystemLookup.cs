using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ProjectTracker.Model
{
    public class SystemLookup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public bool IsActive { get; set; }
    }
}
