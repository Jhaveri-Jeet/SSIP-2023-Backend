using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CriminalDatabaseBackend.Features.Witness
{
    public class Witness
    {
        [Key]
        public int Id { get; set; }
        public string? WitnessName { get; set; }
        public string? WitnessImage { get; set; }

        [ForeignKey(nameof(CaseId))]
        public int CaseId { get; set; }
        public virtual Cases.Cases? Case { get; set; }

        //[ForeignKey(nameof(RoleId))]
        //public int RoleId { get; set; }
        //public virtual Roles.Roles? Role { get; set; }
    }
}
