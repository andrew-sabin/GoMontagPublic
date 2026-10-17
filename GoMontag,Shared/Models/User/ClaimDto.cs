using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Models.User
{
    public record struct ClaimDto
    (
        string Type,
        string Value
    );
}
