using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GoMontag_WebApp.Client.ClientComponentServices
{
    public class AuthMessageHandler : DelegatingHandler
    {
        //private readonly ILocalStorageService _localStorageService;

        //public AuthMessageHandler(ILocalStorageService localStorageService)
        //{
        //    _localStorageService = localStorageService;
        //}

        //protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        //{
        //    var token = await _localStorageService.GetItemAsync<string>("authToken");
        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //    }
        //    return await base.SendAsync(request, cancellationToken);
        //}
    }
}