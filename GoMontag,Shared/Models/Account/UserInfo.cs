namespace GoMontag.Shared.Models.Account
{
    // Add properties to this class and update the server and client AuthenticationStateProviders
    // to expose more information about the authenticated user to the client.
    public class UserInfo
    {
        public required string UserId { get; set; }
        public required string Name { get; set; }
        public required List<string> Roles {  get; set; } = new List<string>();
    }
}
