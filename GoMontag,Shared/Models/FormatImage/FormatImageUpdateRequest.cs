using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Models.FormatImage
{
    public record struct FormatImageUpdateRequest
    (
        string UpdatedAltText,
        int Width
    );
}
