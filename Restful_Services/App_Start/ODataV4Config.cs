using Microsoft.AspNet.OData.Batch;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Restful_Services.Models;
using System.Web.Http;

namespace Restful_Services
{
    public class ODataV4Config
    {
        public static void Register(HttpConfiguration config)
        {
            // ODataV4 routes and services configuration
            ODataBatchHandler batchHandler = new DefaultODataBatchHandler(System.Web.Http.GlobalConfiguration.DefaultServer);
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<EventData>("ODataV4");
            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);
            config.MapODataServiceRoute(routeName: "ODataV4Route", routePrefix: "odata", model: builder.GetEdmModel(), batchHandler: batchHandler);
            //HttpConfigurationExtensions.SetTimeZoneInfo(config, HttpConfigurationExtensions.GetTimeZoneInfo(config));
            //System.Web.Http.HttpConfiguration.MapODataServiceRoute(configuration: config, routeName: "ODataV4Route", routePrefix: "odata", model: builder.GetEdmModel(), batchHandler: batchHandler);
        }
    }
}