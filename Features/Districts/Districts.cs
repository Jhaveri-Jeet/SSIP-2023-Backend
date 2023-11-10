using System.ComponentModel.DataAnnotations;

namespace CriminalDatabaseBackend.Features.Districts
{
    public class Districts
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
