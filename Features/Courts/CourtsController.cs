using CriminalDatabaseBackend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CriminalDatabaseBackend.Features.Courts
{
    [Route("/[controller]")]
    [ApiController]
    public class CourtsController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public CourtsController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpPost("{stateId}/{districtId}/{roleId}")]

        public async Task<IActionResult> AddCourts([FromRoute] int stateId, [FromRoute] int districtId, [FromRoute] int roleId, [FromBody] Courts court)
        {
            var state = await databaseContext.States.FirstOrDefaultAsync(s => s.Id == stateId);
            if (state == null) { return NotFound(); }

            var district = await databaseContext.Districts.FirstOrDefaultAsync(s => s.Id == districtId);
            if (district == null) { return NotFound(); }

            var role = await databaseContext.Roles.FirstOrDefaultAsync(s => s.Id == roleId);
            if (role == null) { return NotFound(); }

            court.StateId = stateId;
            court.DistrictId = districtId;
            court.RoleId = roleId;

            await databaseContext.AddAsync(court);
            await databaseContext.SaveChangesAsync();

            return Ok("Court Added");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courts = await databaseContext.Courts.Include(courts => courts.State).Include(courts => courts.District).Include(courts => courts.Role).ToListAsync();
            if (courts == null) return NotFound();
            return Ok(courts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var court = await databaseContext.Courts.Include(court => court.State).Include(court => court.District).Include(courts => courts.Role).FirstOrDefaultAsync(c => c.Id == id);
            if (court == null) return NotFound();

            return Ok(court);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourt([FromBody] Courts court, [FromRoute] int id)
        {
            var oldCourt = await databaseContext.Courts.FirstOrDefaultAsync(s => s.Id == id);
            if (oldCourt == null) { return NotFound(); }

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
