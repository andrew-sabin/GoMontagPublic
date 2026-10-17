using GoMontag.Shared.Models.Login;
using GoMontag.Shared.Models.Token;
using GoMontag_Shared.Models.AuditLog;
using GoMontag_WebApp.Helpers.Logger;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoMontag_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IAuditLogService _auditLogService;
        private readonly ILogger<LoginController> _logger;
        private readonly IEmailService _emailService;
        private readonly UserManager<ApplicationUser> _userManager;
        public LoginController(ILoginService loginService, ILogger<LoginController> logger, IAuditLogService auditLogService,
            IEmailService emailService, UserManager<ApplicationUser> userManager)
        {
            _loginService = loginService;
            _logger = logger;
            _auditLogService = auditLogService;
            _emailService = emailService;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var result = await _loginService.LoginAsync(request);
            if (result.Success)
            {
                var remoteIp = HttpContext.Connection.RemoteIpAddress;
                if (remoteIp.IsIPv4MappedToIPv6)
                {
                    remoteIp = remoteIp.MapToIPv4();
                }
                var user = HttpContext.User;
                string ipAddress = remoteIp.ToString();
                string userName = user.Identity.Name;
                string userEmail = "";
                if (!string.IsNullOrEmpty(userName))
                {
                    var userFull = await _userManager.FindByNameAsync(userName);
                    userEmail = userFull.Email;
                }

                var date = DateTime.UtcNow;
                if (ipAddress != null && userName != null)
                {
                    var auditLogs = await _auditLogService.GetLogInAuditLogsByUserName(userName);
                    if (auditLogs != null)
                    {
                        var hasIP = false;
                        foreach (var log in auditLogs)
                        {
                            if (log.IpAddress == ipAddress && log.Action == "Login") { hasIP = true; break; }
                        }
                        if (!hasIP)
                        {
                            var foundUser = await _userManager.FindByNameAsync(userName);
                            var code = await _userManager.GeneratePasswordResetTokenAsync(foundUser);
                            await _emailService.SendLoginNotification(userName, code, foundUser.Email, ipAddress);
                        }
                    }
                    _logger.LogInLogger(date, userName, ipAddress);
                    var auditLog = new AuditLogCreateRequest
                    {
                        UserName = userName,
                        UserEmail = userEmail,
                        Action = "Login",
                        IpAddress = ipAddress,
                        Success = true,
                        TimeStampUtc = DateTime.UtcNow
                    };
                    await _auditLogService.CreateNewAuditLog(auditLog);
                }
                    
                return Ok(new LoginResponse(true));
            }
            else
            {
                return Unauthorized(result);
            }
        }
        [HttpPost("refresh")]
        public async Task<ActionResult<LoginResponse>> RefreshTokens()
        {
            return Unauthorized(new LoginResponse(false)
            {
                Error = "Refresh token no longer implemented."
            });
        }

        [HttpPost("logout")]
        public async Task<ActionResult> LogoutCookies()
        {
            await HttpContext.SignOutAsync("Identity.Application");
            return Ok();
        }
    }
}
