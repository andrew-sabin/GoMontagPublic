using GoMontag_Shared.Models.RecommendationList;
using MimeKit.Tnef;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Models.RecommendationItem
{
    public record struct RecommendedItemResponse
        (
        int Id,
        DateTime CreatedDate,
        DateTime ModifiedDate,
        string Notes,
        int Order,
        int ProductId,
        string ProductName,
        string ProductCoverImg,
        string UserName,
        ListResponse RecommendationList
        );
}
