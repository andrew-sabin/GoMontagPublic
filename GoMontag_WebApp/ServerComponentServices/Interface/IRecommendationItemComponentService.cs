using GoMontag_Shared.Entities.Recommendations;
using GoMontag_Shared.Models.RecommendationItem;

namespace GoMontag_WebApp.ServerComponentServices.Interface
{
    public interface IRecommendationItemComponentService
    {
        event Action? OnChange;
        List <RecommendedItemResponse> RecommendationItems { get; set; }
        Task<List<RecommendedItemResponse>> GetItemsByRecommendationList(string listId);
        Task <RecommendedItemResponse?> GetRecommendationItemById(int id);
        Task<RecommendedItemResponse?> GetRecommendationItemByHash(string id);
        Task CreateNewItem(RecommendedItemRequest recommendItem);
        Task UpdateItem(int id, RecommendedItemRequest recommendedItem);
        Task UpdateHashedItem(string id, RecommendedItemRequest recommendedItem);
        Task <List<RecommendedItemResponse?>> ListOfItemsUpdate (List<RecommendedItemRequest> recommendedItems);
        Task DeleteRecommendItem(int id);
        Task DeleteListOfItems(List<RecommendedItemResponse> recommendedItems);
    }
}
