using GoMontag_WebApp.Repositories.Interfaces;
using GoMontag_Shared.Entities.Recommendations;
using GoMontag.Shared.Helpers;

namespace GoMontag_WebApp.Repositories
{
    public class RecommendationListRepo : IRecommendationListRepo
    {
        private readonly DataContext _dataContext;
        private readonly ApplicationDbContext _appDbContext;
        private readonly IUserContextService _userContextService;
        public RecommendationListRepo(DataContext dataContext,  IUserContextService userContextService, ApplicationDbContext appDbContext)
        {
            _dataContext = dataContext;
            _userContextService = userContextService;
            _appDbContext = appDbContext;
        }

        public async Task<List<RecommendationList>> CreateRecommendationList(RecommendationList recommendationList)
        {
            recommendationList.UserId = _userContextService.GetUserId();
            recommendationList.UserName = _userContextService.GetUserName();
            recommendationList.PutOnFrontPage = false;
            _appDbContext.Add(recommendationList);
            await _appDbContext.SaveChangesAsync();
            return await GetAllRecommendationListsAsync();
        }

        public async Task<List<RecommendationList>?> DeleteRecommendationList(int id)
        {
            var allItems = await _appDbContext.Recommendations.Where(i => i.RecommendationListId  == id).ToListAsync();
            var recList = await _appDbContext.RecommendationLists.FirstOrDefaultAsync(i=> i.Id == id);
            foreach (var item in allItems) 
            {
                _appDbContext.Recommendations.Remove(item);
            }
            if (recList == null)
                return null;
            _appDbContext.RecommendationLists.Remove(recList);
            await _appDbContext.SaveChangesAsync();
            return await GetAllRecommendationListsAsync();
        }

        public async Task<List<RecommendationList>> GetAllRecommendationListsAsync()
        {
            return await _appDbContext.RecommendationLists.ToListAsync();
        }

        public async Task<List<RecommendationList>> GetMostRecentFrontPageRecommendationList(int amnt)
        {
            return await _appDbContext.RecommendationLists
                .OrderByDescending(r => r.CreatedDate)
                .Where( r => r.PutOnFrontPage == true)
                .Take(amnt)
                .ToListAsync();
        }

        public async Task<List<RecommendationList>?> GetRecommendatioListByCategory(string categorySlug)
        {
            return await _appDbContext.RecommendationLists.OrderByDescending(l => l.CreatedDate).ToListAsync();
        }

        public async Task<RecommendationList?> GetRecommendationListById(int id)
        {
            var result = await _appDbContext.RecommendationLists.FirstOrDefaultAsync(l => l.Id == id);
            if(result == null) 
                return null;
            return result;
        }

        public async Task<List<RecommendationList>?> GetRecommendationListByUser(string userName)
        {
            return await _appDbContext.RecommendationLists.Where(l =>  l.UserName == userName).ToListAsync();
        }

        public async Task<RecommendationList?> UpdateRecommendationList(int id, RecommendationList recommendationList)
        {
            var dbRecommendationList = await _appDbContext.RecommendationLists.FirstOrDefaultAsync(l => l.Id == id);
            if (dbRecommendationList == null) {
                return null;
            }

            dbRecommendationList.UserId = _userContextService.GetUserId();
            dbRecommendationList.UserName = _userContextService.GetUserName();
            dbRecommendationList.Name = recommendationList.Name;
            dbRecommendationList.Description = recommendationList.Description;
            dbRecommendationList.SharableLink = recommendationList.SharableLink;
            dbRecommendationList.PutOnFrontPage = recommendationList.PutOnFrontPage;

            dbRecommendationList.ModifiedDate = DateTime.UtcNow;

            await _appDbContext.SaveChangesAsync();
            return await GetRecommendationListById(id);
        }
    }
}
