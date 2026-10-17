using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Models.RecommendationList
{
    public record struct ListResponse
        (
            int Id,
            string Name,
            string Description,
            string SharableLink,
            string UserName,
            string UserId,
            bool PutOnFrontPage
        );
}
