using CriminalDatabaseBackend.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CriminalDatabaseBackend.Features.Roles
{
    [Route("/[controller]")]
    [ApiController]
    public class RolesController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public RolesController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRoles([FromBody] Roles roles)
        {
            await databaseContext.AddAsync(roles);
            await databaseContext.SaveChangesAsync();
            return Ok("Roles Inserted");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await databaseContext.Roles.ToListAsync();
            if (roles == null) { return NotFound(); }
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var roles = await databaseContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (roles == null) { return NotFound(); }
            return Ok(roles);
        }

        [Authorize]
        [HttpPut("id")]
        public async Task<IActionResult> UpdateRoles([FromRoute] int id, [FromBody] Roles roles)
        {
            var currentRoles = await databaseContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (roles == null) { return NotFound(); }
            currentRoles.Name = roles.Name;
            await databaseContext.SaveChangesAsync();

            return Ok("Roles Updated !");
        }

        [Authorize]
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteRoles([FromRoute] int id)
        {
            var findRole = await databaseContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (findRole == null) { return NotFound(); }
            databaseContext.Remove(findRole);
            await databaseContext.SaveChangesAsync();

            return Ok("Role Deleted !");
        }
    }
}
