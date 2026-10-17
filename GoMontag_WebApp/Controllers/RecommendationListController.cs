using GoMontag_Shared.Models.RecommendationList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sqids;

namespace GoMontag_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Moderator, AlphaTester, User")]
    public class RecommendationListController : ControllerBase
    {
        private readonly IRecommendationListService _recommendationListService;

        public RecommendationListController(IRecommendationListService service)
        {
            _recommendationListService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ListResponse>>> GetAllRecomendationListsAsync()
        {
            return Ok(await _recommendationListService.GetAllRecommendationListsAsync());
        }

        [AllowAnonymous]
        [HttpGet("frontpage/{amnt}")]
        public async Task<ActionResult<List<ListResponse>>> GetFrontPageRecommendationListsAsync(int amnt)
        {
            return Ok(await _recommendationListService.GetFrontPageLists(amnt));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ListResponse?>> GetRecommendationListByIdAsync(string id)
        {
            var realId = DecodeHash(id);
            var recommendList = await _recommendationListService.GetRecommendationListById(realId);
            if (recommendList == null) {
                return NotFound("Recommendation List Not Found");
            }
            return Ok(recommendList);
        }
        [AllowAnonymous]
        [HttpGet("users/{username}")]
        public async Task<ActionResult<List<ListResponse>>> GetRecommendationListsByUserName(string username) 
        {
            var recommendLists = await _recommendationListService.GetListsByUserName(username);
            return Ok(recommendLists);
        }

        [AllowAnonymous]
        [HttpGet("category/{categorySlug}")]
        public async Task<ActionResult<List<ListResponse>>> GetRecommendationListsByCategorySlug(string categorySlug) 
        {
            var recommendLists = await _recommendationListService.GetListsByCategory(categorySlug);
            return Ok(recommendLists);
        }

        [HttpPost]
        public async Task<ActionResult<List<ListResponse>>> CreateNewRecommendationListAsync(ListCreateRequest request)
        {
            return Ok(await _recommendationListService.CreateNewRecommendationListAsync(request));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ListResponse>> UpdateRecommendationListAsync(string id, ListUpdateRequest request)
        {
            var realId = DecodeHash(id);
            var updatedList = await _recommendationListService.UpdateRecommendationListAsync(realId, request);
            if (updatedList == null) { return NotFound("List was not found"); }
            return Ok(updatedList);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ListResponse>>> DeleteRecommendationListAsync(string id)
        {
            var realId = DecodeHash(id);
            var deletedList = await _recommendationListService.DeleteRecommendationList(realId);
            if (deletedList == null)
                return NotFound("List was not found");

            return Ok(deletedList);
        }

        private int DecodeHash(string hash)
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
