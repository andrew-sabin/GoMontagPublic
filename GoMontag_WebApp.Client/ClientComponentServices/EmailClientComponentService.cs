using GoMontag_Shared.Models.Emails;
using System.Net.Http.Json;

namespace GoMontag_WebApp.Client.ClientComponentServices
{
    public class EmailClientComponentService : IEmailClientComponentService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<EmailClientComponentService> _logger;

        public EmailClientComponentService(HttpClient httpClient, ILogger<EmailClientComponentService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public Task SendConfirmationEmailAsync(SendConfirmationEmailRequest request, string confirmationLink)
        {
            throw new NotImplementedException();
        }

        public async Task<EmailResponse> SendEmailAsync(SendEmailRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/email/send", request);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<EmailResponse>()
                        ?? new EmailResponse { Success = false, Message = "Invalid response" };
                }

                var errorResponse = await response.Content.ReadFromJsonAsync<EmailResponse>();
                return errorResponse ?? new EmailResponse
                {
                    Success = false,
                    Message = $"Error: {response.StatusCode}"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email");
                return new EmailResponse
                {
                    Success = false,
                    Message = "Network error occurred"
                };
            }
        }

        public async Task<bool> SendSimpleEmailAsync(string toEmail, string subject, string body)
        {
            var request = new SendEmailRequest 
            {
                ToEmail = toEmail,
                Subject = subject,
                Body = body,
                IsHtml = false
            };

            var response = await SendEmailAsync(request);
            return response.Success;
        }
    }
}
