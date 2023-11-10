using CriminalDatabaseBackend.Shared;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CriminalDatabaseBackend.Features.Acts
{
    [Route("/[controller]")]
    [ApiController]
    public class ActsController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public ActsController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddAct([FromBody] Acts act)
        {
            await databaseContext.AddAsync(act);
            await databaseContext.SaveChangesAsync();

            return Ok("Act Added !");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var act = await databaseContext.Acts.ToListAsync();
            return Ok(act);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var act = await databaseContext.Acts.FirstOrDefaultAsync(x => x.Id == id);
            if (act == null) { return NotFound(); }

            return Ok(act);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAct([FromBody] Acts act, [FromRoute] int id)
        {
            var oldAct = await databaseContext.Acts.FirstOrDefaultAsync(x => x.Id == id);
            if (oldAct == null) { return NotFound(); };

            oldAct.Name = act.Name;
            oldAct.Description = act.Description;

            await databaseContext.SaveChangesAsync();
            return Ok("Act Updated !");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAct([FromRoute] int id)
        {
            var act = await databaseContext.Acts.FirstOrDefaultAsync(x => x.Id == id);
            if (act == null) { return NotFound(); }

            databaseContext.Remove(act);
            await databaseContext.SaveChangesAsync();

            return Ok("Act Deleted !");
        }

    }
}
