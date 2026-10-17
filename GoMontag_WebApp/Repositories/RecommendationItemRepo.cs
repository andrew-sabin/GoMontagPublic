using GoMontag_WebApp.Repositories.Interfaces;
using GoMontag_Shared.Entities.Recommendations;

namespace GoMontag_WebApp.Repositories
{
    public class RecommendationItemRepo : IRecommendationItemsRepo
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly IUserContextService _userContextService;
        public RecommendationItemRepo(ApplicationDbContext appDbContext, IUserContextService userContextService)
        {
            _appDbContext = appDbContext;
            _userContextService = userContextService;
        }

        public async Task<List<RecommendationItem>> CreateNewRecommendationItem(RecommendationItem item)
        {
            item.UserName = _userContextService.GetUserName();
            _appDbContext.Add(item);
            await _appDbContext.SaveChangesAsync();
            return await GetAllRecommendationItemsAsync();
        }

        public async Task<List<RecommendationItem>?> DeleteRecommendationItem(int id)
        {
            var dbRecommendedItem = _appDbContext.Recommendations.FirstOrDefault(i => i.Id == id);
            if (dbRecommendedItem == null)
                return null;

            var listId = dbRecommendedItem.RecommendationListId;

            _appDbContext.Recommendations.Remove(dbRecommendedItem);
            await _appDbContext.SaveChangesAsync();

            return await GetItemsByListId(listId);
        }

        public async Task<List<RecommendationItem>> DisplayFullListOfItems(int listId, int pageNumber, int pageSize)
        {
            var query = _appDbContext.Recommendations.Where(i => i.RecommendationListId == listId).OrderBy(i => i.Order).Include(i => i.RecommendationList);
            var totalCount = query.Count();
            var skipNumber = (pageNumber - 1) * pageSize;

            return await query.Skip(skipNumber).Take(pageSize).ToListAsync();
        }

        public async Task<List<RecommendationItem>> GetAllRecommendationItemsAsync()
        {
            return await _appDbContext.Recommendations.Include(r => r.RecommendationList).ToListAsync();
        }

        public async Task<List<RecommendationItem>?> GetItemsByListId(int listId)
        {
            var recommendedItem = await _appDbContext.Recommendations.Where(i=> i.RecommendationListId == listId).Include(i => i.RecommendationList).OrderBy(i => i.Order).ToListAsync();
            if (recommendedItem == null)
                return null;

            return recommendedItem;
        }

        public async Task<List<RecommendationItem>> GetItemsByProductId(int productId)
        {
            return await _appDbContext.Recommendations
                .Include(r => r.RecommendationList)
                .Where(i => i.ProductId == productId)
                .ToListAsync();
        }

        public async Task<RecommendationItem?> GetRecommendationItemById(int id)
        {
            return await _appDbContext.Recommendations
                .Include(r => r.RecommendationList)
                .FirstOrDefaultAsync(i  => i.Id == id); 
        }

        public async Task<RecommendationItem?> UpdateRecommendationItem(int id, RecommendationItem item)
        {
            var dbRecommendedItem = await _appDbContext.Recommendations.FirstOrDefaultAsync(i => i.Id == id);
            if (dbRecommendedItem == null)
                return null;

            dbRecommendedItem.ModifiedDate = DateTime.UtcNow;

            dbRecommendedItem.Id = id;
            dbRecommendedItem.Notes = item.Notes;
            dbRecommendedItem.Order = item.Order;
            dbRecommendedItem.RecommendationListId = item.RecommendationListId;
            dbRecommendedItem.ProductName = item.ProductName;
            dbRecommendedItem.ProductCoverImg = item.ProductCoverImg;
            
            await _appDbContext.SaveChangesAsync();
            return await GetRecommendationItemById(id);
        }
    }
}
