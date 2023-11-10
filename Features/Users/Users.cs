using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CriminalDatabaseBackend.Features.Users
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }


        [ForeignKey(nameof(RoleId))]
        public int RoleId { get; set; }
        public virtual Roles.Roles? Role { get; set; }

        [ForeignKey(nameof(DistrictId))]
        public int DistrictId { get; set; }
        public virtual Districts.Districts? District { get; set; }

        [ForeignKey(nameof(CourtId))]
        public int CourtId { get; set; }
        public virtual Courts.Courts? Court { get; set; }
    }
}
