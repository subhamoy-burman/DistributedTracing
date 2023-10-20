using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CLN.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly TelemetryClient _telemetryClient;

        public DemoController()
        {
            TelemetryConfiguration configuration = new TelemetryConfiguration("<insert>");
            _telemetryClient = new TelemetryClient(configuration);
        }
        // GET: api/<DemoController>
        [HttpGet]
        public IActionResult Get()
        {
            var operationId = Activity.Current?.Id;

            // Create an ObjectResult with the values
            var result = new ObjectResult(new string[] { "value1", "value2" })
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            _telemetryClient.TrackEvent($"{operationId}: Fetching from database");

            _telemetryClient.TrackEvent($"{operationId}: Aggregating patient and clinic data");

            // Add the operation ID to the HTTP headers
            Response.Headers.Add("Operation-Id", operationId);

            return result;
        }


        // GET api/<DemoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DemoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DemoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DemoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
