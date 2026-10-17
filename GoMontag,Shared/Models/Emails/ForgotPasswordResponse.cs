using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Models.Emails
{
    public record struct ForgotPasswordResponse(bool Success, IEnumerable<string>? Errors = null, string? Message=null);
}
