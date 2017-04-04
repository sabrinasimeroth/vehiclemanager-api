using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Serialization;

namespace VehicleManager.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

			//enable CORS
			config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

			//camel case reformatting
			config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
				new CamelCasePropertyNamesContractResolver();

			//XML formatting
			config.Formatters.Remove(config.Formatters.XmlFormatter);

        }
    }
}
