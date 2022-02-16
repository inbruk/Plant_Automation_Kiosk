using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Results;

using ASPNETCoreWebApiODataTestServer.DataAccessLayer;
using ASPNETCoreWebApiODataTestServer.Models;
using Microsoft.AspNet.OData.Routing;

namespace ASPNETCoreWebApiODataTestServer.Controllers
{
    [EnableCors("AllowAllOrigins")]
    // [AutoValidateAntiforgeryToken]
    public class PassLeanLogController : ODataController
    {
        private readonly PlantAutomationContext _context;

        public PassLeanLogController(PlantAutomationContext context)
        {
            _context = context;
        }

        //  GET: odata/PassLeanLog?$filter=KioskNumber eq 5
        [EnableQuery]
        public IActionResult Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime chekDt = DateTime.Now.AddMinutes(-1);
            var query = _context.PassLeanLog.Where(x => x.IsConfirmed==true && x.IsUsed == false && x.Time > chekDt )
                .OrderByDescending( x => x.Time ).Take(1);

            return Ok(query);
        }

        // POST: odata/PassLeanLog
        public async Task<IActionResult> Post([FromBody] PassLeanLog passLeanLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            passLeanLog.IsUsed = false;
            if ( ( passLeanLog.KioskNumber==3 && (passLeanLog.PassNumber=="128,44633" || passLeanLog.PassNumber == "128,45002") ) ||
                 ( passLeanLog.KioskNumber==5 && passLeanLog.PassNumber == "128,44258") )
            {
                passLeanLog.IDPERNR = 3424389;
                passLeanLog.IsConfirmed = true;
            }              
            else
            {
                passLeanLog.IsConfirmed = false;
            }            

            _context.PassLeanLog.Add(passLeanLog);
            await _context.SaveChangesAsync();

            return Created("PassLeanLog", passLeanLog);
        }

    }
}
