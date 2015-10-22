using System;
using System.Linq;
using CorrelatorSharp;

namespace RestSharp
{
    public static class IRestRequestCorrelationIdHeaderExtensions
    {
        public static void AddCorrelationHeader(this IRestRequest request)
        {
            request.AddHeader(Headers.CorrelationId, ActivityScope.Current?.Id ?? Guid.NewGuid().ToString());
        }
    }
}
