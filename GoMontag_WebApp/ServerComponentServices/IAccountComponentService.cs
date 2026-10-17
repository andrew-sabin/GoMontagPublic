namespace GoMontag_WebApp.ServerComponentServices
{
    public interface IAccountComponentService
    {
        Task<bool> IsUserInRole(string userName, string roleName);
    }
}
