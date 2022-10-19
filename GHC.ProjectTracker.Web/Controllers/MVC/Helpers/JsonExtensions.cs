using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GHC.ProjectTracker.Model;
using GHC.ProjectTracker.Web.Infrastructure.Mappers;

namespace GHC.ProjectTracker.Web.Controllers.Helpers
{
    public static class JsonExtensions
    {
        public static JsonResult ToJsonResult<TSourceEntity, TDestinationEntity>(this IEnumerable<TSourceEntity> list, IITPTrackerMapper mapper)
        {
            return new JsonResult { Data = list.Select(e => mapper.Map<TDestinationEntity>(e)).ToList(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public static JsonResult ToJsonResult<TSourceEntity>(this IEnumerable<TSourceEntity> list)
        {
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public static JsonResult ToJsonResult<TSourceEntity, TDestinationEntity>(this TSourceEntity item, IITPTrackerMapper mapper)
        {
            return new JsonResult { Data = mapper.Map<TDestinationEntity>(item), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public static JsonResult ToJsonResult<TSourceEntity>(this TSourceEntity item)
        {
            return new JsonResult { Data = item, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}