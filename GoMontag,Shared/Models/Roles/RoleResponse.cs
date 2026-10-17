using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoMontag.Shared.Models.Roles
{
    public record struct RoleResponse(Guid Id, string Name, string NormalizedName);
}
