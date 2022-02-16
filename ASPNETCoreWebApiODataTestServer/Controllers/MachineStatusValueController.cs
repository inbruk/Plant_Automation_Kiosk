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
    public class MachineStatusValueController : ODataController 
    {
        private readonly PlantAutomationContext _context;

        public MachineStatusValueController(PlantAutomationContext context)
        {
            _context = context;
        }

        //  GET: odata/MachineStatusValue?$filter=WorkshopNumber eq 20 and SiteNumber eq 1
        [EnableQuery]
        public IActionResult Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<MachineStatusValue> result = _context.vwMachinePlace.Select(
                x => new MachineStatusValue()
                    {
                        WorkshopNumber = x.WorkshopNumber,
                        SiteNumber = x.SiteNumber,
                        Number = x.Number,
                        StatusId = x.StatusId,
                        IsCritical = x.IsCritical
                    }
                ).AsQueryable();
            
            return Ok(result);
        }
    }
}