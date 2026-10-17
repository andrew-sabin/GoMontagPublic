using GoMontag.Shared.Entities;
using GoMontag.Shared.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GoMontag_Shared.Entities.Recommendations
{
    public class RecommendationList: BaseEntity
    {
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [StringLength(150)]
        public string Description { get; set; } = string.Empty;
        public string SharableLink { get; set; } = string.Empty;
        public bool PutOnFrontPage { get; set; } = false;
        public string UserId { get; set; }= string.Empty;
        public string UserName { get; set; } = string.Empty;
        public List<RecommendationItem> Items { get; set; } = new();
    }
}
