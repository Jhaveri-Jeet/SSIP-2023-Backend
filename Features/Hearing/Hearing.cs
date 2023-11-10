using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CriminalDatabaseBackend.Features.Hearing
{
    public class Hearing
    {
        [Key]
        public int Id { get; set; }
        public string? HearingDate { get; set; }
        public string? HearingDetails { get; set; }

        [ForeignKey(nameof(CaseId))]
        public int CaseId { get; set; } 
        public virtual Cases.Cases? Case { get; set; }

    }
}
