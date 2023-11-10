using System.ComponentModel.DataAnnotations;

namespace CriminalDatabaseBackend.Features.Advocates
{
    public class Advocates
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? EnrollmentNumber { get; set; }
    }
}
