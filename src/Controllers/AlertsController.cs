using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ODataMicroservice.Controllers
{
    [ODataRoutePrefix("Alerts")]
    public class AlertsController : ODataController
    {
        private DbDataContext db;

        public AlertsController(DbDataContext context) => db = context;

        [EnableQuery]
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
        public async Task<IActionResult> Post([FromBody]Alert alert)
        {
            // Check if reading is valid
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            db.Alerts.Add(alert);
            await db.SaveChangesAsync();

            return Created(alert);
        }

        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<IActionResult> Delete([FromODataUri]int id)
        {
            var alert = await GetByIdAsync(id);
            if (alert == null) { return NotFound(); }

            db.Alerts.Remove(alert);
            await db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch]
        [ODataRoute("({id})")]
        public async Task<IActionResult> Patch([FromODataUri] int id, Delta<Alert> delta)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var alert = await GetByIdAsync(id);
            if (alert == null) { return NotFound(); }

            delta.Patch(alert);
            await db.SaveChangesAsync();

            return Updated(alert);
        }

        private async Task<Alert> GetByIdAsync(int id) =>
            await db.Alerts.FirstOrDefaultAsync(c => c.AlertId == id);
    }
}
