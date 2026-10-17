using GoMontag_Shared.Helpers;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using System.Web;
namespace GoMontag_WebApp.Client.Helpers
{
    public class LinkProcessor
    {
        private readonly string _redirectLink;

        private readonly HashSet<string> _whitelist;

        public LinkProcessor(IOptions<ApiSettings> apiSettings, IOptions<UrlWhitelistOptions> whitelistOptions)
        {
            _redirectLink = $"{apiSettings.Value.BaseUrl}/redirect/";

            _whitelist = new HashSet<string>(
                whitelistOptions.Value.Domains ?? new List<string>(),
                StringComparer.OrdinalIgnoreCase
            );
        }

        public string ProcessHtml(string htmlContent)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);

            var links = doc.DocumentNode.SelectNodes("//a[@href]");
            if (links == null) return htmlContent;

            foreach (var link in links)
            {
                string originalHref = link.GetAttributeValue("href", string.Empty);
                if (string.IsNullOrWhiteSpace(originalHref))
                    continue;

                Uri uri;
                if (!Uri.TryCreate(originalHref, UriKind.Absolute, out uri))
                {
                    continue;
                }

                string host = uri.Host;
                string domain = host.StartsWith("www.", StringComparison.OrdinalIgnoreCase) ? host.Substring(4) : host;


                if (!_whitelist.Contains(domain))
                {
                    string redirectUrl = $"{_redirectLink}?url={HttpUtility.UrlEncode(originalHref)}";
                    link.SetAttributeValue("href", redirectUrl);
                }
            }

            return doc.DocumentNode.OuterHtml;
        }

        public string ProcessString(string link)
        {
            if (link.Contains("https://") || link.Contains("http://"))
            {
                string removedHttps = link.Replace("http://", "");
                removedHttps = removedHttps.Replace("https://", "");
                if (!_whitelist.Contains(removedHttps))
                {
                    Console.WriteLine("Link is not included in whitelist");
                    string redirectUrl = $"{_redirectLink}?url={link}";
                    return redirectUrl;
                }
                Console.WriteLine("Link is included in whitelist");
                return link;
            }

            if (!_whitelist.Contains(link))
            {
                Console.WriteLine("Link is not included in whitelist");
                string redirectUrl = $"{_redirectLink}?url={link}";
                return redirectUrl;
            }
            Console.WriteLine("Link is included in whitelist");
            return link;
        }
    }
}
