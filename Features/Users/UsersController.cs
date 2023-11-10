using CriminalDatabaseBackend.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CriminalDatabaseBackend.Features.Users
{
    [Route("/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly DatabaseContext databaseContext;

        public UsersController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpPost("{roleId}/{districtId}/{courtId}")]
        public async Task<IActionResult> AddUser([FromRoute] int roleId, [FromRoute] int districtId, [FromRoute] int courtId, [FromBody] Users users)
        {
            var role = await databaseContext.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
            if (role == null) { return NotFound(); }

            var district = await databaseContext.Districts.FirstOrDefaultAsync(r => r.Id == districtId);
            if (district == null) { return NotFound(); }

            var court = await databaseContext.Courts.FirstOrDefaultAsync(r => r.Id == courtId);
            if (court == null) { return NotFound(); }

            users.RoleId = roleId;
            users.DistrictId = districtId;
            users.CourtId = courtId;
            await databaseContext.AddAsync(users);
            await databaseContext.SaveChangesAsync();
            return Ok("User Created !");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await databaseContext.Users.Include(users => users.Role).Include(users => users.District).Include(users => users.Court).ToListAsync();
            if (users == null) { return NotFound(); }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var users = await databaseContext.Users.Include(users => users.Role).Include(users => users.District).Include(users => users.Court).FirstOrDefaultAsync(u => u.Id == id);
            if (users == null) { return NotFound(); }
            return Ok(users);
        }

        [HttpGet("/FetchCourtAccRoleAndDis/{roleId}/{districtId}")]
        public async Task<IActionResult> FetchCourtAccRoleAndDis([FromRoute] int roleId, [FromRoute] int districtId)
        {
            var courtAccRoleAndDis = await databaseContext.Courts.Where(c => c.RoleId == roleId && c.DistrictId == districtId).ToListAsync();
            if (courtAccRoleAndDis == null) { return NotFound(); }

            return Ok(courtAccRoleAndDis);
        }

        [HttpGet("/CheckUser/{roleId}/{districtId}/{courtId}/{passwordHash}")]
        public async Task<IActionResult> CheckUser([FromRoute] int roleId, [FromRoute] int districtId, [FromRoute] int courtId, [FromRoute] string passwordHash)
        {
            var user = await databaseContext.Users.FirstOrDefaultAsync(u => u.RoleId == roleId && u.DistrictId == districtId && u.CourtId == courtId && u.PasswordHash == passwordHash);
            if (user == null) { return NotFound(); }

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] Users users, [FromRoute] int id)
        {
            var currentUser = await databaseContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (currentUser == null) { return NotFound(); };

            currentUser.UserName = users.UserName;
            currentUser.RoleId = users.RoleId;
            currentUser.DistrictId = users.DistrictId;
            currentUser.CourtId = users.CourtId;
            currentUser.PasswordHash = users.PasswordHash;

            await databaseContext.SaveChangesAsync();
            return Ok("User Updated");
        }
    }
}
