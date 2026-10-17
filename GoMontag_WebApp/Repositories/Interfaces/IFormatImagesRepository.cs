using GoMontag.Shared.Entities.Extras;

namespace GoMontag_WebApp.Repositories.Interfaces
{
    public interface IFormatImagesRepository
    {
        Task<List<FormatImage>> GetFormatImagesAsync();
        Task<List<FormatImage>> GetFormatImagesByFormatId(int formatId);
        Task<FormatImage> CreateFormatImage(FormatImage formatImage);
        Task<FormatImage?> GetFormatImageById(int formatImageId);
        Task<FormatImage?> GetFormatImageByImageUrl(string ImageURL);
        Task<FormatImage?> UpdateFormatImage(int formatImageId, FormatImage formatImage);
        Task<List<FormatImage>> DeleteFormatImage(int formatImageId);

    }
}
