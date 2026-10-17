using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Models.FormatImage
{
    public class FormatImageResponse
    {
        public int Id { get; init; }
        public string? ImageName { get; init; } = string.Empty;
        public string? UserName { get; init; } = string.Empty;
        public string ImageUrl { get; init; } = string.Empty;
        public string AltText { get; init; } = string.Empty;
        public int? Width { get; init; }
        public int FormatId { get; init; }
        public DateTime CreatedDate { get; init; }
        public string? UpdatedAltText { get; init; } = string.Empty;
        public string? LastUserUpdate { get; init; } = string.Empty;
        public DateTime? LastUpdatedDate { get; init; } = null;
    }
}
