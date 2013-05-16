using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace IlinkDb.WebService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { controller = "V1", action="Version", id = RouteParameter.Optional }
            );
        }
    }
}
