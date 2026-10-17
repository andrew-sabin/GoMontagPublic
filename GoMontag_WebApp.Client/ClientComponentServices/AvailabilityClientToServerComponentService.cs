using GoMontag.Shared.Models.Availability;
using System.Net.Http;
using System.Net.Http.Json;

namespace GoMontag_WebApp.Client.ClientComponentServices
{
    public class AvailabilityClientToServerComponentService : IAvailabilityClientToServerComponentService
    {
        private readonly HttpClient _httpClient;

        public AvailabilityClientToServerComponentService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ComponentApi");
        }

        public List<AvailabilityResponse> Availabilities { get; set; }

        public event Action? OnChange;

        public async Task CreateAvailability(AvailabilityRequest availability)
        {
            var createRequest = new AvailabilityCreateRequest
            {
                FormatId = availability.FormatId,
                StoreId = availability.StoreId,
                URL = availability.URL,
                CurrencyCode = availability.CurrenceyCode,
                UnitSold = availability.UnitSold,
                Price = availability.Price
            };
            await _httpClient.PostAsJsonAsync("api/availability", createRequest);
        }

        public async Task DeleteAvailability(int availabilityID)
        {
            await _httpClient.DeleteAsync($"api/availability/{availabilityID}");
        }

        public async Task<List<AvailabilityResponse>> GetAvailabilitiesByFormatId(int formatId)
        {
            List<AvailabilityResponse>? result = null;
            if (formatId <= 0)
            {
                result = await _httpClient.GetFromJsonAsync<List<AvailabilityResponse>>("api/availability");
                return result;
            }
            else
            {
                result = await _httpClient.GetFromJsonAsync<List<AvailabilityResponse>>($"api/availability/format/{formatId}");
                return result;
            }
        }

        public async Task<AvailabilityResponse?> GetAvailabilityByID(int availId)
        {
            return await _httpClient.GetFromJsonAsync<AvailabilityResponse>($"api/Availability/{availId}");
        }

        public Task UpdateAvailability(int availabilityID, AvailabilityRequest availability)
        {
            var updateRequest = new AvailabilityUpdateRequest
            {
                FormatId = availability.FormatId,
                StoreId = availability.StoreId,
                URL = availability.URL,
                CurrencyCode = availability.CurrenceyCode,
                UnitSold = availability.UnitSold,
                Price = availability.Price
            };
            return _httpClient.PutAsJsonAsync($"api/availability/{availabilityID}", updateRequest);
        }
    }
}
