using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Models.RecommendationItem
{
    public class RecommendedItemRequest
    {
        public int Id { get; set; }
        public string Notes { get; set;  }
        public int Order { get; set; }
        public int RecommendationListId { get; set; }
        public int ProductId { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string ProductCoverImg { get; set; }
    }
}
