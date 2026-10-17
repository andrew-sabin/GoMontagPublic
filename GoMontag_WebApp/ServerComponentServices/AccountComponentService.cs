using System.Net.Http.Json;

namespace GoMontag_WebApp.ServerComponentServices
{
    public class AccountComponentService : IAccountComponentService
    {
        private readonly HttpClient _httpClient;
        public AccountComponentService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ComponentApi");
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
