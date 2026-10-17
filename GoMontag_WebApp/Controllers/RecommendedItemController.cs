using GoMontag_Shared.Models.RecommendationItem;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sqids;

namespace GoMontag_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Moderator, AlphaTester, User")]
    public class RecommendedItemController : ControllerBase
    {
        private readonly IRecommendationItemService _recommendationItemService;

        public RecommendedItemController(IRecommendationItemService service)
        {
            _recommendationItemService = service;
        }
        [HttpGet]
        public async Task<ActionResult<List<RecommendedItemResponse>>> GetAllRecommendedItems()
        {
            return Ok(await _recommendationItemService.GetAllRecommendationItemsAsync());
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<RecommendedItemResponse>> GetRecommendedItemById(int id) 
        {
            var result = await _recommendationItemService.GetRecommendedItemById(id);
            if (result == null)
                return NotFound("Recommended Item Not Found");

            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("hash/{hash}")]
        public async Task<ActionResult<RecommendedItemResponse>> GetRecommendedItemByHash(string hash)
        {
            var id = DecodeItemHash(hash);
            var result = await _recommendationItemService.GetRecommendedItemById(id);
            if (result == null)
                return NotFound("Recommended Item Not Found");

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("list/{listId}")]
        public async Task<ActionResult<List<RecommendedItemResponse>>?> GetItemsByListId(string listId)
        {
            var realListId = DecodeListHash(listId);
            var result = await _recommendationItemService.GetRecommendedItemsByListId(realListId);
            if (result == null)
                return NotFound("Recommendation List Not Found");

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<List<RecommendedItemResponse>>?> GetItemsByListId(int productId)
        {
            var result = await _recommendationItemService.GetRecommendedItemsByProductId(productId);
            if (result == null)
                return NotFound("Recommendation List Not Found");

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("list/full/{listId}")]
        public async Task<ActionResult<List<RecommendedItemResponse>>> DisplayFullItemList(string listId, int pageNumber, int pageSize) 
        {
            var realListId = DecodeListHash(listId);
            var result = await _recommendationItemService.DisplayItemList(realListId, pageNumber, pageSize);
            if (result == null)
                return NotFound("Recommendaiton List Not Found");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<RecommendedItemResponse>>> CreateRecommendedItem(RecommendedItemCreateRequest request)
        {
            return Ok(await _recommendationItemService.CreateNewRecommendedItem(request));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RecommendedItemResponse>> UpdateRecommendedItem(int id, RecommendedItemUpdateRequest request)
        {
            var result = await _recommendationItemService.UpdatedRecommendedItem(id, request);
            if (result == null)
                return NotFound("Recommended Item Not Found");
            return Ok(result);
        }
        [HttpPut("hash/{hash}")]
        public async Task<ActionResult<RecommendedItemResponse>> UpdateHashedRecommendedItem(string hash, RecommendedItemUpdateRequest request)
        {
            var id = DecodeItemHash(hash);
            var result = await _recommendationItemService.UpdatedRecommendedItem(id, request);
            if (result == null)
                return NotFound("Recommended Item Not Found");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<RecommendedItemResponse>>> DeleteRecommendedItem(int id)
        {
            var result = await _recommendationItemService.DeleteRecommendedItem(id);
            if (result == null)
                return NotFound("Recommended Item Not Found");
            return Ok(result);
        }

        private int DecodeItemHash(string hash)
        {
            var sqids = new SqidsEncoder<int>(new()
            {
                MinLength = 0,
                Alphabet = "",
            });
            var realId = sqids.Decode(hash).Single();
            return realId;
        }

        private int DecodeListHash(string hash)
        {
            var sqids = new SqidsEncoder<int>(new()
            {
                MinLength = 0,
                Alphabet = "",
            });
            var realId = sqids.Decode(hash).Single();
            return realId;
        }
    }
}
