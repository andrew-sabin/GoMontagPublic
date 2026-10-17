using GoMontag.Shared.Models.Account;
using GoMontag.Shared.Models.Login;
using GoMontag.Shared.Models.Token;
using GoMontag_Shared.Models.Emails;

namespace GoMontag_WebApp.Client.ClientComponentServices
{
    public interface IAuthClientToServerComponentService
    {
        Task<AccountRegistrationResponse> Register(AccountRegistrationRequest request);
        Task<LoginResponse> Login(LoginRequest request);
        Task<ForgotPasswordResponse> RequestPasswordReset(ForgotPasswordRequest request);
        Task Logout();
        Task<LoginResponse> RefreshToken();
        Task EnsureTokenValidAsync();
    }
}
