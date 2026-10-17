using System.Text.RegularExpressions;

namespace GoMontag_WebApp.Client.Helpers
{
    public class SanitizeImageMarkupHelper
    {
        private string SanitizeImageMarkup(string html)
        {
            if (string.IsNullOrWhiteSpace(html)) return html;
            // Remove width and height attributes
            html = Regex.Replace(
                html,
                @"\s*(width|height)\s*=\s*[""'][^""']*[""']",
                "",
                RegexOptions.IgnoreCase);
            // Strip width/height (and min/max variants) from the style attribute
            html = Regex.Replace(
                html,
                @"(style\s*=\s*[""'])([^""']*)([""'])",
                m => {
                    var cleaned = Regex.Replace(
                        m.Groups[2].Value,
                        @"(^|;)\s*(width|height|min-width|max-width|min-height|max-height)\s*:[^;]*",
                        "",
                        RegexOptions.IgnoreCase);
                    return m.Groups[1].Value + cleaned.Trim(';', ' ') + m.Groups[3].Value;
                },
                RegexOptions.IgnoreCase);
            return html;
        }
    }
}
