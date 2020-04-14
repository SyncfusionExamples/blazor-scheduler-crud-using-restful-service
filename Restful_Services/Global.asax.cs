using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Http;

namespace Restful_Services
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(config =>
            {
                config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings()
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver(),
                    DateTimeZoneHandling = DateTimeZoneHandling.Local,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                };
                config.Formatters.JsonFormatter.UseDataContractJsonSerializer = true;
                ODataV4Config.Register(config);
            });
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, Prefer, Authorization");
                HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PATCH, PUT, DELETE, OPTIONS");
                HttpContext.Current.Response.End();
            }
        }
    }
}
