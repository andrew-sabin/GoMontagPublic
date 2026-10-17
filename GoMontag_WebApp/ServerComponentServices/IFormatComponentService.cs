using GoMontag.Shared.Models.Format;

namespace GoMontag_WebApp.ServerComponentServices
{
    public interface IFormatComponentService
    {
        event Action? OnChange;
        List<FormatResponse> Formats { get; set; }
        Task <List<FormatResponse>>GetFormatsByProductId (int productId);
        Task<List<FormatResponse>> GetRecentFormats(int amount);
        Task<List<FormatResponse>> GetRecentlyUpdatedFormats(int amount);
        Task<FormatResponse?> GetFormatByID(string formatIdHash);
        Task<FormatResponse> CreateFormat (FormatRequest format);
        Task<FormatResponse> UpdateFormat (string formatIdHash, FormatRequest format);
        Task RequestInfoForFormat(string formatIdHash, FormatRequest format);
        Task LockFormat (string formatIdHash, FormatRequest format);
        Task DeleteFormat (string formatIdHash);

    }
}
