using GoMontag_Shared.Entities.Recommendations;
using GoMontag_Shared.Models.RecommendationList;

namespace GoMontag_WebApp.ServerComponentServices.Interface
{
    public interface IRecommendationListComponentService
    {
        event Action? OnChange;
        List<ListResponse> RecommendationLists { get; set; }
        Task<List<ListResponse>> GetRecommendListsByUserName(string userName);
        Task<ListResponse?> GetRecommendListById (string id);
        Task<List<ListResponse>> GetFrontPagesRecommendLists(int amnt);
        Task CreateNewList(ListRequest recommendList);
        Task UpdateList(string id, ListRequest recommendList);
        Task DeleteRecommendList(string id);
    }
}
