using GoMontag.Shared.Models.Login;
using GoMontag.Shared.Models.Token;

namespace GoMontag_WebApp.Services
{
    public interface ILoginService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<LoginResponse> RefreshTokensAsync(string refreshToken);
  
    }
}
