﻿using CriminalDatabaseBackend.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace CriminalDatabaseBackend.Features.Witness
{
    [Route("/[controller]")]
    [ApiController]
    [Authorize]
    public class WitnessController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public WitnessController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpPost("{id}"), DisableRequestSizeLimit]
        public async Task<IActionResult> AddWitness([FromRoute] int id, [FromForm] IFormFile file, [FromForm] Witness witness)
        {
            var cases = await databaseContext.Cases.FirstOrDefaultAsync(c => c.Id == id);
            if (cases is null) return NotFound("Case not found");

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\WitnessImages", file.FileName);
            FileStream fileStream = System.IO.File.Create(filePath);
            await file.CopyToAsync(fileStream);
            fileStream.Close();

            witness.CaseId = id;
            witness.WitnessImage = file.FileName;
            await databaseContext.AddAsync(witness);
            await databaseContext.SaveChangesAsync();

            return Ok("Witness Added !");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var witness = await databaseContext.Witness.Include(witness => witness.Case).ToListAsync();
            if (witness is null) return NotFound("Witnesss not found");

            return Ok(witness);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var witness = await databaseContext.Witness.Include(witness => witness.Case).FirstOrDefaultAsync(w => w.Id == id);
            if (witness is null) return NotFound("Witness not found");

            return Ok(witness);
        }

        [HttpGet("/WitnessAccCase/{caseId}")]
        public async Task<IActionResult> GetByCase([FromRoute] int caseId)
        {
            var cases = await databaseContext.Cases.FirstOrDefaultAsync(c => c.Id == caseId);
            if(cases is null) return NotFound("Case not found");

            var witness = await databaseContext.Witness.Where(w => w.CaseId == caseId).ToListAsync();
            if (witness is null) return NotFound("Witness not found");

            return Ok(witness);
        }

    }
}
