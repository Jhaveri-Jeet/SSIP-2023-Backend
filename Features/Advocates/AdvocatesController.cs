using CriminalDatabaseBackend.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CriminalDatabaseBackend.Features.Advocates
{
    [Route("/[controller]")]
    [ApiController]
    [Authorize]
    public class AdvocatesController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public AdvocatesController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddAdvocate([FromBody] Advocates advocate)
        {
            await databaseContext.AddAsync(advocate);
            await databaseContext.SaveChangesAsync();

            return Ok("Advocate Added !");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var advocates = await databaseContext.Advocates.ToListAsync();
            return Ok(advocates);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var advocate = await databaseContext.Advocates.FirstOrDefaultAsync(a => a.Id == id);
            if (advocate == null) return NotFound("Advocate not found");

            return Ok(advocate);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdvocate([FromBody] Advocates advocate, [FromRoute] int id)
        {
            var oldAdvocate = await databaseContext.Advocates.FirstOrDefaultAsync(a => a.Id == id);
            if (oldAdvocate == null) return NotFound("Advocate not found");

            oldAdvocate.Name = advocate.Name;
            oldAdvocate.EnrollmentNumber = advocate.EnrollmentNumber;

            await databaseContext.SaveChangesAsync();
            return Ok("Advocate Updated !");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdvocate([FromRoute] int id)
        {
            var advocate = await databaseContext.Advocates.FirstOrDefaultAsync(a => a.Id == id);
            if (advocate == null) return NotFound("Advocate not found");

            databaseContext.Remove(advocate);
            await databaseContext.SaveChangesAsync();
            return Ok("Advocate Deleted !");
        }
    }
}
