using GoMontag.Shared.Entities;
using GoMontag.Shared.Models.Store;
using System.Net.Http.Json;

namespace GoMontag_WebApp.Client.ClientComponentServices
{
    public class StoreWASMComponentService : IStoreWASMComponentService
    {
        private readonly HttpClient _httpClient;
        public event Action? OnChange;
        public List<StoreResponse> Stores { get; set; } = new List<StoreResponse>();
        public StoreWASMComponentService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ComponentApi");
        }
        public async Task LoadAllStoresAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<StoreResponse>>("api/store");
            if (result != null)
            {
                Stores = result;
                OnChange?.Invoke();
            }
        }
        public async Task<StoreResponse?> GetStoreByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/store/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            return await _httpClient.GetFromJsonAsync<StoreResponse>($"api/store/{id}");
        }
        public async Task<StoreResponse?> GetStoreBySlugAsync(string slug)
        {
            var response = await _httpClient.GetAsync($"api/store/slug/{slug}");
            if (response == null)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            return await _httpClient.GetFromJsonAsync<StoreResponse>($"api/store/slug/{slug}");
        }

        public async Task CreateStoreAsync(StoreRequest store)
        {
            var storeRequest = new StoreCreateRequest
            {
                Name = store.Name,
                LogoURL = store.LogoURL,
                Industry = store.Industry,
                Online = store.Online,
                Street = store.Street,
                City = store.City,
                State = store.State,
                PostalCode = store.PostalCode,
                Country = store.Country,
                Phone = store.Phone,
                Email = store.Email,
                WebsiteURL = store.WebsiteURL
            };
            await _httpClient.PostAsJsonAsync("api/store", storeRequest);
        }

        public async Task UpdateStoreAsync(int id, StoreRequest store)
        {
            var updaterequest = new StoreUpdateRequest
            {
                Name = store.Name,
                Slug = store.Slug,
                LogoURL = store.LogoURL,
                Industry = store.Industry,
                Online = store.Online,
                Street = store.Street,
                City = store.City,
                State = store.State,
                PostalCode = store.PostalCode,
                Country = store.Country,
                Phone = store.Phone,
                Email = store.Email,
                WebsiteURL = store.WebsiteURL
            };
            await _httpClient.PutAsJsonAsync($"api/store/{id}", updaterequest);
        }

        public async Task DeleteStoreAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/store/{id}");
        }
    }
}
