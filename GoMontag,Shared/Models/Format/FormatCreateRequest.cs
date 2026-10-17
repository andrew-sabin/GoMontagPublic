using GoMontag.Shared.Enums;
using GoMontag.Shared.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GoMontag.Shared.Models.Format
{
    public record struct FormatCreateRequest
    {
        public string Type { get; set; }
        public int FormatTypeId { get; set; }
        public string Edition { get; init; }
        public string? CoverImageUrl { get; init; }
        public DateOnly? ReleaseDate { get; init; }
        public int ProductId { get; init; }
    }
}
