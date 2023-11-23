using CriminalDatabaseBackend.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CriminalDatabaseBackend.Features.Evidences
{
    [Route("/[controller]")]
    [ApiController]
    [Authorize]
    public class EvidencesController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public EvidencesController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> AddWitness([FromForm] IFormFile file, [FromForm] Evidences evidence)
        {
            var cases = await databaseContext.Cases.FirstOrDefaultAsync(c => c.Id == evidence.CaseId);
            if (cases == null) return NotFound("Case not found");

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\EvidenceImages", file.FileName);
            FileStream fileStream = System.IO.File.Create(filePath);
            await file.CopyToAsync(fileStream);
            fileStream.Close();

            evidence.EvidenceImageName = file.FileName;
            await databaseContext.AddAsync(evidence);
            await databaseContext.SaveChangesAsync();

            return Ok("Evidence Added !");
        }

        [HttpGet]   
        public async Task<IActionResult> GetAll()
        {
            var evidence = await databaseContext.Evidences.Include(evidence => evidence.Case).ToListAsync();
            if (evidence == null) return NotFound("Evidence not found");

            return Ok(evidence);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var evidence = await databaseContext.Evidences.Include(evidence => evidence.Case).FirstOrDefaultAsync(e => e.Id == id);
            if(evidence == null) return NotFound("Evidence not found");

            return Ok(evidence);
        }

        [HttpGet("/EvidenceAccCase/{caseId}")]
        public async Task<IActionResult> GetByCase([FromRoute] int caseId)
        {
            var cases = await databaseContext.Cases.FirstOrDefaultAsync(c => c.Id == caseId);
            if (cases == null) return NotFound("Case not found");

            var evidence = await databaseContext.Evidences.Where(e => e.CaseId == caseId).ToListAsync();
            if(evidence == null) return NotFound("Evidence not found");

            return Ok(evidence);
        }
    }
}
