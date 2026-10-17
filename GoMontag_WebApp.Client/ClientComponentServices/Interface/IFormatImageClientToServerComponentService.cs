using GoMontag_Shared.Models.FormatImage;

namespace GoMontag_WebApp.Client.ClientComponentServices.Interface
{
    public interface IFormatImageClientToServerComponentService
    {
        event Action? OnChange;
        List<FormatImageResponse> FormatImages { get; set; }
        Task<List<FormatImageResponse>> GetAllFormatImages();
        Task<int> CountAllFormatImages();
        Task<List<FormatImageResponse>> GetAllFormatImagesByFormatId(string hashedFormatId);
        Task<FormatImageResponse> GetFormatImageById(string hash);
        Task<FormatImageResponse> GetFormatImageByUrl(string url);
        Task<FormatImageResponse> CreateFormatImage();
        Task DeleteFormatImage(string hash);

    }
}
