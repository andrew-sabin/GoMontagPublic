using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Models.AuditLog
{
    public record struct AuditLogCreateRequest
    (
    string? UserName,
    string? UserEmail,
    string Action,
    string? TargetType,
    string? TargetId,
    string IpAddress,
    string? UserAgent,
    bool Success,
    DateTime TimeStampUtc
    );
    
}
