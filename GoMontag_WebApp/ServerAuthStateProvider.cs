using GoMontag.Shared.Models.Account;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Security.Claims;

namespace GoMontag_WebApp
{
    // This is a server-side AuthenticationStateProvider that revalidates the security stamp for the connected user
    // every 30 minutes an interactive circuit is connected. It also uses PersistentComponentState to flow the
    // authentication state to the client which is then fixed for the lifetime of the WebAssembly application.
    internal sealed class ServerAuthStateProvider : ServerAuthenticationStateProvider, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly PersistentComponentState _state;
        private readonly IdentityOptions _options;

        private readonly PersistingComponentStateSubscription subscription;

        private Task<AuthenticationState>? authenticationStateTask;

        public ServerAuthStateProvider(
            PersistentComponentState state,
            IOptions<IdentityOptions> optionsAccessor)
            : base()
        {
            _state = state;
            _options = optionsAccessor.Value;

            AuthenticationStateChanged += OnAuthenticationStateChanged;
            subscription = state.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveWebAssembly);
        }

        private void OnAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            authenticationStateTask = task;
        }

        private async Task OnPersistingAsync()
        {
            if (authenticationStateTask is null)
            {
                throw new UnreachableException($"Authentication state not set in {nameof(OnPersistingAsync)}().");
            }

            var authenticationState = await authenticationStateTask;
            var principal = authenticationState.User;

            if (principal.Identity?.IsAuthenticated == true)
            {
                var userId = principal.FindFirst(_options.ClaimsIdentity.UserIdClaimType)?.Value;
                var name = principal.FindFirst(_options.ClaimsIdentity.UserNameClaimType)?.Value;
                var roles = principal.FindAll(_options.ClaimsIdentity.RoleClaimType).Select(c => c.Value).ToList();

                if (userId != null && name != null)
                {
                    _state.PersistAsJson(nameof(UserInfo), new UserInfo
                    {
                        UserId = userId,
                        Name = name,
                        Roles = roles
                    });
                }
            }
        }

        public void Dispose()
        {
            AuthenticationStateChanged -= OnAuthenticationStateChanged;
        }
    }
}
