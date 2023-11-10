using System.ComponentModel.DataAnnotations;

namespace CriminalDatabaseBackend.Features.Acts
{
    public class Acts
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
