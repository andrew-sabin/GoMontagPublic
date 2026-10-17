using GoMontag.Shared.Models.Format;
using GoMontag.Shared.Models.FormatType;

namespace GoMontag_WebApp.ServerComponentServices
{
    public interface IFormatTypeComponentService
    {
        event Action? OnChange;
        List<FormatTypeResponse> FormatTypes { get; }
        Task<FormatTypeResponse?> GetFormatTypeByID(int formatTypeId);
        Task<FormatTypeResponse?> GetFormatTypeByHash(string formatTypeHash);
        Task<List<FormatTypeResponse>> GetFormatTypesByCategoryId(int categoryId);
        Task CreateFormatType(FormatTypeRequest formatType);
        Task UpdateFormatType(string formatTypeHash, FormatTypeRequest formatType);
        Task DeleteFormatType(string formatTypeHash);
    }
}
