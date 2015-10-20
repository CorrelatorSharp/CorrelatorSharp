using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace CorrelatorSharp.WebApi
{
    public class CorrelationIdActionFilter : IActionFilter
    {

        private const string CORRELATION_ID_HTTP_HEADER = "X-Correlation-Id";

        public bool AllowMultiple {
            get { return false; }
        }

        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext,
                                                                  CancellationToken cancellationToken,
                                                                  Func<Task<HttpResponseMessage>> continuation)
        {
            string correlationId = null;

            HttpRequestHeaders headers = actionContext.Request?.Headers;
            IEnumerable<string> correlationHeaderValues;

            if (headers != null && headers.TryGetValues(CORRELATION_ID_HTTP_HEADER, out correlationHeaderValues))
                correlationId = correlationHeaderValues.FirstOrDefault(value => !String.IsNullOrWhiteSpace(value));

            if (String.IsNullOrWhiteSpace(correlationId))
                correlationId = Guid.NewGuid().ToString();

            ActivityScope scope = new ActivityScope(null, correlationId);

            return continuation().ContinueWith(task => {
                task.Result.Headers.Add(CORRELATION_ID_HTTP_HEADER, scope.Id);
                scope.Dispose();

                return task.Result;
            });
        }
    }
}
