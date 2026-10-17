using GoMontag.Shared.Models.Format;

namespace GoMontag_WebApp.Client.ClientComponentServices
{
    public interface IFormatClientToServerComponentService
    {
        event Action? OnChange;
        List<FormatResponse> Formats { get; set; }
        Task<List<FormatResponse>> GetAllFormats();
        Task<List<FormatResponse>> GetAllInfoRequestedFormats();
        Task<int> CountAllFormats();
        Task<int> CountAllInformationRequestedFormats();
        Task<int> CountAllUserCreatedFormats(string userId);
        Task <List<FormatResponse>>GetFormatsByProductId (int productId);
        Task<List<FormatResponse>> GetRecentFormats(int amount);
        Task<List<FormatResponse>> GetRecentlyUpdatedFormats(int amount);
        Task<FormatResponse?> GetFormatByID(string formatIdHash);
        Task<FormatResponse> CreateFormat (FormatRequest format);
        Task<FormatResponse> UpdateFormat (string formatIdHash, FormatRequest format);
        Task RequestInformationForFormat(string formatIdHash, FormatRequest format);
        Task LockFormat (string formatIdHash, FormatRequest format);
        Task DeleteFormat (string formatIdHash);

    }
}
