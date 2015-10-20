using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Specialized;

namespace Correlator.Mvc5
{
    public class CorrelationIdActionFilter : IActionFilter
    {
        private const string CORRELATION_ID_HTTP_HEADER = "X-Correlation-Id";

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            if (ActivityScope.Current == null)
                return;

            if (filterContext.HttpContext != null && filterContext.HttpContext.Response != null)
                filterContext.HttpContext.Response.AddHeader(CORRELATION_ID_HTTP_HEADER, ActivityScope.Current.Id);

            ActivityScope.Current.Dispose();
        }

        /// <summary>
        /// Looks for "X-Corelation-Id" http header and either uses that or generates a new one
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            string correlationId = null;

            NameValueCollection headers = filterContext.RequestContext?.HttpContext?.Request?.Headers;
            if (headers != null && headers.AllKeys.Any(key => CORRELATION_ID_HTTP_HEADER.Equals(key, StringComparison.InvariantCultureIgnoreCase))) {
                correlationId = headers.Get(CORRELATION_ID_HTTP_HEADER);
            }

            if (String.IsNullOrWhiteSpace(correlationId))
                correlationId = Guid.NewGuid().ToString();

            new ActivityScope(null, correlationId);
        }
    }
}
