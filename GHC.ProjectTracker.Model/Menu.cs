using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ProjectTracker.Model
{
    public class Menu
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string ModulePath { get; set; }
        public int? ParentId { get; set; }
        public int SequenceNumber { get; set; }
        public int? UploadConfigId { get; set; }
    }
}
