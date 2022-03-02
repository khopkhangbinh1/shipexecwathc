using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace iMWS.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ClientUtils.ServerUrl = ConfigurationManager.AppSettings["CLIENT_API"];
            // Web API 設定和服務
            config.EnableCors();

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
