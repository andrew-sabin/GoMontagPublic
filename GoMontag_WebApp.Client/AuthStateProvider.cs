using Blazored.LocalStorage;
using GoMontag.Shared.Models.Account;
using GoMontag_Shared.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace GoMontag_WebApp.Client
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;

        public AuthStateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<UserDto>("api/account/me");
                if (response == null || string.IsNullOrEmpty(response.Name))
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, response.Name)
                };

                if (response.Claims != null)
                {
                    foreach (var c in response.Claims)
                    {
                        // Avoid duplicate name claim
                        if (c.Type == ClaimTypes.Name || c.Type == "name") continue;
                        claims.Add(new Claim(c.Type, c.Value));
                    }
                }

                var identity = new ClaimsIdentity(claims, "cookie");
                var user = new ClaimsPrincipal(identity);

                var authState = new AuthenticationState(user);
                NotifyAuthenticationStateChanged(Task.FromResult(authState));
                return authState;
            }
            catch
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            //private static readonly Task<AuthenticationState> _unauthenticatedTask =
            //    Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));


            //public override Task<AuthenticationState> GetAuthenticationStateAsync()
            //{
            //    if (!persistentState.TryTakeFromJson<UserInfo>(nameof(UserInfo), out var userInfo) || userInfo is null)
            //    {
            //        return _unauthenticatedTask;
            //    }

            //    var claims = new List<Claim> { 
            //        new Claim(ClaimTypes.NameIdentifier, userInfo.UserId),
            //        new Claim(ClaimTypes.Name, userInfo.Name)};

            //    claims.AddRange(userInfo.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            //    return Task.FromResult(
            //        new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims,
            //            authenticationType: nameof(AuthStateProvider)))));
            //}

        }
    }
}
