using GoMontag.Shared.Enums;
using GoMontag.Shared.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GoMontag.Shared.Models.Format
{
    public record struct FormatInformationRequest(bool EditRequested, string? InformationRequested);
}
