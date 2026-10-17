
using GoMontag.Shared.Models.Format;
using GoMontag.Shared.Models.Product;
using System.Net.Http.Json;

namespace GoMontag_WebApp.ServerComponentServices
{
    public class FormatComponentService : IFormatComponentService
    {
        private readonly HttpClient _httpClient;

        public FormatComponentService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ComponentApi");
        }

        public List<FormatResponse> Formats { get; set; } = new List<FormatResponse>();
        public event Action? OnChange;

        public async Task<FormatResponse> CreateFormat(FormatRequest format)
        {
            var createRequest = new FormatCreateRequest
            {
                FormatTypeId = format.FormatTypeId,
                Type = format.Type,
                Edition = format.Edition,
                ReleaseDate = format.ReleaseDate,
                CoverImageUrl = format.CoverImageUrl,
                ProductId = format.ProductId
            };
            var response = await _httpClient.PostAsJsonAsync("api/format", createRequest);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.Error.WriteLine($"CreateFormat failed: {response.StatusCode} - {response.ReasonPhrase} - {body}");
                throw new InvalidOperationException($"CreateFormat failed: {response.StatusCode} - {response.ReasonPhrase}");
            }
            var createdFormat = await response.Content.ReadFromJsonAsync<FormatResponse>();
            return createdFormat;
        }

        public async Task DeleteFormat(string formatIdHash)
        {
            await _httpClient.DeleteAsync($"api/format/{formatIdHash}");
        }

        public async Task<FormatResponse?> GetFormatByID(string formatIdHash)
        {
            var response = await _httpClient.GetAsync($"api/format/{formatIdHash}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadFromJsonAsync<FormatResponse>();
                return data;
            }
            else
            {
                return (null);
            }
        }

        public async Task<List<FormatResponse>> GetFormatsByProductId(int productId)
        {
            if (productId <= 0)
            {
                var result = await _httpClient.GetFromJsonAsync<List<FormatResponse>>("api/format");
                if (result == null)
                    return new List<FormatResponse>();
                else
                {
                    return result;
                }
            }
            else
            {
                var result = await _httpClient.GetFromJsonAsync<List<FormatResponse>>($"api/format/product/{productId}");
                if (result == null)
                    return new List<FormatResponse>();
                else
                {
                    return result;
                }
            }
        }

        public async Task<List<FormatResponse>> GetRecentFormats(int amount)
        {
            List<FormatResponse>? result = await _httpClient.GetFromJsonAsync<List<FormatResponse>>($"api/format/recent?amount={amount}");
            if (result != null)
                return result;
            else
                return null!;
        }

        public async Task<List<FormatResponse>> GetRecentlyUpdatedFormats(int amount)
        {
            List<FormatResponse>? result = await _httpClient.GetFromJsonAsync<List<FormatResponse>>($"api/format/recent/updated?amount={amount}");
            if (result != null)
                return result;
            else
                return null!;
        }

        public async Task LockFormat(string formatIdHash, FormatRequest format)
        {
            var lockRequest = new FormatLockRequest
            {
                IsLocked = format.IsLocked,
                lockedReason = format.lockedReason
            };
            await _httpClient.PutAsJsonAsync($"api/format/lock/{formatIdHash}", lockRequest);
        }

        public async Task RequestInfoForFormat(string formatIdHash, FormatRequest format)
        {
            var infoRequest = new FormatInformationRequest
            {
                EditRequested = format.EditRequested,
                InformationRequested = format.InformationRequested
            };
            await _httpClient.PutAsJsonAsync($"api/format/request/{formatIdHash}", infoRequest);
        }

        public async Task<FormatResponse> UpdateFormat(string formatIdHash, FormatRequest format)
        {
            var updateRequest = new FormatUpdateRequest
            {
                FormatTypeId = format.FormatTypeId,
                Type = format.Type,
                Edition = format.Edition,
                ReleaseDate = format.ReleaseDate,
                CoverImageUrl = format.CoverImageUrl,
                ProductId = format.ProductId,
                formatrevisionid = format.formatrevisionid
            };
            var response = await _httpClient.PutAsJsonAsync($"api/format/{formatIdHash}", updateRequest);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.Error.WriteLine($"UpdateFormat failed: {response.StatusCode} - {response.ReasonPhrase} - {body}");
                throw new InvalidOperationException($"UpdateFormat failed: {response.StatusCode} - {response.ReasonPhrase}");
            }
            var updatedFormat = await response.Content.ReadFromJsonAsync<FormatResponse>();
            return updatedFormat;
        }
    }
}
