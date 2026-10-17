using GoMontag_Shared.Models.Emails;

namespace GoMontag_WebApp.Client.ClientComponentServices
{
    public interface IEmailClientComponentService
    {
        Task<EmailResponse> SendEmailAsync(SendEmailRequest request);
        Task SendConfirmationEmailAsync(SendConfirmationEmailRequest request, string confirmationLink);
        Task<bool> SendSimpleEmailAsync(string toEmail, string subject, string body);
    }
}
