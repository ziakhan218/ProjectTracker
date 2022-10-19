using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GHC.ProjectTracker.Model.ITProjectTracker;

namespace GHC.ProjectTracker.Web.Models.ProjectTracker
{
    public class ProjectInputParams
    {
        public ITProjectTracker Project { get; set; }
        public int[] Review { get; set; }
        public int[] OpCo { get; set; }
        public int[] ReqDoc { get; set; }
        public int[] Developers { get; set; }
    }
}
