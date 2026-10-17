using GoMontag.Shared.Models.Format;
using GoMontag.Shared.Models.FormatRevision;
using System.Net.Http.Json;

namespace GoMontag_WebApp.ServerComponentServices
{
    public class FormatRevisionComponentService : IFormatRevisionComponentService
    {
        private readonly HttpClient _httpClient;
        public FormatRevisionComponentService (IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ComponentApi");
        }

        public List<FormatRevisionResponse> FormatRevisions { get; set; } = new List<FormatRevisionResponse>();
        public event Action? OnChange;
        public async Task<FormatRevisionResponse> CreateRevision(FormatRevisionRequest request)
        {
            var createRequest = new FormatRevisionCreateRequest
            {
                IsInPrint = request.IsInPrint,
                Description = request.Description,
                OwnershipLevel = request.OwnershipLevel,
                IsAiAssisted = request.IsAiAssisted,
                AIAssistsWith = request.AIAssistsWith,
                FormatId = request.FormatId,
                PreviousRevisionId = request.PreviousRevisionId,
                RevisionNumber = request.RevisionNumber
            };
            var response = await _httpClient.PostAsJsonAsync("api/formatrevision", createRequest);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.Error.WriteLine($"CreateFormat failed: {response.StatusCode} - {response.ReasonPhrase} - {body}");
                throw new InvalidOperationException($"CreateFormat failed: {response.StatusCode} - {response.ReasonPhrase}");
            }
            var createdRevision = await response.Content.ReadFromJsonAsync<FormatRevisionResponse>();
            return createdRevision;


            throw new NotImplementedException();
        }

        public async Task DeleteRevision(int revisionId)
        {
            await _httpClient.DeleteAsync($"api/formatrevision/{revisionId}");
        }

        public async Task<FormatRevisionResponse?> GetMostRecentRevision(int formatId)
        {
            return await _httpClient.GetFromJsonAsync<FormatRevisionResponse>($"api/formatrevision/format/recent/{formatId}");
        }

        public async Task<FormatRevisionResponse?> GetRevisionById(int revisionId)
        {
            return await _httpClient.GetFromJsonAsync<FormatRevisionResponse>($"api/formatrevision/{revisionId}");
        }

        public async Task<List<FormatRevisionResponse>> GetRevisionsByFormatId(string formatHash)
        {
            List<FormatRevisionResponse>? revisions = await _httpClient.GetFromJsonAsync<List<FormatRevisionResponse>>($"api/formatrevision/format/{formatHash}");
            if (revisions == null)
                return null!;
            else
                return revisions;
        }
    }
}
