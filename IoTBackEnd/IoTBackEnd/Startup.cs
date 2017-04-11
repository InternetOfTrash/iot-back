using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(iot_backend.Startup))]
namespace IoTBackEnd
{
    class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.

        public void Configuration(IAppBuilder appBuilder)
        {

            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            SwaggerConfig.Register(config);
            //config.MessageHandlers.Add(new APIKeyHandler());
            WebApiConfig.Register(config);
            config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "containers/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            config.Formatters.Add(new JsonMediaTypeFormatter());
            appBuilder.UseWebApi(config);
        }

    }
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        }
    }

}
