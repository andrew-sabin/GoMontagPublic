using GoMontag.Shared.Entities.Extras;
using GoMontag.Shared.Entities.Interfaces;
using GoMontag.Shared.Enums;
using GoMontag_Shared.Entities.Recommendations;
using GoMontag_Shared.Entities.Revisions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMontag.Shared.Entities
{
    [Index(nameof(Slug), IsUnique = true)]
    public class Format : SoftDeleteableEntity
    {
        public string? CoverImageUrl { get; set; } = string.Empty;
        // public string? CoverImageUser { get; set; } //used for the user responsible for Uploading a new CoverImage
        [Column(TypeName = "varchar(100)")]
        public string Slug { get; set; } = string.Empty;
        // Format Type
        public string? Type { get; set; } // Value set by FormatType
        public int FormatTypeId { get; set; }
        public FormatType? FormatType { get; set; }
        // Edition
        public string? Edition { get; set; }
        // Release Date
        public DateOnly? ReleaseDate { get; set; }
        public int ProductId { get; set; } /* Begining of Relational Attributes */
        public Product? Product { get; set; }
        public string? CreatorName { get; set; }
        public string? CreatorId { get; set; } // Guid of the user who created this format entry
        public bool EditRequested { get; set; } // More Information Requested
        [MaxLength(300)]
        public string? InformationRequested { get; set; }
        public string? ModifierName { get; set; }
        public string? ModifierId { get; set; }
        public List<Availability> Availabilities { get; set; } = new List<Availability>();
        public int formatrevisionid { get; set; } //Current Revision
        public List<FormatRevision> FormatRevisions { get; set; } = new();
    }
}
