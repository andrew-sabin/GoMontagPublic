using GoMontag.Shared.Entities.Extras;
using GoMontag_WebApp.Repositories.Interfaces;
using System.Net;
using Sqids;

namespace GoMontag_WebApp.Repositories
{
    public class FormatImageRepository: IFormatImagesRepository
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;
        public FormatImageRepository(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<FormatImage> CreateFormatImage(FormatImage formatImage)
        {
            formatImage.UserName = _userContextService.GetUserName();
            _context.Add(formatImage);
            await _context.SaveChangesAsync();
            return formatImage;
        }

        public async Task <List<FormatImage>> DeleteFormatImage(int formatImageId)
        {
            var dbFormatImage = await _context.FormatImages.FirstOrDefaultAsync(i => i.Id == formatImageId);
            if (dbFormatImage is null) 
            {
                return null;
            }
            var formatId = dbFormatImage.FormatId;

            if (dbFormatImage.isDeleted)
            {
                dbFormatImage.isDeleted = false;
                dbFormatImage.DeletedDate = null;
            }
            else
            {
                dbFormatImage.isDeleted = true;
                dbFormatImage.DeletedDate = DateTime.UtcNow;
            }
        
            await _context.SaveChangesAsync();

            return await GetFormatImagesByFormatId(formatId);
        }

        public async Task<FormatImage?> GetFormatImageById(int formatImageId)
        {
            var formatImage = await _context.FormatImages.Include(i => i.Format).FirstOrDefaultAsync(i => i.Id == formatImageId && i.isDeleted == false);
            return formatImage;
        }

        public async Task<FormatImage?> GetFormatImageByImageUrl(string ImageURL)
        {
            var decodedImageUrl = WebUtility.UrlDecode(ImageURL);
            var formatImage = await _context.FormatImages.Include(i => i.Format).FirstOrDefaultAsync(i => i.ImageUrl == decodedImageUrl);
            return formatImage;
        }

        public async Task<List<FormatImage>> GetFormatImagesAsync()
        {
            return await _context.FormatImages.Include(i => i.Format).ToListAsync();
        }

        public async Task<List<FormatImage>> GetFormatImagesByFormatId(int formatId)
        {
            return await _context.FormatImages.Where(i => i.FormatId == formatId && i.isDeleted == false)
                .Include(i => i.Format)
                .ToListAsync();
        }

        public async Task<FormatImage?> UpdateFormatImage(int formatImageId, FormatImage formatImage)
        {
            var dbFormatImage = await _context.FormatImages.FirstOrDefaultAsync(i => i.Id == formatImageId);
            if (dbFormatImage == null)
            {
                return null;
            }
            formatImage.LastUpdatedDate = DateTime.UtcNow;
            formatImage.LastUserUpdate = _userContextService.GetUserName();
            dbFormatImage.LastUserUpdate = formatImage.LastUserUpdate;
            dbFormatImage.LastUpdatedDate = formatImage.LastUpdatedDate;
            dbFormatImage.UpdatedAltText = formatImage.UpdatedAltText;
            dbFormatImage.Width = formatImage.Width;

            await _context.SaveChangesAsync();
            return dbFormatImage;
        }
    }
}
