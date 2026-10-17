namespace GoMontag_WebApp.Client.ClientComponentServices
{
    public interface IAccountClientComponentService
    {
        Task<bool> IsUserInRole(string userName, string roleName);
    }
}
