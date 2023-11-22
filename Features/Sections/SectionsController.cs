using CriminalDatabaseBackend.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

namespace CriminalDatabaseBackend.Features.Sections
{
    [Route("/[controller]")]
    [ApiController]
    [Authorize]
    public class SectionsController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public SectionsController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddSection([FromBody] Sections section)
        {
            var act = await databaseContext.Acts.FirstOrDefaultAsync(s => s.Id == section.ActId);
            if (act == null) { return NotFound(); }

            await databaseContext.AddAsync(section);
            await databaseContext.SaveChangesAsync();
            return Ok("Section Added !");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sections = await databaseContext.Sections.Include(sections => sections.Act).ToListAsync();
            if (sections == null) { return NotFound(); }

            return Ok(sections);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var section = await databaseContext.Sections.Include(section => section.Act).FirstOrDefaultAsync(x => x.Id == id);
            if (section == null) { return NotFound(); }

            return Ok(section);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSection([FromRoute] int id, [FromBody] Sections section)
        {
            var oldSection = await databaseContext.Sections.FirstOrDefaultAsync(x => x.Id == id);
            if (oldSection == null) { return NotFound(); };

            oldSection.Name = section.Name;
            oldSection.Description = section.Description;
            oldSection.ActId = section.ActId;

            await databaseContext.SaveChangesAsync();

            return Ok("Section Updated !");
        }
    }
}
