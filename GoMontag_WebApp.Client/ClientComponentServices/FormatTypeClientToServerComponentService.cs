using GoMontag.Shared.Entities;
using GoMontag.Shared.Models.Format;
using GoMontag.Shared.Models.FormatType;

using System.Net.Http;
using System.Net.Http.Json;

namespace GoMontag_WebApp.Client.ClientComponentServices
{
    public class FormatTypeClientToServerComponentService : IFormatTypeClientToServerComponentService
    {
        private readonly HttpClient _httpClient;

        public FormatTypeClientToServerComponentService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ComponentApi");
        }
        public List<FormatTypeResponse> FormatTypes {  get; set; } = new List<FormatTypeResponse>();
        public event Action? OnChange;

        public async Task CreateFormatType(FormatTypeRequest formatType)
        {

            var createRequest = new FormatTypeCreateRequest
            {
                Name = formatType.Name,
                Description = formatType.Description ?? string.Empty,
                CategoryId = formatType.CategoryId,
                ImageUrl = formatType.ImageUrl,
                HasIssue = formatType.HasIssue,
                Issue = formatType.Issue ?? string.Empty,
                IssueURL = formatType.IssueURL ?? string.Empty
            };
            var response = await _httpClient.PostAsJsonAsync("api/FormatType", createRequest);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.Error.WriteLine($"CreateFormat failed: {response.StatusCode} - {response.ReasonPhrase} - {body}");
                throw new InvalidOperationException($"CreateFormat failed: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }

        public async Task DeleteFormatType(string formatTypeHash)
        {
            await _httpClient.DeleteAsync($"api/formattype/{formatTypeHash}");
        }

        public async Task<List<FormatTypeResponse>> GetAllFormatTypes()
        {
            var result = await _httpClient.GetFromJsonAsync<List<FormatTypeResponse>>("api/formattype");
            if (result == null)
                return new List<FormatTypeResponse>();

            return result;
        }

        public async Task<FormatTypeResponse?> GetFormatTypeByHash(string formatTypeHash)
        {
            return await _httpClient.GetFromJsonAsync<FormatTypeResponse>($"api/formattype/hash/{formatTypeHash}");
        }

        public async Task<FormatTypeResponse?> GetFormatTypeByID(int formatTypeId)
        {
            return await _httpClient.GetFromJsonAsync<FormatTypeResponse>($"api/formattype/{formatTypeId}");
        }

        public async Task<List<FormatTypeResponse>> GetFormatTypesByCategoryId(int categoryId)
        {
            var formatTypesByCat = new List<FormatTypeResponse>();

            formatTypesByCat = await _httpClient.GetFromJsonAsync<List<FormatTypeResponse>>(($"api/formattype/category/{categoryId}"));

            if(formatTypesByCat == null)
                return new List<FormatTypeResponse>();

            return formatTypesByCat;
        }

        public async Task UpdateFormatType(string formatTypeHash, FormatTypeRequest formatType)
        {

            var updateRequest = new FormatTypeUpdateRequest
            {
                Name = formatType.Name,
                Description = formatType.Description ?? string.Empty,
                CategoryId = formatType.CategoryId,
                ImageUrl = formatType.ImageUrl,
                HasIssue = formatType.HasIssue,
                Issue = formatType.Issue ?? string.Empty,
                IssueURL = formatType.IssueURL ?? string.Empty
            };
            var response = await _httpClient.PutAsJsonAsync($"api/formattype/{formatTypeHash}", updateRequest);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.Error.WriteLine($"UpdateFormat failed: {response.StatusCode} - {response.ReasonPhrase} - {body}");
                throw new InvalidOperationException($"UpdateFormat failed: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }
}
