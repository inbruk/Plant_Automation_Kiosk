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

namespace ASPNETCoreWebApiODataTestServer.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [AutoValidateAntiforgeryToken]
    public class WorkshopController : ODataController
    {
        private readonly PlantAutomationContext _context;

        public WorkshopController(PlantAutomationContext context)
        {
            _context = context;
        }

        //  GET: odata/Workshop
        [EnableQuery]
        public IActionResult Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_context.Workshop);
        }
    }
}