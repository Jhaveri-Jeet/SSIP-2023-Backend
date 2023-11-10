using CriminalDatabaseBackend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace CriminalDatabaseBackend.Features.Witness
{
    [Route("/[controller]")]
    [ApiController]
    public class WitnessController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public WitnessController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpPost("{id}"), DisableRequestSizeLimit]
        public async Task<IActionResult> AddWitness([FromRoute] int id, [FromForm] IFormFile file)
        {
            var cases = await databaseContext.Cases.FirstOrDefaultAsync(c => c.Id == id);
            if (cases is null) return NotFound();

            string uploadDir = "D:\\Jeet Jhaveri\\Projects\\.Net\\CriminalDatabaseBackend\\Shared\\Images\\";
            string filePath = Path.Combine(uploadDir, file.FileName);
            var imageName = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
            FileStream fileStream = System.IO.File.Create(filePath);
            await file.CopyToAsync(fileStream);
            fileStream.Close();

            var newWitness = new Witness
            {
                WitnessImage = imageName,
                CaseId = id
            };

            await databaseContext.AddAsync(newWitness);
            await databaseContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var witness = await databaseContext.Witness.Include(witness => witness.Case).ToListAsync();
            if (witness is null) return NotFound();
            return Ok(witness);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var witness = await databaseContext.Witness.Include(witness => witness.Case).FirstOrDefaultAsync(w => w.Id == id);
            if (witness is null) return NotFound();

            return Ok(witness);
        }


    }
}
