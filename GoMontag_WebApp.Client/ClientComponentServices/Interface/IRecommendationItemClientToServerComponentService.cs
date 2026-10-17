using GoMontag_Shared.Entities.Recommendations;
using GoMontag_Shared.Models.RecommendationItem;

namespace GoMontag_WebApp.Client.ClientComponentServices.Interface
{
    public interface IRecommendationItemClientToServerComponentService
    {
        event Action? OnChange;
        List <RecommendedItemResponse> RecommendationItems { get; set; }
        Task<List<RecommendedItemResponse>> GetItemsByRecommendationList(string listId);
        Task<List<RecommendedItemResponse>> GetItemsByProductId(int productId);
        Task<List<RecommendedItemResponse>> GetItemsByRecommendationListByPage(string listHash, int pageIndex, int pageSize);
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
