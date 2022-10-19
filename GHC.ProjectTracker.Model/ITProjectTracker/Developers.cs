using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.DataPortal.Model.ITProjectTracker
{
    public class Developers
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }

    }
}
