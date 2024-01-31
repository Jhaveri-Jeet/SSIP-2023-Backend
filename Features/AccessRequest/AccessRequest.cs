using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Text;
using System.Net.Mime;

namespace CriminalDatabaseBackend.Features.AccessRequest
{
    public class AccessRequest
    {
        [Key]
        public int Id { get; set; }
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Remarks { get; set; }

        public string? Status { get; set; }

        [ForeignKey(nameof(CaseId))]
        public int CaseId { get; set; }
        public virtual Cases.Cases? Case { get; set; }

        [ForeignKey(nameof(StateId))] 
        public int StateId { get; set; }
        public virtual States.States? State { get; set; }

        [ForeignKey(nameof(DistrictId))]
        public int DistrictId { get; set; }
        public virtual Districts.Districts? Districts { get; set; }

        [ForeignKey(nameof(RoleId))]
        public int RoleId { get; set; }
        public virtual Roles.Roles? Role { get; set; }

        [ForeignKey(nameof(CourtId))]
        public int CourtId { get; set; }
        public virtual Courts.Courts? Courts { get; set; }

        [ForeignKey(nameof(RequestedCourtId))]
        public int RequestedCourtId { get; set; }
        public virtual Courts.Courts? RequestedCourt { get; set; }

        [ForeignKey(nameof(TransferReqestCourtId))]
        public int TransferReqestCourtId { get; set; }
        public virtual Courts.Courts? TransferCourtToId { get; set; }

    }
}
