using GoMontag.Shared.Models.FormatRevision;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Sqids;

namespace GoMontag_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormatRevisionController : ControllerBase
    {
        private readonly IFormatRevisionService _formatRevisionService;
        public FormatRevisionController(IFormatRevisionService formatRevisionService)
        {
            _formatRevisionService = formatRevisionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<FormatRevisionResponse>>> GetFormatRevisionsAsync()
        {
            var result = await _formatRevisionService.GetFormatRevisionsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FormatRevisionResponse>> GetFormatRevisionByIdAsync(int id)
        {
            var result = await _formatRevisionService.GetFormatRevisionByIdAsync(id);
            if (result == null)
                return BadRequest($"No FormatRevision with {id} found");

            return Ok(result);
        }

        [HttpGet("format/{formatHash}")]
        public async Task<ActionResult<List<FormatRevisionResponse>>> GetFormatRevisionsByFormatId(string formatHash)
        {
            var formatId = DecodeFormatHash(formatHash);
            var result = await _formatRevisionService.GetFormatRevisionsByFormatId(formatId);
            return Ok(result);
        }

        [HttpGet("format/recent/{formatId}")]
        public async Task<ActionResult<FormatRevisionResponse>> GetMostRecentRevisionResponseByFormatId(int formatId)
        {

            var result = await _formatRevisionService.GetLatestFormatRevisionByFormatId(formatId);
            if (result == null)
                return BadRequest("Format did not have any revisions");

            return Ok(result);
        }

        [HttpGet("format/recent/edit/{formatId}")]
        public async Task<ActionResult<FormatRevisionResponse>> GetMostRecentRevisionForEditing(int formatId)
        {

            var result = await _formatRevisionService.GetMostRecentFormatRevisionForEditingAsync(formatId);
            if (result == null)
                return BadRequest("Format did not have any revisions");

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Moderator, AlphaTester, User")]
        public async Task<ActionResult<FormatRevisionResponse>> CreateNewFormatRevisionAsync(FormatRevisionCreateRequest request)
        {
            return Ok(await _formatRevisionService.CreateNewFormatRevisionAsync(request));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<ActionResult<List<FormatRevisionResponse>>> DeleteFormatRevisionAsync(int id) 
        { 
            var result = await _formatRevisionService.DeleteFormatRevisionAsync(id);
            if (result == null)
            {
                return NotFound("FormatRevision not found");
            }
            return Ok(result);
        }


        private int DecodeFormatHash(string hash)
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

namespace GoMontag.OData.Controllers
{
    public class FormatRevisionController : ODataController
    {
        private readonly DataContext _dbContext;

        public FormatRevisionController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.Top | AllowedQueryOptions.Count
            | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Expand | AllowedQueryOptions.Select)]
        public IActionResult Get()
        {
            return Ok(_dbContext.FormatRevisions);
        }
    }

}
