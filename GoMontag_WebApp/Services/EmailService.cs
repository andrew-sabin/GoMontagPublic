using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;
using GoMontag_Shared.Models.Emails;
using GoMontag.Shared.Configuration;

namespace GoMontag_WebApp.Services
{
    public class EmailService: IEmailService
    {
        private readonly Smtp2GoSettings _settings;
        private readonly ILogger<EmailService> _logger;
        private readonly IConfiguration _configuration;
        
        public EmailService(IOptions<Smtp2GoSettings> settings, ILogger<EmailService> logger, IConfiguration configuration)
        {
            _settings = settings.Value;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task SendConfirmation(string? userName, string? code, string? email)
        {
            var clientBaseUrl = _configuration["ClientApp:BaseUrl"] ?? "https://localhost:7168";

            var callbackUrl = $"{clientBaseUrl}/confirm-email?user={userName}&c={code}";

            var confirmationEmail = new SendEmailRequest
            {
                ToEmail = email!,
                ToName = userName!,
                Subject = "GoMontag Account Registration Confirmation",
                Body = $"""
                        <!DOCTYPE html>
                        <html>
                        <head>
                            <meta charset="utf-8" />
                        </head>
                        <body>
                            <img src="https://images.gmntgstrg.com/defaultandresourceimages/gomontag-02_fit.svg" 
                            style="max-width: 91px; max-height: 45px;">
                            <h2>GoMontag Confirmation Email Code</h2>
                            <p>Welcome Aboard {userName}!</p>
                            <p>Here is your confirmation email code:</p>
                            <div class="code"><a href={callbackUrl}>{callbackUrl}</a></div>
                            <p>If you didn't request this, you can ignore this email.</p>
                            <p>Thanks,<br>Andy</p>
                        </body>
                        </html>
                        """,
                IsHtml = true
            };
            await SendEmailAsync(confirmationEmail);
        }

        public async Task<EmailResponse> SendEmailAsync(SendEmailRequest request)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));

                message.To.Add(new MailboxAddress(
                    string.IsNullOrEmpty(request.ToName) ? request.ToEmail : request.ToName,
                    request.ToEmail
                ));

                message.Subject = request.Subject;

                var bodyBuilder = new BodyBuilder();
                if (request.IsHtml)
                    bodyBuilder.HtmlBody = request.Body;
                else
                    bodyBuilder.TextBody = request.Body;

                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort,
                                         SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_settings.Username, _settings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                _logger.LogInformation($"Email sent successfully to {request.ToEmail}");

                return new EmailResponse
                {
                    Success = true,
                    Message = "Email sent successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send email to {request.ToEmail}");
                return new EmailResponse
                {
                    Success = false,
                    Message = "Failed to send email"
                };

            }
        }

        public async Task SendEmailChangeInfo(string userName, string? prevEmail, string? newEmail, string? ipAddress)
        {
            var loginNotification = new SendEmailRequest
            {
                ToEmail = prevEmail!,
                ToName = userName,
                Subject = "GoMontag: Change in Email",
                Body = $"""
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset="utf-8" />
                </head>
                <body>
                    <img src="https://images.gmntgstrg.com/defaultandresourceimages/gomontag-02_fit.svg" 
                    style="max-width: 91px; max-height: 45px;">
                    <h2>Your Email Has Been Changed</h2>
                    <p>Hey there {userName}!</p>
                    <h3>Your Email Has Been Changed To:</h3>
                    <h2>{newEmail}</h2>
                    <h3>From The IP Address Of:</h3>
                    <h2><strong>{ipAddress}</strong></h2>
                    <p><strong>If this was not your doing please contact me by this email or send a message with:</strong></p>
                    <p><strong><a href="www.gomontag.com/contact">https://www.gomontag.com/contact</a></strong></p>
                    <p>If this is an authorized change in email address, you can ignore this email.</p>
                    <p>Thanks,<br>Andy</p>
                </body>
                </html>
                """,
                IsHtml = true
            };
            await SendEmailAsync(loginNotification);
        }

        public async Task SendLoginNotification(string userName, string? code, string? email, string? ipAddress)
        {
            var clientBaseUrl = _configuration["ClientApp:BaseUrl"] ?? "https://localhost:7168";

            var callbackUrl = $"{clientBaseUrl}/reset-email?user={userName}&c={code}";

            var loginNotification = new SendEmailRequest 
            { 
                ToEmail = email!,
                ToName = userName,
                Subject = "GoMontag: Logged in From New Location",
                Body = $"""
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset="utf-8" />
                </head>
                <body>
                    <img src="https://images.gmntgstrg.com/defaultandresourceimages/gomontag-02_fit.svg" 
                    style="max-width: 91px; max-height: 45px;">
                    <h2>Logged In From New Location</h2>
                    <p>Hey there {userName}!</p>
                    <h3>You have logged into your account from a new IP address: </h3>
                    <h2><strong>{ipAddress}</strong></h2>
                    <p><strong>If this IP address does not match up with your current IP please reset your email here:</strong></p>
                    <div class="code"><a href={callbackUrl}>{callbackUrl}</a></div>
                    <p>To check your current IP address please use <a href="https://whatismyipaddress.com/">WhatIsMyIPAddress</a></p>
                    <p>If this is from an IP address you are currently using, you can ignore this email.</p>
                    <p>Thanks,<br>Andy</p>
                </body>
                </html>
                """,
                IsHtml = true
            };
            await SendEmailAsync(loginNotification);
        }

        public async Task SendResetEmail(string? userName, string? code, string? email)
        {
            var clientBaseUrl = _configuration["ClientApp:BaseUrl"] ?? "https://localhost:7168";

            var callbackUrl = $"{clientBaseUrl}/reset-email?user={userName}&c={code}";

            var resetEmail = new SendEmailRequest
            {
                ToEmail = email!,
                ToName = userName!,
                Subject = "GoMontag Password Reset Confirmation",
                Body = $"""
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset="utf-8" />
                </head>
                <body>
                    <img src="https://images.gmntgstrg.com/defaultandresourceimages/gomontag-02_fit.svg" 
                    style="max-width: 91px; max-height: 45px;">
                    <h2>Password Reset Code</h2>
                    <p>Hey there {userName}!</p>
                    <p>Use the code below to reset your password:</p>
                    <div class="code"><a href={callbackUrl}>{callbackUrl}</a></div>
                    <p>If you didn't request this, you can ignore this email.</p>
                    <p>Thanks,<br>Andy</p>
                </body>
                </html>
                """,
                IsHtml = true
            };

            await SendEmailAsync(resetEmail);
        }
    }
}
