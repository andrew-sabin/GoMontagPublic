using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Entities.Logs
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string Action {  get; set; } = string.Empty;
        public string? TargetType { get; set; }
        public string? TargetId { get; set; }
        public string IpAddress { get; set; } = "";
        public string? UserAgent { get; set; }
        public bool Success { get; set; }
        public DateTime TimeStampUtc { get; set; } = DateTime.UtcNow;
    }
}
