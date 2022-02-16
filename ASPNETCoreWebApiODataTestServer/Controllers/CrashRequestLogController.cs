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
    public class CrashRequestLogController : ODataController
    {
        private readonly PlantAutomationContext _context;

        public CrashRequestLogController(PlantAutomationContext context)
        {
            _context = context;
        }

        // GET: odata/CrashRequestLog
        // FOR DEBUG ONLY !!!
        //[EnableQuery]
        //public IActionResult Get()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    return Ok(_context.CrashRequestLog);
        //}

        // POST: odata/CrashRequestLog
        public async Task<IActionResult> Post([FromBody] CrashRequestLog crashRequestLog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DateTime chekDt = DateTime.Now.AddMinutes(-1);
            PassLeanLog currPassLeanLog = _context.PassLeanLog
                .Where( x => x.KioskNumber==crashRequestLog.KioskNumber && x.IsConfirmed == true && x.IsUsed == false && x.Time > chekDt )
                .OrderByDescending(x => x.Time).Take(1).SingleOrDefault();
            
            if (currPassLeanLog == null) // не нашли соответствующую разрешенную запись в таблице с прикладываниями пропусков
            {
                return NotFound();
            }
            else // нашли соответствующую разрешенную запись в таблице с прикладываниями пропусков, используем ее и связываем с ней
            {
                Machine currMachine = _context.Machine.SingleOrDefault(x => x.InventoryNumber == crashRequestLog.MachineInventoryNumber );
                if( currMachine==null)
                {
                    return NotFound(); ;
                }
                else
                {
                    currMachine.StatusId = crashRequestLog.NewStatusId;
                }
                
                crashRequestLog.ConfirmationId = currPassLeanLog.Id;
                currPassLeanLog.IsUsed = true;

                _context.CrashRequestLog.Add(crashRequestLog);
                await _context.SaveChangesAsync();

                return Created("CrashRequestLog", crashRequestLog);
            }
        }
    }
}