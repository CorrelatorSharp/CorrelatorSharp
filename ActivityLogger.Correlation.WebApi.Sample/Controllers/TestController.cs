using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ActivityLogger.Correlation.WebApi.Sample.Controllers
{
    public class TestController : ApiController
    {

        public IHttpActionResult Test()
        {
            return Json(ActivityLogScope.Current.Id);
        }
           
    }
}
