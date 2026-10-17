using GoMontag.Shared.Models.Format;
using GoMontag.Shared.Models.FormatRevision;
using System.Net.Http.Json;

namespace GoMontag_WebApp.Client.ClientComponentServices
{
    public class FormatRevisionClientToServerComponentService : IFormatRevisionClientToServerComponentService
    {
        private readonly HttpClient _httpClient;
        public FormatRevisionClientToServerComponentService (IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ComponentApi");
        }

        public List<FormatRevisionResponse> FormatRevisions { get; set; } = new List<FormatRevisionResponse>();
        public event Action? OnChange;

        public async Task<int> CountUserFormatRevisions(string userName)
        {
            var response = await _httpClient.GetAsync($"odata/FormatRevision/$count?filter=ModifierName eq '{userName}'");

            if (response.IsSuccessStatusCode)
            {
                var countString = await response.Content.ReadAsStringAsync();

                return int.Parse(countString);
            }
            return 0;
        }

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
        }

        public async Task DeleteRevision(int revisionId)
        {
            await _httpClient.DeleteAsync($"api/formatrevision/{revisionId}");
        }

        public async Task<FormatRevisionResponse?> GetMostRecentRevision(int formatId)
        {
            return await _httpClient.GetFromJsonAsync<FormatRevisionResponse>($"api/formatrevision/format/recent/{formatId}");
        }

        public async Task<FormatRevisionResponse?> GetRecentRevisionForEditing(int formatId)
        {
            return await _httpClient.GetFromJsonAsync<FormatRevisionResponse>($"api/formatrevision/format/recent/edit/{formatId}");
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
