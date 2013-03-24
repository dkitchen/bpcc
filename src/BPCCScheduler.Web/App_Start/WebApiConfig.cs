using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BPCCScheduler
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //DMK 2013-03-24 added elmah error handling for webapi
            config.Filters.Add(
                new ElmahErrorAttribute()
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
