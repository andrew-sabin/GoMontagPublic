using System.Net.Http.Json;

namespace GoMontag_WebApp.Client.ClientComponentServices
{
    public class AccountClientComponentService : IAccountClientComponentService
    {
        private readonly HttpClient _httpClient;
        public AccountClientComponentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> IsUserInRole(string userName, string roleName)
        {
            var response = await _httpClient.GetAsync($"api/account/role/check?userName={userName}&roleName={roleName}");
            if (response == null)
            {
                return false;
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
