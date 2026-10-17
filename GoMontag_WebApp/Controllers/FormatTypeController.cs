using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Sqids;

namespace GoMontag_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Moderator, AlphaTester, User")]
    public class FormatTypeController : ControllerBase
    {
        readonly IFormatTypeService _formatTypeService;
        public FormatTypeController(IFormatTypeService formatTypeService)
        {
            _formatTypeService = formatTypeService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<FormatTypeResponse>>> GetAllFormatTypes()
        {
            return Ok(await _formatTypeService.GetAllFormatTypesAsync());
        }

        [AllowAnonymous]
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<List<FormatTypeResponse>>> GetFormatTypesByCategoryId(int categoryId) 
        { 
            return Ok(await _formatTypeService.GetFormatTypesByCategoryId(categoryId));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<FormatTypeResponse>> GetFormatTypeById(int id)
        {
            var result = await _formatTypeService.GetFormatTypeByIdAsync(id);
            if (result is null)
            {
                return NotFound($"Format Type with ID {id} was not found.");
            }
            return Ok(await _formatTypeService.GetFormatTypeByIdAsync(id));
        }

        [AllowAnonymous]
        [HttpGet("hash/{hash}")]
        public async Task<ActionResult<FormatTypeResponse>> GetFormatTypeByHash(string hash)
        {
            var id = DecodeHash(hash);
            var result = await _formatTypeService.GetFormatTypeByIdAsync(id);
            if (result is null)
            {
                return NotFound($"Format Type with {hash} was not found.");
            }
            return Ok(await _formatTypeService.GetFormatTypeByIdAsync(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateFormatType(FormatTypeCreateRequest formatType)
        {
            return Ok(await _formatTypeService.CreateFormatTypeAsync(formatType));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{hash}")]
        public async Task<ActionResult<FormatTypeResponse>> UpdateFormatType(FormatTypeUpdateRequest formatType, string hash)
        {
            var id = DecodeHash(hash);
            var result = await _formatTypeService.UpdateFormatTypeAsync(formatType, id);
            if (result is null)
            {
                return NotFound($"Format Type with {hash} was not found.");
            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{hash}")]
        public async Task<ActionResult<FormatTypeResponse>> DeleteFormatType(string hash)
        {
            var id = DecodeHash(hash);
            var result = await _formatTypeService.DeleteFormatTypeByIdAsync(id);
            if (result is null)
            {
                return NotFound($"Format Type with {hash} was not found.");
            }
            return Ok(result);
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
