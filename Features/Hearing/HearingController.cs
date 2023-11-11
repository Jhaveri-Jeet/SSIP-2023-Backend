using CriminalDatabaseBackend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CriminalDatabaseBackend.Features.Hearing
{
    [Route("/[controller]")]
    [ApiController]
    public class HearingController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public HearingController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }


        [HttpPost("{caseId}")]
        public async Task<IActionResult> AddHearing([FromRoute] int caseId, [FromBody] Hearing hearing)
        {
            var cases = await databaseContext.Cases.FirstOrDefaultAsync(c => c.Id == caseId);
            if (cases == null) { return NotFound(); }

            hearing.CaseId = caseId;
            await databaseContext.AddAsync(hearing);
            await databaseContext.SaveChangesAsync();
            return Ok("Hearing Created !");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hearing = await databaseContext.Hearing.Include(hearing => hearing.Case).ToListAsync();
            if (hearing == null) { return NotFound(); }
            return Ok(hearing);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var hearing = await databaseContext.Hearing.Include(hearing => hearing.Case).FirstOrDefaultAsync(u => u.Id == id);
            if (hearing == null) { return NotFound(); }
            return Ok(hearing);
        }

        [HttpGet("/HearingAccCase/{caseId}")]
        public async Task<IActionResult> GetByCase([FromRoute] int caseId)
        {
            var cases = await databaseContext.Cases.FirstOrDefaultAsync(c => c.Id == caseId);
            if (cases == null) { return NotFound(); };

            var hearing = await databaseContext.Hearing.Where(h => h.CaseId == caseId).ToListAsync();
            if (hearing == null) { return NotFound(); }

            return Ok(hearing);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHearing([FromBody] Hearing hearing, [FromRoute] int id)
        {
            var oldHearing = await databaseContext.Hearing.FirstOrDefaultAsync(u => u.Id == id);
            if (oldHearing == null) { return NotFound(); };

            oldHearing.HearingDate = hearing.HearingDate;
            oldHearing.HearingDetails = hearing.HearingDetails;
            oldHearing.CaseId = hearing.CaseId;

            await databaseContext.SaveChangesAsync();
            return Ok("Hearing Updated");
        }
    }
}
