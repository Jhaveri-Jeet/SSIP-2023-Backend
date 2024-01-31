using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CriminalDatabaseBackend.Features.Evidences
{
    public class Evidences
    {
        [Key]
        public int Id { get; set; }
        public string? EvidenceDescription { get; set; }
        public string? EvidenceImageName { get; set; }

        [ForeignKey(nameof(CaseId))]
        public int CaseId { get; set; }
        public virtual Cases.Cases? Case { get; set; }

        [ForeignKey(nameof(RoleId))]
        public int RoleId { get; set; }
        public virtual Roles.Roles? Role { get; set; }
    }
}
