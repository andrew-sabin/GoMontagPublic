using GoMontag_Shared.Models.RecommendationList;
using System.Net.Http.Json;
using GoMontag_WebApp.Client.ClientComponentServices.Interface;

namespace GoMontag_WebApp.Client.ClientComponentServices
{
    public class RecommendationListClientToServerComponentService : IRecommendationListClientToServerComponentService
    {
        private readonly HttpClient _httpClient;
        public RecommendationListClientToServerComponentService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ComponentApi");
        }

        public List<ListResponse> RecommendationLists { get; set; } = new List<ListResponse>();

        public event Action? OnChange;

        public async Task CreateNewList(ListRequest recommendList)
        {
            var listRequest = new ListCreateRequest
            {
                Name = recommendList.Name,
                Description = recommendList.Description
            };
            await _httpClient.PostAsJsonAsync("api/recommendationlist", listRequest);
        }

        public async Task DeleteRecommendList(string id)
        {
            await _httpClient.DeleteAsync($"api/recommendationlist/{id}");
        }

        public async Task<List<ListResponse>> GetFrontPagesRecommendLists(int amnt)
        {
            var results = await _httpClient.GetFromJsonAsync<List<ListResponse>>($"api/recommendationlist/frontpage/{amnt}");
            if (results == null)
                return new List<ListResponse>();
            else
            {
                return results;
            }
        }

        public async Task<ListResponse?> GetRecommendListById(string id)
        {
            var result = await _httpClient.GetAsync($"api/recommendationlist/{id}");
            if (!result.IsSuccessStatusCode)
                return null;
            return await result.Content.ReadFromJsonAsync<ListResponse?>();
        }

        public async Task<List<ListResponse>> GetRecommendListsByUserName(string? userName)
        {
            var results = await _httpClient.GetFromJsonAsync<List<ListResponse>>($"api/recommendationlist/users/{userName}");
            if (results == null)
                return new List<ListResponse>();
            else
            {
                return results;
            }

        }

        public Task UpdateList(string id, ListRequest recommendList)
        {
            var updateRequest = new ListUpdateRequest
            {
                Name = recommendList.Name,
                Description = recommendList.Description,
                SharableLink = recommendList.SharableLink
            };
            return _httpClient.PutAsJsonAsync($"api/recommendationlist/{id}", updateRequest);
        }
    }
}
