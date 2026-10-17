using GoMontag_Shared.Models.RecommendationList;

namespace GoMontag_WebApp.Services.Interface
{
    public interface IRecommendationListService
    {
        Task<List<ListResponse>> GetAllRecommendationListsAsync();
        Task<List<ListResponse>> CreateNewRecommendationListAsync(ListCreateRequest listCreateRequest);
        Task<ListResponse?> GetRecommendationListById(int id);
        Task<List<ListResponse>?> GetListsByUserName(string userName);
        Task<List<ListResponse>> GetFrontPageLists(int amnt);
        Task<ListResponse?> UpdateRecommendationListAsync(int id, ListUpdateRequest listUpdateRequest);
        Task<List<ListResponse>?> GetListsByCategory(string categoryName);
        Task<List<ListResponse>?> DeleteRecommendationList(int id);
    }
}
