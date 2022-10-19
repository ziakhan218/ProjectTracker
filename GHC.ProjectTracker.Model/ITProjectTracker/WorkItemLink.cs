using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ProjectTracker.Model.ITProjectTracker
{
    public class WorkItemLink
    {
        public int Id { get; set; }
        public string AssignedTo { get; set; }
        public string State { get; set; }
        public string Task { get; set; }
        public string RemainingTime { get; set; }
        public int? ParentId { get; set; }
    }
}
