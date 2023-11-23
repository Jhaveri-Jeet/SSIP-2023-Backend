using CriminalDatabaseBackend.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CriminalDatabaseBackend.Features.Users
{
    [Route("/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly DatabaseContext databaseContext;
        private readonly AppSettings appSettings;
        //private readonly IWebHostEnvironment env;

        public UsersController(DatabaseContext databaseContext, IOptions<AppSettings> appSettings)
        {
            this.databaseContext = databaseContext;
            this.appSettings = appSettings.Value;
            //this.env = env;
        }

        // JWT function which will return the generated token 
        private async Task<string> GenerateJwtAsync(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JWTSecurityKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, user.Id.ToString()),
                new(ClaimTypes.Role, user.RoleId.ToString()),
                new("districtId", user.DistrictId.ToString()),
                new("courtId", user.CourtId.ToString())
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            Response.Cookies.Append("token", tokenString, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                //Secure = !env.IsDevelopment(),
            });

            return tokenString;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] Users users)
        {
            var role = await databaseContext.Roles.FirstOrDefaultAsync(r => r.Id == users.RoleId);
            if (role == null) return NotFound("Role not found");

            var district = await databaseContext.Districts.FirstOrDefaultAsync(r => r.Id == users.DistrictId);
            if (district == null) return NotFound("District not found");

            var court = await databaseContext.Courts.FirstOrDefaultAsync(r => r.Id == users.CourtId);
            if (court == null) return NotFound("Court not found");

            await databaseContext.AddAsync(users);
            await databaseContext.SaveChangesAsync();
            return Ok("User Created !");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await databaseContext.Users.Include(users => users.Role).Include(users => users.District).Include(users => users.Court).ToListAsync();
            if (users == null) return NotFound("User not found");

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var users = await databaseContext.Users.Include(users => users.Role).Include(users => users.District).Include(users => users.Court).FirstOrDefaultAsync(u => u.Id == id);
            if (users == null) return NotFound("User not found");

            return Ok(users);
        }

        [HttpGet("/FetchCourtAccRoleAndDis/{roleId}/{districtId}")]
        public async Task<IActionResult> FetchCourtAccRoleAndDis([FromRoute] int roleId, [FromRoute] int districtId)
        {
            var role = await databaseContext.Roles.FirstOrDefaultAsync(u => u.Id == roleId);
            if (role == null) return NotFound("Role not found");

            var district = await databaseContext.Districts.FirstOrDefaultAsync(d => d.Id == districtId);
            if (district == null) return NotFound("District not found");

            var courtAccRoleAndDis = await databaseContext.Courts.Where(c => c.RoleId == roleId && c.DistrictId == districtId).ToListAsync();
            if (courtAccRoleAndDis == null) return NotFound("Court not found");

            return Ok(courtAccRoleAndDis);
        }

        [HttpGet("/FetchUserAccCourtAndDis/{roleId}/{districtId}")]
        public async Task<IActionResult> FetchUserAccCourtAndDis([FromRoute] int roleId, [FromRoute] int districtId)
        {
            var role = await databaseContext.Roles.FirstOrDefaultAsync(u => u.Id == roleId);
            if (role == null) return NotFound("Role not found");

            var district = await databaseContext.Districts.FirstOrDefaultAsync(d => d.Id == districtId);
            if (district == null) return NotFound("District not found");

            var user = await databaseContext.Users.Where(u => u.RoleId == roleId && u.DistrictId == districtId).ToListAsync();
            if (user == null) return NotFound("User not found");

            return Ok(user);
        }

        [Authorize]
        [HttpGet("/FetchUserAccCourt/{courtId}")]
        public async Task<IActionResult> FetchUserAccCourt([FromRoute] int courtId)
        {
            var court = await databaseContext.Courts.FirstOrDefaultAsync(c => c.Id == courtId);
            if (court == null) return NotFound("Court not found");

            var user = await databaseContext.Users.Where(u => u.CourtId == courtId).ToListAsync();
            if (user == null) return NotFound("User not found");

            return Ok(user);
        }

        [HttpGet("/CheckUser/{userId}/{passwordHash}")]
        public async Task<IActionResult> CheckUser([FromRoute] int userId, [FromRoute] string passwordHash)
        {
            var user = await databaseContext.Users.FirstOrDefaultAsync(u => u.Id == userId && u.PasswordHash == passwordHash);
            if (user == null) return NotFound("User not found");

            string token = await GenerateJwtAsync(user);

            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] Users users, [FromRoute] int id)
        {
            var currentUser = await databaseContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (currentUser == null) return NotFound("User not found");

            currentUser.UserName = users.UserName;
            currentUser.RoleId = users.RoleId;
            currentUser.DistrictId = users.DistrictId;
            currentUser.CourtId = users.CourtId;
            currentUser.PasswordHash = users.PasswordHash;

            await databaseContext.SaveChangesAsync();
            return Ok("User Updated");
        }

        [Authorize]
        [HttpPut("/CheckPassword")]
        public async Task<IActionResult> CheckPassword([FromBody] Users user)
        {
            var oldUser = await databaseContext.Users.FirstOrDefaultAsync(o => o.Id == user.Id && o.PasswordHash == user.PasswordHash);
            if (oldUser == null) return NotFound("User not found");

            return Ok("Old is Password Correct");
        }

        [Authorize]
        [HttpPut("/UpdatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] Users user)
        {
            var oldUser = await databaseContext.Users.FirstOrDefaultAsync(o => o.Id == user.Id);
            if (oldUser == null) return NotFound("User not found");

            oldUser.PasswordHash = user.PasswordHash;
            await databaseContext.SaveChangesAsync();
            return Ok("Password Updated");
        }
    }
}
