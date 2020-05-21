using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using webapi1.DAO;
using webapi1.DAO.Member;
using webapi1.Exceptions;
using webapi1.Filters;
using webapi1.Models;

namespace webapi1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 設定和服務

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // MediaType Formatter Setting
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // Exception Filter Setting
            config.Filters.Add(new ExceptionHandlerAttribute());
            // Action Filter Setting
            config.Filters.Add(new ActionInfoAttribute());
        }
    }
}
