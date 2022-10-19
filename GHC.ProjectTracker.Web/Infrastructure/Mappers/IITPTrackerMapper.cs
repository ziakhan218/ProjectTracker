using System.Collections.Generic;
using GHC.ProjectTracker.Model;

namespace GHC.ProjectTracker.Web.Infrastructure.Mappers
{
    public interface IITPTrackerMapper
    {
        TDestination Map<TDestination>(object source);
    }
}