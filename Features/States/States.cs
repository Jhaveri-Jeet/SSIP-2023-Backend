using System.ComponentModel.DataAnnotations;

namespace CriminalDatabaseBackend.Features.States
{
    public class States
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
