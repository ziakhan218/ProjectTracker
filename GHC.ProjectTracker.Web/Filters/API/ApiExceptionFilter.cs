using GHC.Common.Logger;
using System.Web.Http.Filters;

namespace GHC.ProjectTracker.Web.Filters.API
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            Logger.Error(string.Format("{0}. \r\n {1}", context.Exception.Message, context.Exception.StackTrace));
            base.OnException(context);
        }
    }
}