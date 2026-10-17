using GoMontag.Shared.Models.Product;
using GoMontag.Shared.Models.User;
using GoMontag_Shared.Models.User;
using System.Net.Http.Json;

namespace GoMontag_WebApp.ServerComponentServices
{
    public class UserComponentService : IUserComponentService
    {
        public event Action? OnChange;
        private readonly HttpClient _httpClient;
        private readonly IAuthComponentService _authService;
        public UserComponentService(IHttpClientFactory httpClientFactory, IAuthComponentService authService)
        {
            _httpClient = httpClientFactory.CreateClient("ComponentApi");
            _authService = authService;
        }

        public async Task<UserResponse?> GetUserAsync(string userName)
        {
            var response = await _httpClient.GetAsync($"api/user/{userName}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<UserResponse>();
                return data;
            }
            else
            {
                return null;
            }
        }

        public async Task UpdateUserAsync(string userName, UserRequest userRequest)
        {
            var updateRequest = new UserUpdateRequest
            {
                DisplayName = userRequest.DisplayName,
                ProfileImageURL = userRequest.ProfileImageURL,
                WebsiteURL = userRequest.WebsiteURL,
                Bio = userRequest.Bio,
                DateOfBirth = userRequest.DateOfBirth,
                Email = userRequest.Email
            };
            await _authService.EnsureTokenValidAsync();
            await _httpClient.PutAsJsonAsync($"api/user/{userName}", updateRequest);
        }

        public Task<bool> IsUserInRole(string userName, string roleName)
        {
            var response = _httpClient.GetFromJsonAsync<bool>($"api/account/role/check?userName={userName}&roleName={roleName}");
            return response;
        }

        public async Task UpdateUserAdminAsync(string userName, UserRequest userAdminUpdateRequest)
        {
            var updateRequest = new AdminUserUpdateRequest
            {
                DisplayName = userAdminUpdateRequest.DisplayName,
                UserName = userAdminUpdateRequest.UserName,
                ProfileImageURL = userAdminUpdateRequest.ProfileImageURL,
                Bio = userAdminUpdateRequest.Bio,
                WebsiteURL = userAdminUpdateRequest.WebsiteURL,
                IsVerified = userAdminUpdateRequest.IsVerified,
                IsBanned = userAdminUpdateRequest.IsBanned,
                BanEndDate = userAdminUpdateRequest.BanEndDate,
                BanReason = userAdminUpdateRequest.BanReason
            };
            await _authService.EnsureTokenValidAsync();
            await _httpClient.PutAsJsonAsync($"api/user/admin/{userName}", updateRequest);
        }

        public Task<UserResponse?> GetUserByIdAsync(string userId)
        {
            var response = _httpClient.GetFromJsonAsync<UserResponse?>($"api/user?userId={userId}");
            return response;
        }


        public async Task<IList<string>> GetUserRole(string userName)
        {
            var response = await _httpClient.GetFromJsonAsync<IList<string>>($"/api/Account/role/get?userName={userName}");
            if (response == null)
                return [];
            return response;
        }

        public async Task UpdateUserRole(string userName, UserRoleAddRequest roleAdd, UserRemoveRoleRequest roleRemove)
        {
            if (!string.IsNullOrEmpty(roleRemove.removeRole))
                await _httpClient.PostAsJsonAsync($"/api/Account/role/remove", roleRemove);

            if(!string.IsNullOrEmpty(roleAdd.addRole))
                await _httpClient.PostAsJsonAsync($"/api/Account/role", roleAdd);
        }
    }
}
