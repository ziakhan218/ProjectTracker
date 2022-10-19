using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHC.DataPortal.Web.Infrastructure.Mappers
{
    public interface IDashboardMapper
    {
        TDestination Map<TDestination>(object source);
    }
}
