using CriminalDatabaseBackend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CriminalDatabaseBackend.Features.Evidences
{
    [Route("/[controller]")]
    [ApiController]
    public class EvidencesController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public EvidencesController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpPost("{id}"), DisableRequestSizeLimit]
        public async Task<IActionResult> AddWitness([FromRoute] int id, [FromForm] IFormFile file, [FromForm] Evidences evidence)
        {
            var cases = await databaseContext.Cases.FirstOrDefaultAsync(c => c.Id == id);
            if (cases is null) return NotFound();

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\EvidenceImages", file.FileName);
            FileStream fileStream = System.IO.File.Create(filePath);
            await file.CopyToAsync(fileStream);
            fileStream.Close();

            evidence.CaseId = id;
            evidence.EvidenceImageName = file.FileName;
            await databaseContext.AddAsync(evidence);
            await databaseContext.SaveChangesAsync();

            return Ok("Evidence Added !");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var evidence = await databaseContext.Evidences.Include(evidence => evidence.Case).ToListAsync();
            return Ok(evidence);
        }

        [HttpGet("/EvidenceAccCase/{caseId}")]
        public async Task<IActionResult> GetByCase([FromRoute] int caseId)
        {
            var cases = await databaseContext.Cases.FirstOrDefaultAsync(c => c.Id == caseId);
            if (cases is null) return NotFound();

            var evidence = await databaseContext.Evidences.Where(e => e.CaseId == caseId).ToListAsync();
            if(evidence is null) return NotFound();

            return Ok(evidence);
        }
    }
}
