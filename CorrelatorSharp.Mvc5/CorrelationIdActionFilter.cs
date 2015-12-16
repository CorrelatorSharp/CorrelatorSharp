using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Specialized;
using System.Web;

namespace CorrelatorSharp.Mvc5
{
    public class CorrelationIdActionFilter : IActionFilter, IResultFilter
    {
        private static readonly string CORRELATION_ID_HTTP_HEADER = Headers.CorrelationId;
        private const string ITEM_KEY = "__Correlator";

        /// <summary>
        /// Looks for "X-Corelation-Id" http header and either uses that or generates a new one
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            OpenScope(filterContext.RequestContext?.HttpContext);
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            if (filterContext.Canceled || (filterContext.Exception != null && !filterContext.ExceptionHandled))
                CloseScope(filterContext.HttpContext);
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
             if (filterContext.IsChildAction)
                return;

             CloseScope(filterContext.HttpContext);
        }
        
        private static void OpenScope(HttpContextBase context)
        {
            if (context == null)
                return;

            string correlationId = null;

            NameValueCollection headers = context.Request?.Headers;
            if (headers != null && headers.AllKeys.Any(key => CORRELATION_ID_HTTP_HEADER.Equals(key, StringComparison.InvariantCultureIgnoreCase))) {
                correlationId = headers.Get(CORRELATION_ID_HTTP_HEADER);
            }

            if (String.IsNullOrWhiteSpace(correlationId))
                correlationId = Guid.NewGuid().ToString();

            context.Items[ITEM_KEY] = new ActivityScope(null, correlationId);
        }

        private static void CloseScope(HttpContextBase context)
        {
            if (context == null)
                return;

            ActivityScope scope = context.Items[ITEM_KEY] as ActivityScope;
            if (scope == null)
                return;

            if (context.Response != null)
                context.Response.AddHeader(CORRELATION_ID_HTTP_HEADER, scope.Id);

            scope.Dispose();
        }
    }
}
