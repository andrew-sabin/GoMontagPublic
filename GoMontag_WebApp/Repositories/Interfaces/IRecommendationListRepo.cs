using GoMontag_Shared.Entities.Recommendations;

namespace GoMontag_WebApp.Repositories.Interfaces
{
    public interface IRecommendationListRepo
    {
        public Task<List<RecommendationList>> GetAllRecommendationListsAsync();
        public Task<RecommendationList?> GetRecommendationListById(int id);
        public Task<List<RecommendationList>?> GetRecommendationListByUser(string userName);
        public Task<List<RecommendationList>?> GetRecommendatioListByCategory(string categorySlug);
        public Task<List<RecommendationList>> GetMostRecentFrontPageRecommendationList(int amnt);
        public Task<List<RecommendationList>> CreateRecommendationList(RecommendationList recommendationList);
        public Task<RecommendationList?> UpdateRecommendationList(int id, RecommendationList recommendationList);
        public Task<List<RecommendationList>?> DeleteRecommendationList(int id);
    }
}
