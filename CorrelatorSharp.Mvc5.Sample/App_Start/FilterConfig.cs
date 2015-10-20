using System.Web;
using System.Web.Mvc;

namespace CorrelatorSharp.Mvc5.Sample
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CorrelationIdActionFilter());
        }
    }
}
