using GoMontag.Shared.Enums;
using GoMontag.Shared.Models.Format;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMontag.Shared.Models.FormatRevision
{
    public class FormatRevisionRequest
    {
        public int Id { get; set; }
        //public string Type { get; set; } = string.Empty;
        //public int FormatTypeId { get; set; }
        //public string Edition { get; set; } = string.Empty;
        //public DateOnly? ReleaseDate { get; set; } = null;
        public string Description { get; set; } = string.Empty;
        public required OwnershipLevel OwnershipLevel { get; set; }
        public bool IsInPrint { get; set; }
        public bool IsAiAssisted { get; set; }
        public string? AIAssistsWith { get; set; }
        //public string? CreatorId { get; set; }
        public string? ModifierId { get; set; }
        public string? ModifierName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<string?>? ContributerIds { get; set; }
        public int FormatId {  get; set; }
        public int? PreviousRevisionId { get; set; }
        public int RevisionNumber { get; set; }
    }
}
