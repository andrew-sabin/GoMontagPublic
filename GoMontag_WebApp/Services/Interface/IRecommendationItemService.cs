using GoMontag_Shared.Models.RecommendationItem;

namespace GoMontag_WebApp.Services.Interface
{
    public interface IRecommendationItemService
    {
        Task<List<RecommendedItemResponse>> GetAllRecommendationItemsAsync();
        Task<List<RecommendedItemResponse>> CreateNewRecommendedItem(RecommendedItemCreateRequest request);
        Task<RecommendedItemResponse?> GetRecommendedItemById(int id);
        Task<List<RecommendedItemResponse>?> GetRecommendedItemsByListId(int listId);
        Task<List<RecommendedItemResponse>> GetRecommendedItemsByProductId(int productId);
        Task<List<RecommendedItemResponse>?> DisplayItemList(int listId, int pageNumber, int pageSize);
        Task<RecommendedItemResponse?> UpdatedRecommendedItem(int id, RecommendedItemUpdateRequest newRecommendedItem);
        Task<List<RecommendedItemResponse>?> DeleteRecommendedItem(int id);
    }
}
