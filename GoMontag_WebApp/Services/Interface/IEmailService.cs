using GoMontag_Shared.Models.Emails;

namespace GoMontag_WebApp.Services.Interface
{
    public interface IEmailService
    {
        Task<EmailResponse> SendEmailAsync(SendEmailRequest request);
        Task SendConfirmation(string? userName, string? code, string? email);
        Task SendResetEmail(string? userName, string? code, string? email);
        Task SendLoginNotification(string userName, string? code, string? email, string? ipAddress);
        Task SendEmailChangeInfo(string userName, string? prevEmail, string? newEmail, string? ipAddress);
    }
}
