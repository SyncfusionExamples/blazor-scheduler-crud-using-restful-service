using Microsoft.Owin;
using Owin;
using System;
using System.Threading.Tasks;
using System.Web.Http;

[assembly: OwinStartup(typeof(Restful_Services.Startup))]

namespace Restful_Services
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configure(config => {
                app.UseWebApi(config);
                app.UseWebApi(GlobalConfiguration.DefaultServer);
            });
        }
    }
}
