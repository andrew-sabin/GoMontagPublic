using GoMontag.Shared.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMontag.Shared.Entities.Extras
{
    [Index(nameof(ImageUrl), IsUnique = true)]
    public class FormatImage : BaseEntity
    {
        public string? UserName {get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        
        public string ImageUrl { get; set; } = string.Empty;
        public int? Width { get; set; } // Uploaded Width
        public string AltText { get; set; } = string.Empty;

        /* Relationships M:1 */
        public int FormatId { get; set; }
        public Format? Format { get; set; }

        /* Soft Delete */
        public bool isDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; } = null;

        /* Update Info */
        public string? UpdatedAltText { get; set; } = string.Empty;
        public string? LastUserUpdate { get; set; } = string.Empty;
        public DateTime? LastUpdatedDate { get; set;} = null;
    }
}
