namespace GoMontag_WebApp.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> GetUserByUserNameAsync(string UserName);
    }
}
