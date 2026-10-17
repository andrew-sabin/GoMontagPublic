namespace GoMontag_WebApp.Services
{
    public interface IUserContextService
    {
        string? GetUserId();
        string? GetUserName();
        Task<ApplicationUser?> GetUserAsync();
    }
}
