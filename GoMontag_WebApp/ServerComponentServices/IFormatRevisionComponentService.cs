using GoMontag.Shared.Models.FormatRevision;

namespace GoMontag_WebApp.ServerComponentServices
{
    public interface IFormatRevisionComponentService
    {
        event Action? OnChange;
        List<FormatRevisionResponse> FormatRevisions { get; set; }
        Task <List<FormatRevisionResponse>> GetRevisionsByFormatId(string formatHash);
        Task<FormatRevisionResponse?> GetMostRecentRevision(int formatId);
        Task<FormatRevisionResponse?> GetRevisionById(int revisionId);
        Task<FormatRevisionResponse> CreateRevision(FormatRevisionRequest request);
        Task DeleteRevision(int revisionId);
    }
}
