using GoMontag.Shared.Entities;
using GoMontag.Shared.Entities.Interfaces;
using GoMontag.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Entities.Revisions
{
    public class FormatRevision : BaseEntity
    {
        // Associated Format
        public int FormatId { get; set; }
        public Format? Format { get; set; }
        // Uses AI? Y/N
        public bool IsAiAssisted { get; set; }
        public string? AIAssistsWith { get; set; }
        // Review
        public string? Description { get; set; }
        // Ownership Level
        public required OwnershipLevel OwnershipLevel { get; set; }
        // IsInPrint
        public bool IsInPrint { get; set; }
        //Edit Summary
        public string? EditSummary { get; set; } = string.Empty;
        public int RevisionNumber { get; set; } = 0;
        // Last Person To Modify the Format Review
        public string? ModifierName { get; set; }
        public string? ModifierId { get; set; }
        // Previous Version
        public int? PreviousRevisionId { get; set; }
        public FormatRevision? PreviousRevision { get; set; }
    }
}
