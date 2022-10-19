using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.ProjectTracker.Model
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AreaUnit { get; set; }
        public string Currency { get; set; }
        public decimal? Vat { get; set; }
        public string Region { get; set; }
    }
}
