using GoMontag.Shared.Entities.Extras;
using GoMontag_WebApp.Repositories.Interfaces;
using Mapster;

namespace GoMontag_WebApp.Services
{
    public class FormatImageService : IFormatImageService
    {
        private readonly IFormatImagesRepository _formatImageRepo;

        public FormatImageService(IFormatImagesRepository formatImagesRepo)
        {
            _formatImageRepo = formatImagesRepo;
        }

        public async Task<FormatImageResponse> CreateNewFormatImage(FormatImageCreateRequest formatImage)
        {
            var newEntry = formatImage.Adapt<FormatImage>();
            var result = await _formatImageRepo.CreateFormatImage(newEntry);
            return result.Adapt<FormatImageResponse>();
        }

        public async Task<List<FormatImageResponse>> DeleteFormatImage(int formatImageId)
        {
            Console.WriteLine($"delete image called with {formatImageId}");
            var result = await _formatImageRepo.DeleteFormatImage(formatImageId);
            if (result == null) { return null; }
            return result.Adapt<List<FormatImageResponse>>();
        }

        public async Task<List<FormatImageResponse>> GetAllFormatImages()
        {
            var result = await _formatImageRepo.GetFormatImagesAsync();
            return result.Adapt<List<FormatImageResponse>>();
        }

        public async Task<List<FormatImageResponse>> GetAllFormatImagesByFormatId(int formatId)
        {
            var result = await _formatImageRepo.GetFormatImagesByFormatId(formatId);
            return result.Adapt<List<FormatImageResponse>>();
        }

        public async Task<FormatImageResponse> GetFormatImageById(int formatImageId)
        {
            var result = await _formatImageRepo.GetFormatImageById(formatImageId);
            if (result is null)
            {
                return null;
            }
            return result.Adapt<FormatImageResponse>();
        }

        public async Task<FormatImageResponse> GetFormatImageByImageUrl(string formatImageUrl)
        {
            var result = await _formatImageRepo.GetFormatImageByImageUrl(formatImageUrl);
            if (result is null)
            {
                return null;
            }
            return result.Adapt<FormatImageResponse>();
        }

        public async Task<FormatImageResponse>? UpdateFormatImage(int formatImageId, FormatImageUpdateRequest formatImage)
        {
            var updatedEntry = formatImage.Adapt<FormatImage>();
            var result = await _formatImageRepo.UpdateFormatImage(formatImageId, updatedEntry);
            if (result == null)
            {
                return null;
            }
            return result.Adapt<FormatImageResponse>();
        }
    }
}
