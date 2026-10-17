using GoMontag.Shared.Models.Account;
using GoMontag_Shared.Models.Emails;
using GoMontag_Shared.Models.User;
using GoMontag_WebApp.Helpers.Logger;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace GoMontag_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, UserManager<ApplicationUser> userManager, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _userManager = userManager;
            _logger = logger;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountRegistrationRequest request)
        {
            var response = await _accountService.RegisterAsync(request);
            if (response.Success)
            {
                var remoteIp = HttpContext.Connection.RemoteIpAddress;
                if (remoteIp.IsIPv4MappedToIPv6)
                {
                    remoteIp = remoteIp.MapToIPv4();
                }
                string ipAddress = remoteIp.ToString();
                string userName = HttpContext.User.Identity.Name;

                var date = DateTime.UtcNow;
                if (ipAddress != null && userName != null)
                    _logger.Register(date, userName, request.Email, ipAddress);
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userName, string code)
        {
            if (userName == null || code == null)
                return BadRequest("Invalid Email Confirmation Request");

            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return BadRequest("Invalid User Name");

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {
                return Ok(new {Message = "Email has been confirmed! You can log in now."});
            }
            return BadRequest("Error confirming your email.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Await the async service call so we return the actual response object
            var response = await _accountService.ResetPasswordAsync(request);

            if (response.Success)
            {
                var remoteIp = HttpContext.Connection.RemoteIpAddress;
                if (remoteIp.IsIPv4MappedToIPv6)
                {
                    remoteIp = remoteIp.MapToIPv4();
                }
                string ipAddress = remoteIp.ToString();
                string userName = HttpContext.User.Identity.Name;

                var date = DateTime.UtcNow;
                if (ipAddress != null && userName != null)
                    _logger.ResetPassword(date, userName, ipAddress);
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _accountService.ForgotPasswordAsync(request.Username, request.Email);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("me")]
        public IActionResult Me()
        {
            if (!User.Identity?.IsAuthenticated ?? false)
                return Unauthorized();

            var claims = User.Claims.Select(c => new { Type = c.Type, Value = c.Value }).ToList();
            return Ok(new
            {
                Name = User.Identity?.Name,
                Claims = claims
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Identity.Application");
            return Ok();
        }


        [Authorize (Roles = "Admin")]
        [HttpPost("role")]
        public async Task<IActionResult> AssignRole([FromBody] UserRoleAddRequest request)
        {
            await _accountService.AssignRole(request.userName, request.addRole);
            return Ok();
        }
        [Authorize (Roles = "Admin")]
        [HttpPost("role/remove")]
        public async Task<IActionResult> RemoveRole([FromBody] UserRemoveRoleRequest request)
        {
            await _accountService.RemoveRole(request.userName, request.removeRole);
            return Ok();
        }
        [AllowAnonymous]
        [HttpGet("role/check")]
        public async Task<IActionResult> IsUserInRole(string userName, string roleName)
        {
            var isInRole = await _accountService.IsUserInRole(userName, roleName);
            return Ok(isInRole);
        }
        [AllowAnonymous]
        [HttpGet("role/get")]
        public async Task<IActionResult> GetRole(string userName) 
        { 
            var userRoles = await _accountService.GetRole(userName);
            return Ok(userRoles);
        }
    }
}
