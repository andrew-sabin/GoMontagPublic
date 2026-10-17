using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Models.FormatImage
{
    public record struct FormatImageCreateRequest
    (
        string ImageName,
        string ImageUrl,
        string AltText,
        int Width,
        int FormatId
    );
}
