using GoMontag.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMontag.Shared.Models.Format
{
    public class FormatRequest
    {
        public string Type { get; set; } = string.Empty;
        public int FormatTypeId { get; set; }
        public string Edition { get; set; } = string.Empty;
        public string? Slug { get; set; }
        public bool IsLocked { get; set; }
        public string? lockedReason { get; set; }
        public string? LockedByUser { get; set; } = string.Empty;
        public DateTime? LockedDate { get; set; } = null;
        public string? CoverImageUrl { get; set; } = string.Empty;
        public DateOnly? ReleaseDate { get; set; } = null;
        public string? CreatorId { get; set; }
        public int ProductId { get; set; }
        public int formatrevisionid { get; set; }
        public bool EditRequested { get; set; }
        public string? InformationRequested { get; set; }
    }
}
