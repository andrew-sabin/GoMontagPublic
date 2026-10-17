using GoMontag.Shared.Models.User;
using GoMontag_Shared.Models.User;

namespace GoMontag_WebApp.ServerComponentServices
{
    public interface IUserComponentService
    {
        event Action? OnChange;
        Task <UserResponse?> GetUserAsync(string userName);
        Task UpdateUserAsync(string userName, UserRequest userRequest);
        Task UpdateUserAdminAsync(string userName, UserRequest userRequest);
        Task <bool> IsUserInRole(string userName, string roleName);
        Task <UserResponse?> GetUserByIdAsync(string userId);
        Task UpdateUserRole(string userName, UserRoleAddRequest rolAdd , UserRemoveRoleRequest roleRemove);
        Task<IList<string>> GetUserRole(string userName);
    }
}
