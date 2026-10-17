using GoMontag.Shared.Entities;
using GoMontag.Shared.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GoMontag_Shared.Entities.Recommendations
{
    public class RecommendationItem : BaseEntity
    {
        [MaxLength(300)]
        public string Notes { get; set; } = string.Empty;
        public int Order {  get; set; }
        public int RecommendationListId { get; set; }
        public required RecommendationList RecommendationList { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductCoverImg { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
