using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag.Shared.Models.Category
{
    public record struct CategoryResponseJustIdAndName
        (int Id, string? Name);
}
