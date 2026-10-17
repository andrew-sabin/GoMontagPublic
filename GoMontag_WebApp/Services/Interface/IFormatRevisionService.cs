using GoMontag.Shared.Models.FormatRevision;

namespace GoMontag_WebApp.Services.Interface
{
    public interface IFormatRevisionService
    {
        public Task<List<FormatRevisionResponse>> GetFormatRevisionsAsync();
        public Task<FormatRevisionResponse?> GetFormatRevisionByIdAsync(int formatRevisionId);
        public Task<FormatRevisionResponse> CreateNewFormatRevisionAsync(FormatRevisionCreateRequest formatRevisionCreateRequest);
        public Task<List<FormatRevisionResponse>?> DeleteFormatRevisionAsync (int formatRevisionId);
        public Task <List<FormatRevisionResponse>> GetFormatRevisionsByFormatId (int formatId);
        public Task <FormatRevisionResponse?> GetLatestFormatRevisionByFormatId (int formatId);
        public Task<FormatRevisionResponse?> GetMostRecentFormatRevisionForEditingAsync(int formatId);
    }
}
