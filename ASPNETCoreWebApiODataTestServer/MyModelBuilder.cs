using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ASPNETCoreWebApiODataTestServer.DataAccessLayer;
using ASPNETCoreWebApiODataTestServer.Models;

namespace ASPNETCoreWebApiODataTestServer
{
    public class MyModelBuilder
    {
        public IEdmModel GetEdmModel(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);

            builder.EntitySet<Workshop>(nameof(Workshop))
                            .EntityType
                            .Filter(); // Allow for the $filter Command

            builder.EntityType<Site>().HasKey(c => new { c.WorkshopNumber, c.Number });
            builder.EntitySet<Site>(nameof(Site))
                            .EntityType
                            .Filter(); // Allow for the $filter Command

            builder.EntityType<MachineStatusValue>().HasKey(c => new { c.WorkshopNumber, c.SiteNumber });
            builder.EntitySet<MachineStatusValue>(nameof(MachineStatusValue))
                            .EntityType
                            .Filter(); // Allow for the $filter Command

            builder.EntityType<vwMachinePlace>().HasKey(c => new { c.WorkshopNumber, c.SiteNumber, c.Number });
            builder.EntitySet<vwMachinePlace>(nameof(vwMachinePlace))
                            .EntityType
                            .Filter(); // Allow for the $filter Command

            builder.EntityType<CrashRequestLog>().HasKey(c => new { c.Id });
            builder.EntitySet<CrashRequestLog>(nameof(CrashRequestLog))
                            .EntityType
                            .Filter(); // Allow for the $filter Command

            builder.EntityType<PassLeanLog>().HasKey(c => new { c.Id });
            builder.EntitySet<PassLeanLog>(nameof(PassLeanLog))
                            .EntityType
                            .OrderBy() // Allow for the $orderby Command
                            .Page() // Allow for the $top and $skip Commands
                            .Filter(); // Allow for the $filter Command

            return builder.GetEdmModel();
        }
    }
}
