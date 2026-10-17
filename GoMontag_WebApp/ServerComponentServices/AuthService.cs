using Blazored.LocalStorage;
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

namespace GoMontag_WebApp.ServerComponentServices
{
    public class AuthService : IAuthComponentService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly NavigationManager _navigationManager;
        public AuthService(IHttpClientFactory httpClientFactory,
            AuthenticationStateProvider authenticationStateProvider, NavigationManager navigationManager)
        {
            _httpClient = httpClientFactory.CreateClient("ComponentApi");
            _authenticationStateProvider = authenticationStateProvider;
            _navigationManager = navigationManager;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/login", request);
            if (result != null)
            {
                var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
                if (response.Success && response.Token != null)
                {
                    await _authenticationStateProvider.GetAuthenticationStateAsync();
                }
                return response;
            }
            return new LoginResponse(false);
        }

        public async Task Logout()
        {
            await _httpClient.PostAsync("api/login/logout", null);
            await _authenticationStateProvider.GetAuthenticationStateAsync();
            _navigationManager.NavigateTo("login");
        }

        public async Task<LoginResponse> RefreshToken()
        {
            var result = await _httpClient.PostAsync("api/login/refresh", null);
            if (result != null)
            {
                var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
                if (response.Success && response.Token != null)
                {

                }
                return response;
            }
            return new LoginResponse(false);
        }

        public async Task<AccountRegistrationResponse> Register(AccountRegistrationRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/account/register", request);
            if (result != null)
            {
                var response = await result.Content.ReadFromJsonAsync<AccountRegistrationResponse>();
                return response;
            }
            return new AccountRegistrationResponse(false);
        }

        public async Task EnsureTokenValidAsync()
        {
            throw new NotImplementedException(); 

        }

        public async Task<ForgotPasswordResponse> RequestPasswordReset(ForgotPasswordRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/account/forgot-password", request);
            if (result != null)
            {
                var response = await result.Content.ReadFromJsonAsync<ForgotPasswordResponse>();
                return response;    
            }
            else
            {
                return new ForgotPasswordResponse
                {
                    Success = false,
                    Message = "Could not connect to server. Please try again later."
                };
            }
        }
    }
}
