using GoMontag_Shared.Models.Emails;

namespace GoMontag_WebApp.ServerComponentServices
{
    public interface IEmailComponentService
    {
        Task<EmailResponse> SendEmailAsync(SendEmailRequest request);
        Task SendConfirmationEmailAsync(SendConfirmationEmailRequest request, string confirmationLink);
        Task<bool> SendSimpleEmailAsync(string toEmail, string subject, string body);
    }
}
