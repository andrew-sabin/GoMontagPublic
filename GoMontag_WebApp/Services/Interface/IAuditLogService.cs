using GoMontag_Shared.Entities.Logs;
using GoMontag_Shared.Models.AuditLog;

namespace GoMontag_WebApp.Services.Interface
{
    public interface IAuditLogService
    {
        public Task CreateNewAuditLog(AuditLogCreateRequest request);
        public Task<List<AuditLog>> GetLogInAuditLogsByUserName(string userName);
        public Task<AuditLog?> GetMostRecentAuditLogForUser(string userName);
    }
}
