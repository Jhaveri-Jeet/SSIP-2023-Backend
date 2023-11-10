using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CriminalDatabaseBackend.Features.Cases
{
    public class Cases
    {
        [Key]
        public int Id { get; set; }
        public string? DateFiled { get; set; }
        public string? CnrNumber { get; set; }
        public string? Petitioner { get; set; }
        public string? Defendant { get; set; }
        public string? JudgeName { get; set; }
        public string? Description { get; set; }
        public string? CaseStatus { get; set; }
        public string? Judgment { get; set; }
        public string? Comments { get; set; }

        [ForeignKey(nameof(CaseTypeId))]
        public int CaseTypeId { get; set; }
        public virtual CaseType.CaseType? CaseType { get; set; }

        [ForeignKey(nameof(CourtId))]
        public int CourtId { get; set; }
        public virtual Courts.Courts? Court { get; set; }

        [ForeignKey(nameof(ActId))]
        public int ActId { get; set; }
        public virtual Acts.Acts? Act { get; set; }

        [ForeignKey(nameof(AdvocateId))]
        public int AdvocateId { get; set; }
        public virtual Advocates.Advocates? Advocate { get; set; }

        [ForeignKey(nameof(AttorneyId))]
        public int AttorneyId { get; set; }
        public virtual Advocates.Advocates? Attorney { get; set; }

        [ForeignKey(nameof(RoleId))]
        public int RoleId { get; set; }
        public virtual Roles.Roles? Role { get; set; }

        [ForeignKey(nameof(TransferToId))]
        public int TransferToId { get; set; }
        public virtual Roles.Roles? TransferTo { get; set; }

        [ForeignKey(nameof(TransferFromId))]
        public int TransferFromId { get; set; }
        public virtual Roles.Roles? TransferFrom { get; set; }

    }
}
