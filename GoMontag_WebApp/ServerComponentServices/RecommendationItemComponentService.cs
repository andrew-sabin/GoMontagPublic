using GoMontag_WebApp.ServerComponentServices.Interface;
using GoMontag_Shared.Entities.Recommendations;
using GoMontag_Shared.Models.RecommendationItem;
using Syncfusion.Blazor.RichTextEditor;
using System.Net.Http.Json;

namespace GoMontag_WebApp.ServerComponentServices
{
    public class RecommendationItemComponentService : IRecommendationItemComponentService
    {
        private readonly HttpClient _httpClient;
        private readonly IAuthComponentService _authService;

        public RecommendationItemComponentService(IHttpClientFactory httpClientFactory, IAuthComponentService authService)
        {
            _httpClient = httpClientFactory.CreateClient("ComponentApi");
            _authService = authService;
        }

        public List<RecommendedItemResponse> RecommendationItems { get; set; }

        public event Action? OnChange;

        public async Task CreateNewItem(RecommendedItemRequest recommendItem)
        {
            var itemRequest = new RecommendedItemCreateRequest
            {
                Notes = recommendItem.Notes,
                Order = recommendItem.Order,
                ProductId = recommendItem.ProductId,
                ProductName = recommendItem.ProductName,
                ProductCoverImg = recommendItem.ProductCoverImg
            };
            await _httpClient.PostAsJsonAsync("api/RecommendedItem", recommendItem);
        }

        public async Task DeleteListOfItems(List<RecommendedItemResponse> recommendedItems)
        {
            foreach (var item in recommendedItems)
            {
                await _httpClient.DeleteAsync($"api/RecommendedItem/{item.Id}");
            }
        }

        public async Task DeleteRecommendItem(int id)
        {
            await _httpClient.DeleteAsync($"api/RecommendedItem/{id}");
        }

        public async Task<List<RecommendedItemResponse>> GetItemsByRecommendationList(string listId)
        {
            var result = await _httpClient.GetFromJsonAsync<List<RecommendedItemResponse>>($"/api/RecommendedItem/list/{listId}");
            if (result == null)
                return new List<RecommendedItemResponse>();
            else
            {
                return result;
            }
        }

        public async Task<RecommendedItemResponse?> GetRecommendationItemByHash(string id)
        {
            return await _httpClient.GetFromJsonAsync<RecommendedItemResponse>($"/api/RecommendedItem/hash/{id}");
        }

        public async Task<RecommendedItemResponse?> GetRecommendationItemById(int id)
        {
            return await _httpClient.GetFromJsonAsync<RecommendedItemResponse>($"/api/RecommendedItem/{id}");
        }

        public async Task<List<RecommendedItemResponse?>> ListOfItemsUpdate(List<RecommendedItemRequest> recommendedItems)
        {
            List<RecommendedItemResponse> responses = new List<RecommendedItemResponse>();
            foreach(var item in recommendedItems)
            {
                
            }
            throw new NotImplementedException();
        }

        public async Task UpdateItem(int id, RecommendedItemRequest recommendedItem)
        {
            var reccItemUpdate = new RecommendedItemUpdateRequest
            {
                Notes = recommendedItem.Notes,
                Order = recommendedItem.Order,
                ProductId = recommendedItem.ProductId,
                ProductName = recommendedItem.ProductName,
                ProductCoverImg = recommendedItem.ProductCoverImg,
                RecommendationListId = recommendedItem.RecommendationListId
            };
            await _httpClient.PutAsJsonAsync($"api/RecommendedItem/{id}", reccItemUpdate);
        }

        public async Task UpdateHashedItem(string id, RecommendedItemRequest recommendedItem)
        {
            var reccItemUpdate = new RecommendedItemUpdateRequest
            {
                Notes = recommendedItem.Notes,
                Order = recommendedItem.Order,
                ProductId = recommendedItem.ProductId,
                ProductName = recommendedItem.ProductName,
                ProductCoverImg = recommendedItem.ProductCoverImg,
                RecommendationListId = recommendedItem.RecommendationListId
            };
            await _httpClient.PutAsJsonAsync($"/api/RecommendedItem/hash/{id}", reccItemUpdate);
        }
    }
}
