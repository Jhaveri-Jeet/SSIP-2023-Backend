using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CriminalDatabaseBackend.Features.Courts
{
    public class Courts
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? FullAddress { get; set; }

        [ForeignKey(nameof(RoleId))]
        public int RoleId { get; set; }
        public virtual Roles.Roles? Role { get; set; }

        [ForeignKey(nameof(StateId))]
        public int StateId { get; set; }

        public virtual States.States? State { get; set; }

        [ForeignKey(nameof(DistrictId))]
        public int DistrictId { get; set; }
        public virtual Districts.Districts? District { get; set; }

    }
}
