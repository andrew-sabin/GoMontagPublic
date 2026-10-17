using GoMontag_Shared.Entities.Recommendations;

namespace GoMontag_WebApp.Repositories.Interfaces
{
    public interface IRecommendationItemsRepo
    {
        Task<List<RecommendationItem>> GetAllRecommendationItemsAsync();
        Task<List<RecommendationItem>> CreateNewRecommendationItem(RecommendationItem item);
        Task<RecommendationItem?> GetRecommendationItemById(int id);
        Task<List<RecommendationItem>?> GetItemsByListId(int listId);
        Task<List<RecommendationItem>> GetItemsByProductId(int productId);
        Task<List<RecommendationItem>> DisplayFullListOfItems(int listId, int pageNumber, int pageSize);
        Task<RecommendationItem?> UpdateRecommendationItem(int id,RecommendationItem item);
        Task<List<RecommendationItem>?> DeleteRecommendationItem(int id);
    }
}
