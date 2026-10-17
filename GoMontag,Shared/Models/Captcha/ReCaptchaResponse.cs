using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GoMontag_Shared.Models.Captcha
{
    public class ReCaptchaResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
