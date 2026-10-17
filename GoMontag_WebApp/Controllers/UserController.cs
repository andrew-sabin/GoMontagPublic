using GoMontag.Shared.Models.User;
using GoMontag_WebApp.Helpers.Logger;
using GoMontag_WebApp.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GoMontag_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        public UserController(IUserService userService, ILogger<UserController> logger, UserManager<ApplicationUser> userManager,
            IEmailService emailService)
        {
            _userService = userService;
            _logger = logger;
            _userManager = userManager;
            _emailService = emailService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult>? GetUserById(string userId)
        {
            var result = await _userService.GetUserById(userId);
            if (result == null)
            {
                return NotFound($"User with this ID {userId} was not found");
            }
            else
            {
                return Ok(result);
            }

        }

        [AllowAnonymous]
        [HttpGet("{userName}")]
        public async Task<IActionResult>? GetUser(string userName)
        {
            var result = await _userService.GetAsync(userName);
            if (result == null)
            {
                return NotFound($"User with this username {userName} was not found");
            }
            else
            {
                return Ok(result);
            }
                
        }
        [Authorize]
        [HttpPut("{userName}")]
        public async Task<IActionResult>? UpdateUser(string userName, UserUpdateRequest userRequest)
        {
            var currentUserName = User?.Identity?.Name;
            var currentUserInfo = await _userManager.FindByNameAsync(userName);
            var currentUserEmail = currentUserInfo.Email;
            

            if (userName != currentUserName)
            {
                return StatusCode(403, "Username in the URL does not match the username in the request body.");
            }
            if (currentUserEmail != userRequest.Email)
            {
                var remoteIp = HttpContext.Connection.RemoteIpAddress;
                if (remoteIp.IsIPv4MappedToIPv6)
                {
                    remoteIp = remoteIp.MapToIPv4();
                }
                string ipAddress = remoteIp.ToString();
                _emailService.SendEmailChangeInfo(userName, currentUserEmail, userRequest.Email, ipAddress);
            }
            var result = await _userService.UpdateUser(userName, userRequest);
            if (result == null)
            {
                return NotFound($"User with this username {userName} was not found");
            }
            else
            {
                var remoteIp = HttpContext.Connection.RemoteIpAddress;
                if (remoteIp.IsIPv4MappedToIPv6)
                {
                    remoteIp = remoteIp.MapToIPv4();
                }
                string ipAddress = remoteIp.ToString();
                string userNameGrab = HttpContext.User.Identity.Name;

                var date = DateTime.UtcNow;
                if (ipAddress != null && userName != null)
                    _logger.EditAccountInfo(date, userNameGrab, userRequest.DisplayName, userRequest.Email, ipAddress);
                return Ok(result);
            }
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPut("admin/{userName}")]
        public async Task<IActionResult>? AdminUpdateUser(string userName, AdminUserUpdateRequest userRequest)
        {
            var result = await _userService.AdminUpdateUser(userName, userRequest);
            if (result == null)
            {
                return NotFound($"User with this username {userName} was not found");
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
