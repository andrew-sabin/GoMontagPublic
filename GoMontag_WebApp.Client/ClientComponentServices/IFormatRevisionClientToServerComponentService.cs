using GoMontag.Shared.Models.FormatRevision;

namespace GoMontag_WebApp.Client.ClientComponentServices
{
    public interface IFormatRevisionClientToServerComponentService
    {
        event Action? OnChange;
        List<FormatRevisionResponse> FormatRevisions { get; set; }
        Task<int> CountUserFormatRevisions(string userName);
        Task <List<FormatRevisionResponse>> GetRevisionsByFormatId(string formatHash);
        Task<FormatRevisionResponse?> GetMostRecentRevision(int formatId);
        Task<FormatRevisionResponse?> GetRecentRevisionForEditing(int formatId);
        Task<FormatRevisionResponse?> GetRevisionById(int revisionId);
        Task<FormatRevisionResponse> CreateRevision(FormatRevisionRequest request);
        Task DeleteRevision(int revisionId);
    }
}
