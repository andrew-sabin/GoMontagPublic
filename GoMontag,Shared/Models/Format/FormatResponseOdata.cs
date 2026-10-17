using GoMontag.Shared.Enums;
using GoMontag.Shared.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using GoMontag.Shared.Models.FormatType;

namespace GoMontag.Shared.Models.Format
{
    public class FormatResponseOdata
    {
        public int Id { get; set; }
        public string CoverImageUrl { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public string? Slug { get; set; }
        public string Type { get; set; }
        public int FormatTypeId { get; set; }
        public string Edition { get; set; }
        public string? ModifierName { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? ModifierId { get; set; }
        public bool IsLocked { get; set; }
        public string? lockedReason { get; set; }
        public string? LockedByUser { get; set; }
        public string? CreatorId { get; set; }
        public ProductResponseJustCategoryId? Product { get; set; }
        public FormatTypeResponseNoCategory FormatType { get; set; }
        public int formatrevisionid { get; set; }
        public bool EditRequested { get; set; }
        public string InformationRequested { get; set; }
    }
}

