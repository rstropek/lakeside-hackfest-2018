using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ODataMicroservice.Controllers
{
    [ODataRoutePrefix("TemperatureReadings")]
    public class TemperatureReadingsController : ODataController
    {
        private DbDataContext db;

        public TemperatureReadingsController(DbDataContext context) => db = context;

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        [ODataRoute]
        public IActionResult Get() => Ok(db.TemperatureReadings);

        [HttpGet]
        [ODataRoute("({id})")]
        public async Task<IActionResult> Get([FromODataUri]int id)
        {
            var reading = await GetByIdAsync(id);
            if (reading == null) { return NotFound(); }

            return Ok(reading);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TemperatureReading reading)
        {
            // Check if reading is valid
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            db.TemperatureReadings.Add(reading);
            await db.SaveChangesAsync();

            return Created(reading);
        }

        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IActionResult> Delete([FromODataUri]int id)
        {
            var reading = await GetByIdAsync(id);
            if (reading == null) { return NotFound(); }

            db.TemperatureReadings.Remove(reading);
            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch]
        [ODataRoute("({id})")]
        public async Task<IActionResult> Patch([FromODataUri] int id, Delta<TemperatureReading> delta)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var reading = await GetByIdAsync(id);
            if (reading == null) { return NotFound(); }

            delta.Patch(reading);
            await db.SaveChangesAsync();

            return Updated(reading);
        }

        private async Task<TemperatureReading> GetByIdAsync(int id) =>
            await db.TemperatureReadings.FirstOrDefaultAsync(c => c.TemperatureReadingId == id);
    }
}
