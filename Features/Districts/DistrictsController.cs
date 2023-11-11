﻿using CriminalDatabaseBackend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CriminalDatabaseBackend.Features.Districts
{
    [Route("/[controller]")]
    [ApiController]
    public class DistrictsController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public DistrictsController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddDistrict([FromBody] Districts district)
        {
            await databaseContext.AddAsync(district);
            await databaseContext.SaveChangesAsync();

            return Ok("District Added !");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var district = await databaseContext.Districts.ToListAsync();
            return Ok(district);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var district = await databaseContext.Districts.FirstOrDefaultAsync(a => a.Id == id);
            if (district is null) { return NotFound(); }

            return Ok(district);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDistrict([FromBody] Districts district, [FromRoute] int id)
        {
            var oldDistrict = await databaseContext.Districts.FirstOrDefaultAsync(a => a.Id == id);
            if (oldDistrict is null) { return NotFound(); }

            oldDistrict.Name = district.Name;

            await databaseContext.SaveChangesAsync();

            return Ok("District Updated !");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistrict([FromRoute] int id)
        {
            var district = await databaseContext.Districts.FirstOrDefaultAsync(a => a.Id == id);
            if (district is null) { return NotFound(id); }
            databaseContext.Remove(district);
            await databaseContext.SaveChangesAsync();

            return Ok("District Deleted !");
        }
    }
}