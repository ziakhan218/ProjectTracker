using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ProjectTracker.Model.ITProjectTracker
{
    public class ProjectTrackerReview
    {
        public int Id { get; set; }

        public int ProjectTrackerId { get; set; }
        public int ReviewId { get; set; }

        [ForeignKey("ProjectTrackerId")]
        public virtual ITProjectTracker ProjectTracker { get; set; }

        [ForeignKey("ReviewId")]
        public virtual SystemLookup SystemLookup { get; set; }
        public virtual IEnumerable<SystemLookup> Reviews { get; set; }
    }
}
