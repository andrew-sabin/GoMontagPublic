using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Models.FormatImage
{
    public class FormatImageRequest
    {
        public int Id { get; set; }
        public string ImageName { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string AltText { get; set; } = string.Empty;
        public int? Width { get; set; }
        public int FormatId { get; set; }
        public int CreatedDate { get; set; }
        public string? LastUserUpdate { get; set; } = string.Empty;
        public DateTime? LastUpdatedDate { get; set; } = null;
        public string? UpdatedAltText { get; set; } = string.Empty;
    }
}
