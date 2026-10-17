using GoMontag.Shared.Models.Account;
using GoMontag.Shared.Models.Login;
using GoMontag.Shared.Models.Token;
using GoMontag_Shared.Models.Emails;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using static System.Net.WebRequestMethods;
using GoMontag_WebApp.Client.ClientComponentServices.Interface;

namespace GoMontag_WebApp.Client.ClientComponentServices
{
    public class LoginClientToServerComponentService : ILoginClientToServerComponentService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly NavigationManager _navigationManager;

        public LoginClientToServerComponentService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _navigationManager = navigationManager;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/login", request);
            if (result != null)
            {
                var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
                if (response.Success)
                {
                    await _authenticationStateProvider.GetAuthenticationStateAsync();
                }
                return response;
            }
            return new LoginResponse(false);
        }
    }
}
