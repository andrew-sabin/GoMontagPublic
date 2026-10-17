namespace GoMontag.Shared.Configuration
{
    public class Smtp2GoSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password {  get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
    }
}
