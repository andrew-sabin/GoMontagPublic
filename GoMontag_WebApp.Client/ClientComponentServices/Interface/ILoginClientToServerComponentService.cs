using GoMontag.Shared.Models.Login;

namespace GoMontag_WebApp.Client.ClientComponentServices.Interface
{
    public interface ILoginClientToServerComponentService
    {
        Task<LoginResponse> Login(LoginRequest request);
    }
}
