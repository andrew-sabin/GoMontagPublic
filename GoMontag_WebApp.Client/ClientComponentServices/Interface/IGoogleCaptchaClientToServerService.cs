using GoMontag_Shared.Models.Captcha;
using GoMontag_WebApp.Client.Helpers;
using GoogleCaptchaComponent.Models;

namespace GoMontag_WebApp.Client.ClientComponentServices.Interface
{
    public interface IGoogleCaptchaClientToServerService
    {
        Task<ServerSideCaptchaValidationResultModel> ValidateCaptcha(ServerSideCaptchaValidationRequestModel requestModel);
    }
}
