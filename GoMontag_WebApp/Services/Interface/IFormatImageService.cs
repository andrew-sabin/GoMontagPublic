
namespace GoMontag_WebApp.Services.Interface
{
    public interface IFormatImageService
    {
        Task<List<FormatImageResponse>> GetAllFormatImages();
        Task<List<FormatImageResponse>> GetAllFormatImagesByFormatId(int formatId);
        Task<FormatImageResponse> CreateNewFormatImage(FormatImageCreateRequest formatImage);
        Task<FormatImageResponse> GetFormatImageById(int formatImageId);
        Task<FormatImageResponse> GetFormatImageByImageUrl(string formatImageUrl);
        Task<List<FormatImageResponse>> DeleteFormatImage(int formatImageId);
        Task<FormatImageResponse>? UpdateFormatImage(int formatImageId, FormatImageUpdateRequest formatImage);
    }
}
