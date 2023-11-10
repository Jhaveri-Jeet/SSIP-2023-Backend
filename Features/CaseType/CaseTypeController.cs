﻿using CriminalDatabaseBackend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CriminalDatabaseBackend.Features.CaseType
{
    [Route("/[controller]")]
    [ApiController]
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
            if (caseType == null) { return NotFound(); }
            return Ok(caseType);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var caseType = await databaseContext.CaseTypes.FirstOrDefaultAsync(c => c.Id == id);
            if (caseType == null) { NotFound(); }
            return Ok(caseType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoles([FromRoute] int id, [FromBody] CaseType caseType)
        {
            var CurrentcaseType = await databaseContext.CaseTypes.FirstOrDefaultAsync(r => r.Id == id);
            if (caseType == null) { return NotFound(); }
            CurrentcaseType.Name = caseType.Name;
            CurrentcaseType.Description = caseType.Description;
            await databaseContext.SaveChangesAsync();

            return Ok("CaseType Updated !");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoles([FromRoute] int id)
        {
            var findCaseType = await databaseContext.CaseTypes.FirstOrDefaultAsync(r => r.Id == id);
            if (findCaseType == null) { return NotFound(); }
            databaseContext.Remove(findCaseType);
            await databaseContext.SaveChangesAsync();

            return Ok("CaseType Deleted !");
        }
    }
}
