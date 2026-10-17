using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Models.RecommendationList
{
    public record struct ListCreateRequest
        (
        string Name,
        string Description
        );
}
