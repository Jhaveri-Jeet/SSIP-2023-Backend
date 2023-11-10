using System.ComponentModel.DataAnnotations;

namespace CriminalDatabaseBackend.Features.CaseType
{
    public class CaseType
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
