using GoMontag_Shared.Models.Captcha;
using GoMontag_WebApp.Client.ClientComponentServices.Interface;
using GoMontag_WebApp.Client.Helpers;
using GoogleCaptchaComponent.Models;
using System.Net.Http.Json;

namespace GoMontag_WebApp.Client.ClientComponentServices
{

    public class GoogleCaptchaClientToServerService : IGoogleCaptchaClientToServerService
    {
        private readonly HttpClient _httpClient;
        public GoogleCaptchaClientToServerService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ComponentApi");
        }

        public async Task<ServerSideCaptchaValidationResultModel> ValidateCaptcha(ServerSideCaptchaValidationRequestModel requestModel)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Captcha/validate", requestModel.CaptchaResponse);

                var result = await response.Content.ReadFromJsonAsync<CaptchaVerificationResponse>();

                if (response.IsSuccessStatusCode && result != null && result.Success)
                {
                    return new ServerSideCaptchaValidationResultModel(true, "CAPTCHA validation successful.");
                }
                else
                {
                    var errorMessage = result?.Message ?? "CAPTCHA validation failed on the server.";
                    return new ServerSideCaptchaValidationResultModel(false, errorMessage);
                }
            }
            catch (HttpRequestException ex)
            {
                return new ServerSideCaptchaValidationResultModel(false, $"Network Error: {ex.Message}");
            }
        }
    }
}
