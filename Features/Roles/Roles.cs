using System.ComponentModel.DataAnnotations;

namespace CriminalDatabaseBackend.Features.Roles
{
    public class Roles
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
