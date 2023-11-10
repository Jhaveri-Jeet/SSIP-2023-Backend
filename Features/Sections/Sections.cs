using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CriminalDatabaseBackend.Features.Sections
{
    public class Sections
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        [ForeignKey(nameof(ActId))]
        public int ActId { get; set; }
        public virtual Acts.Acts? Act { get; set; }
    }

}