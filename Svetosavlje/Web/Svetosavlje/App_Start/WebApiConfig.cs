using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Svetosavlje
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "TwoParametersApi",
                routeTemplate: "api/{controller}/{param1}/{param2}"
            );
            config.Routes.MapHttpRoute(
                name: "ThreeParametersApi",
                routeTemplate: "api/{controller}/{param1}/{param2}/{param3}"
            );

        }
    }
}
