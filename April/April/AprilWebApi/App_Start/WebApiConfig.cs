using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AprilWebApi.Handler;

namespace AprilWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务
            //config.MessageHandlers.Add(new ResultWrapHandler());
            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
