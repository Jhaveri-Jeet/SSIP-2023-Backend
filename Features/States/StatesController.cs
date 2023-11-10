using CriminalDatabaseBackend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CriminalDatabaseBackend.Features.States
{
    [Route("/[controller]")]
    [ApiController]
    public class StatesController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public StatesController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddState([FromBody] States state)
        {
            await databaseContext.AddAsync(state);
            await databaseContext.SaveChangesAsync();

            return Ok("State Added !");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var advocates = await databaseContext.States.ToListAsync();
            return Ok(advocates);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var state = await databaseContext.States.FirstOrDefaultAsync(a => a.Id == id);
            if (state is null) { return NotFound(); }

            return Ok(state);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateState([FromBody] States state, [FromRoute] int id)
        {
            var oldState = await databaseContext.States.FirstOrDefaultAsync(a => a.Id == id);
            if (oldState is null) { return NotFound(); }

            oldState.Name = state.Name;

            await databaseContext.SaveChangesAsync();

            return Ok("State Updated !");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteState([FromRoute] int id)
        {
            var state = await databaseContext.States.FirstOrDefaultAsync(a => a.Id == id);
            if (state is null) { return NotFound(id); }
            databaseContext.Remove(state);
            await databaseContext.SaveChangesAsync();

            return Ok("State Deleted !");
        }

    }
}
