using GoMontag.Shared.Models.Roles;
using Microsoft.AspNetCore.Identity;

namespace GoMontag_WebApp.Services
{
    public interface IRoleService
    {
        Task CreateRoleAsync(string roleName);
        Task DeleteRoleAsync(string roleName);
        Task<List<IdentityRole>> GetAllRolesAsync();
    }
}
