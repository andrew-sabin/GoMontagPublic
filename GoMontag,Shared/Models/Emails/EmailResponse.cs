using System;
using System.Collections.Generic;
using System.Text;

namespace GoMontag_Shared.Models.Emails
{
    public class EmailResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
