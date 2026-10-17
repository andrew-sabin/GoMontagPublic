using GoMontag_Shared.Entities.Recommendations;
using GoMontag_Shared.Models.RecommendationItem;
using GoMontag_WebApp.Repositories.Interfaces;
using Mapster;
using Radzen.Documents.Markdown;

namespace GoMontag_WebApp.Services
{
    public class RecommendationItemService : IRecommendationItemService
    {
        private readonly IRecommendationItemsRepo _repo;

        public RecommendationItemService(IRecommendationItemsRepo ItemRepo)
        {
            _repo = ItemRepo;
        }

        public async Task<List<RecommendedItemResponse>> CreateNewRecommendedItem(RecommendedItemCreateRequest request)
        {
            var newReccommendedItem = request.Adapt<RecommendationItem>();
            var result = await _repo.CreateNewRecommendationItem(newReccommendedItem);
            return result.Adapt<List<RecommendedItemResponse>>();
        }

        public async Task<List<RecommendedItemResponse>?> DeleteRecommendedItem(int id)
        {
            var deletedRecommendedItem = await _repo.DeleteRecommendationItem(id);
            if (deletedRecommendedItem == null)
                return null;
            return deletedRecommendedItem.Adapt<List<RecommendedItemResponse>>();
        }

        public async Task<List<RecommendedItemResponse>?> DisplayItemList(int listId, int pageNumber, int pageSize)
        {
            var result = await _repo.DisplayFullListOfItems(listId, pageNumber, pageSize);
            return result.Adapt<List<RecommendedItemResponse>>();
        }

        public async Task<List<RecommendedItemResponse>> GetAllRecommendationItemsAsync()
        {
            var result = await _repo.GetAllRecommendationItemsAsync();
            return result.Adapt<List<RecommendedItemResponse>>();
        }

        public async Task<RecommendedItemResponse?> GetRecommendedItemById(int id)
        {
            var result = await _repo.GetRecommendationItemById(id);
            if (result == null)
                return null;
            return result.Adapt<RecommendedItemResponse>();
        }

        public async Task<List<RecommendedItemResponse>?> GetRecommendedItemsByListId(int listId)
        {
            var result = await _repo.GetItemsByListId(listId);
            if (result == null) 
                return null;
            return result.Adapt<List<RecommendedItemResponse>>();
        }

        public async Task<List<RecommendedItemResponse>> GetRecommendedItemsByProductId(int productId)
        {
            var result = await _repo.GetItemsByProductId(productId);
            if (result == null)
                return null;
            return result.Adapt<List<RecommendedItemResponse>>();
        }

        public async Task<RecommendedItemResponse?> UpdatedRecommendedItem(int id, RecommendedItemUpdateRequest newRecommendedItem)
        {
            var updatedRecommendedItem = newRecommendedItem.Adapt<RecommendationItem>();
            var result = await _repo.UpdateRecommendationItem(id, updatedRecommendedItem);
            if (result == null)
                return null;
            return result.Adapt<RecommendedItemResponse>();
        }
    }
}
