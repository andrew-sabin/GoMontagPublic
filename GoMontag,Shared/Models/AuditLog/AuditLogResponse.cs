using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Models.AuditLog
{
    public record struct AuditLogResponse
    {
        public int Id { get; init; }
        public string? UserName { get; init; }
        public string? UserEmail { get; init; }
        public string Action { get; init; }
        public string? TargetType { get; init; }
        public string? TargetId { get; init; }
        public string IpAddress { get; init; }
        public string? UserAgent { get; init; }
        public bool Success { get; init; }
        public DateTime TimeStampUtc { get; init; }
    }
}
