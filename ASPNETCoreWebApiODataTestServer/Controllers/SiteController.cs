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
    public class SiteController : ODataController
    {
        private readonly PlantAutomationContext _context;

        public SiteController(PlantAutomationContext context)
        {
            _context = context;
        }

        //  GET: odata/Site?$filter=WorkshopNumber eq 20
        [EnableQuery]
        public IActionResult Get(int num)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_context.Site);
        }
    }
}