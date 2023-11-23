using CriminalDatabaseBackend.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CriminalDatabaseBackend.Features.Courts
{
    [Route("/[controller]")]
    [ApiController]
    [Authorize]
    public class CourtsController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public CourtsController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddCourts([FromBody] Courts court)
        {
            var state = await databaseContext.States.FirstOrDefaultAsync(s => s.Id == court.StateId);
            if (state == null) return NotFound("States not found");

            var district = await databaseContext.Districts.FirstOrDefaultAsync(s => s.Id == court.DistrictId);
            if (district == null) return NotFound("Districts not found");

            var role = await databaseContext.Roles.FirstOrDefaultAsync(s => s.Id == court.RoleId);
            if (role == null) return NotFound("Roles not found");

            await databaseContext.AddAsync(court);
            await databaseContext.SaveChangesAsync();

            return Ok(court.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courts = await databaseContext.Courts.Include(courts => courts.State).Include(courts => courts.District).Include(courts => courts.Role).ToListAsync();
            if (courts == null) return NotFound("Courts not found");

            return Ok(courts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var court = await databaseContext.Courts.Include(court => court.State).Include(court => court.District).Include(courts => courts.Role).FirstOrDefaultAsync(c => c.Id == id);
            if (court == null) return NotFound("Courts not found");

            return Ok(court);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourt([FromBody] Courts court, [FromRoute] int id)
        {
            var oldCourt = await databaseContext.Courts.FirstOrDefaultAsync(s => s.Id == id);
            if (oldCourt == null) return NotFound("Courts not found");

            oldCourt.Name = court.Name;
            oldCourt.IdentificationNumber = court.IdentificationNumber;
            oldCourt.FullAddress = court.FullAddress;
            oldCourt.StateId = court.StateId;
            oldCourt.DistrictId = court.DistrictId;
            oldCourt.RoleId = court.RoleId;

            await databaseContext.SaveChangesAsync();

            return Ok("Court Updated !");
        }
    }
}
