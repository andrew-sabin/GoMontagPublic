using GoMontag_Shared.Entities.Logs;
using GoMontag_Shared.Models.AuditLog;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System.Linq.Dynamic.Core;

namespace GoMontag_WebApp.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IEmailService _emailService;
        //private readonly IConfiguration _configuration;

        public AuditLogService(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager )
        {
            _context = applicationDbContext;
            _userManager = userManager;
        }
        public async Task CreateNewAuditLog(AuditLogCreateRequest request)
        {
            var auditLog = request.Adapt<AuditLog>();
            _context.Add(auditLog);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AuditLog>> GetLogInAuditLogsByUserName(string userName)
        {
            return _context.AuditLogs.Where(a =>  a.UserName == userName).ToList();
        }

        public async Task<AuditLog?> GetMostRecentAuditLogForUser(string userName)
        {
            var mostRecent = _context.AuditLogs.OrderBy(a => a.TimeStampUtc).LastOrDefault(a => a.UserName == userName);
            return mostRecent;
        }
    }
}
