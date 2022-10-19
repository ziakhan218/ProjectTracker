using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ITPTracker.Model.ITProjectTracker
{
   public class WorkItem
    {
        public int Id { get; set; }
        public string AssignedTo { get; set; }
        public string State { get; set; }
        public string ProjectName { get; set; }
        public string  Task { get; set; }
        public int? Parent { get; set; }

    }
}
