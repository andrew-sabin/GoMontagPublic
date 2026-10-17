using GoMontag_WebApp.Repositories.Interfaces;
using GoMontag_Shared.Entities.Recommendations;
using GoMontag_Shared.Models.RecommendationList;
using Mapster;

namespace GoMontag_WebApp.Services
{
    public class RecommendationListService : IRecommendationListService
    {
        private readonly IRecommendationListRepo _recommendationListRepo;

        public RecommendationListService(IRecommendationListRepo recommendationListRepo)
        {
            _recommendationListRepo = recommendationListRepo;
        }
        public async Task<List<ListResponse>> CreateNewRecommendationListAsync(ListCreateRequest listCreateRequest)
        {
            var newRecommendList = listCreateRequest.Adapt<RecommendationList>();
            var result = await _recommendationListRepo.CreateRecommendationList(newRecommendList);
            return result.Adapt<List<ListResponse>>();
        }

        public async Task<List<ListResponse>?> DeleteRecommendationList(int id)
        {
            var result = await _recommendationListRepo.DeleteRecommendationList(id);
            if (result == null)
                return null;

            return result.Adapt<List<ListResponse>>();
        }

        public async Task<List<ListResponse>> GetAllRecommendationListsAsync()
        {
            var result = await _recommendationListRepo.GetAllRecommendationListsAsync();
            return result.Adapt<List<ListResponse>>();
        }

        public async Task<List<ListResponse>> GetFrontPageLists(int amnt)
        {
            var result = await _recommendationListRepo.GetMostRecentFrontPageRecommendationList(amnt);
            return result.Adapt<List<ListResponse>>();
        }

        public async Task<List<ListResponse>?> GetListsByCategory(string categoryName)
        {
            var result = await _recommendationListRepo.GetRecommendatioListByCategory(categoryName);
            if (result == null)
                return null;
            return result.Adapt<List<ListResponse>>();
        }

        public async Task<List<ListResponse>?> GetListsByUserName(string userName)
        {
            var result = await _recommendationListRepo.GetRecommendationListByUser(userName);
            if (result == null)
            {
                return null;
            }
            return result.Adapt<List<ListResponse>>();
        }

        public async Task<ListResponse?> GetRecommendationListById(int id)
        {
            var result = await _recommendationListRepo.GetRecommendationListById(id);
            if (result == null)
                return null;
            return result.Adapt<ListResponse>();
        }

        public async Task<ListResponse?> UpdateRecommendationListAsync(int id, ListUpdateRequest listUpdateRequest)
        {
            var updatedList = listUpdateRequest.Adapt<RecommendationList>();
            var result = await _recommendationListRepo.UpdateRecommendationList(id, updatedList);
            if (result == null)
            {
                return null;
            }
            return result.Adapt<ListResponse>();
        }
    }
}
