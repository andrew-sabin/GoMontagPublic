using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Models.RecommendationList
{
    public record struct ListUpdateRequest
        (
        string Name,
        string Description,
        string SharableLink,
        bool PutOnFrontPage
        );
}
