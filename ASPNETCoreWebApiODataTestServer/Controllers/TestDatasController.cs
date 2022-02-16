using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNETCoreWebApiODataTestServer.DataAccessLayer;
using ASPNETCoreWebApiODataTestServer.Models;
using Microsoft.AspNetCore.Cors;

namespace ASPNETCoreWebApiODataTestServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAllOrigins")]
    [AutoValidateAntiforgeryToken]
    public class TestDatasController : ControllerBase
    {
        private List<TestData> _data = null;

        public TestDatasController(PlantAutomationContext context)
        {
            _data = new List<TestData>();

            var val1 = new TestData();
            val1.Id = 0;

            var val2 = new TestData();
            val2.Id = 1;

            var val3 = new TestData();
            val3.Id = 2;

            _data.Add(val1);
            _data.Add(val2);
            _data.Add(val3);
        }

        // GET: api/TestDatas
        [HttpGet]
        public IEnumerable<TestData> GetTestData()
        {
            return _data;
        }

        // GET: api/TestDatas/5
        [HttpGet("{id}")]
        public IActionResult GetTestData([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TestData result = null;
            try
            {
                result = _data[id];
            }
            catch
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/TestDatas/5
        [HttpPut("{id}")]
        public IActionResult PutTestData([FromRoute] int id, [FromBody] TestData testData)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != testData.Id )  return BadRequest();
            if ( _data.Count < (id - 1) || _data.Count == 0) return NotFound();

            _data[id] = testData;

            return NoContent();
        }

        // POST: api/TestDatas
        [HttpPost]
        public IActionResult PostTestData([FromBody] TestData testData)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _data.Add(testData);

            return CreatedAtAction("GetTestData", new { id = testData.Id }, testData);
        }

        // DELETE: api/TestDatas/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTestData([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_data.Count < (id - 1) || _data.Count == 0) return NotFound();

            var testData = _data[id];
            if (testData == null)
            {
                return NotFound();
            }

            _data.RemoveAt(id);

            return Ok(testData);
        }
    }
}