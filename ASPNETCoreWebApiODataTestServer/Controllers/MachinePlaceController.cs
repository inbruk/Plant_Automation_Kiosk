using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNet.OData;

using ASPNETCoreWebApiODataTestServer.DataAccessLayer;
using ASPNETCoreWebApiODataTestServer.Models;

namespace ASPNETCoreWebApiODataTestServer.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [AutoValidateAntiforgeryToken]
    public class vwMachinePlaceController : ODataController
    {
        private readonly PlantAutomationContext _context;

        public vwMachinePlaceController(PlantAutomationContext context)
        {
            _context = context;
        }

        //  GET: odata/vwMachinePlace?$filter=WorkshopNumber eq 20 and SiteNumber eq 1 and Number eq 1
        [EnableQuery]
        public IActionResult Get(int num)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_context.vwMachinePlace);
        }
    }
}