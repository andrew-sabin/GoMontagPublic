using GoMontag.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GoMontag.Shared.Models.Format
{
    public record struct FormatResponseNoProduct
    {
        public int Id { get; init; }
        public string? Type { get; init; }
        public int FormatTypeId { get; init; }
        public string Edition { get; init; }
        public DateOnly? ReleaseDate { get; init; }
        public string? CoverImageUrl { get; init; }
        public string? CreatorId { get; init; }
        public int ProductId { get; init; }
        public int formatrevisionid { get; init; }
        public bool EditRequested { get; init; }
        public string InformationRequested { get; init; }
    }
}

