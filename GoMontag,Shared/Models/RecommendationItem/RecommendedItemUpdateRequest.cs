using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Models.RecommendationItem
{
    public record struct RecommendedItemUpdateRequest
        (
        string Notes,
        int Order,
        int RecommendationListId,
        int ProductId,
        string ProductName,
        string ProductCoverImg
        );
}
