using CriminalDatabaseBackend.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CriminalDatabaseBackend.Features.CaseType
{
    [Route("/[controller]")]
    [ApiController]
    [Authorize]
    public class CaseTypeController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public CaseTypeController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddRoles([FromBody] CaseType caseType)
        {
            await databaseContext.AddAsync(caseType);
            await databaseContext.SaveChangesAsync();
            return Ok("CaseType Inserted");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var caseType = await databaseContext.CaseTypes.ToListAsync();
            if (caseType == null) return NotFound("CaseType not found");

            return Ok(caseType);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var caseType = await databaseContext.CaseTypes.FirstOrDefaultAsync(c => c.Id == id);
            if (caseType == null) return NotFound("CaseType not found");

            return Ok(caseType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoles([FromRoute] int id, [FromBody] CaseType caseType)
        {
            var CurrentcaseType = await databaseContext.CaseTypes.FirstOrDefaultAsync(r => r.Id == id);
            if (caseType == null) return NotFound("CaseType not found");

            CurrentcaseType.Name = caseType.Name;
            CurrentcaseType.Description = caseType.Description;

            await databaseContext.SaveChangesAsync();
            return Ok("CaseType Updated !");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoles([FromRoute] int id)
        {
            var findCaseType = await databaseContext.CaseTypes.FirstOrDefaultAsync(r => r.Id == id);
            if (findCaseType == null) return NotFound("CaseType not found");

            databaseContext.Remove(findCaseType);
            await databaseContext.SaveChangesAsync();
            return Ok("CaseType Deleted !");
        }
    }
}
