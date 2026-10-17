using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Models.User
{
    public class UserRemoveRoleRequest
    {
        public string userName { get; set; } = string.Empty;
        public string removeRole { get; set; } = string.Empty;
    }
}
