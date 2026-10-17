using GoMontag_WebApp.Helpers.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Sqids;

namespace GoMontag_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin, Moderator, AlphaTester, User")]
    public class FormatController : ControllerBase
    {
        private readonly IFormatService _formatService;
        private readonly ILogger<FormatController> _logger;
        public FormatController(IFormatService formatService, ILogger<FormatController> logger)
        {
            _formatService = formatService;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<FormatResponse>>> GetAllFormatsAsync()
        {
            return Ok(await _formatService.GetAllFormatsAsync());
        }

        [AllowAnonymous]
        [HttpGet("inforequested")]
        public async Task<ActionResult<List<FormatResponse>>> GetAllInfoRequestedFormats()
        {
            return Ok(await _formatService.GetAllInfoRequestedFormatsAsync());
        }

        [AllowAnonymous]
        [HttpGet("recent")]
        public async Task<ActionResult<List<FormatResponse>>> GetRecentFormatsAsync(int amount)
        {
            var result = await _formatService.GetRecentFormatsAsync(amount);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("recent/updated")]
        public async Task<ActionResult<List<FormatResponse>>> GetRecentlyUpdatedFormatsAsync(int amount) 
        {
            var result = await _formatService.GetRecentlyUpdatedFormatsAsync(amount);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/product/{productId}")]
        public async Task<ActionResult<List<FormatResponse>>> GetFormatsByProductIdAdminAsync(int productId)
        {
            var result = await _formatService.GetFormatsByProductIdAdminAsync(productId);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<List<FormatResponse>>> GetFormatsByProductIdAsync(int productId)
        {
            var result = await _formatService.GetFormatsByProductIdAsync(productId);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{hash}")]
        public async Task<ActionResult<FormatResponse>> GetFormatByIdAsync(string hash)
        {
            var id = DecodeHash(hash);
            var result = await _formatService.GetFormatByIdAsync(id);
            if (result is null)
            {
                return NotFound($"Format with {hash} was not found.");
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/{hash}")]
        public async Task<ActionResult<FormatResponse>> GetFormatByIdAdminAsync(string hash)
        {
            var id = DecodeHash(hash);
            var result = await _formatService.GetFormatByIdAdminAsync(id);
            if (result is null)
            {
                return NotFound($"Format with {hash} was not found.");
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("product/{prodSlug}/slug/{slug}")]
        public async Task<ActionResult<StoreResponse>?> GetFormatBySlugAsync(string prodSlug, string slug)
        {
            var result = await _formatService.GetFormatBySlugAsync(prodSlug, slug);
            if (result == null)
            {
                return NotFound($"Format with this slug {slug} was not found in {prodSlug}");
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/product/{prodSlug}/slug/{slug}")]
        public async Task<ActionResult<StoreResponse>?> GetFormatBySlugAdminAsync(string prodSlug, string slug)
        {
            var result = await _formatService.GetFormatBySlugAdminAsync(prodSlug, slug);
            if (result == null)
            {
                return NotFound($"Format with this slug {slug} was not found in {prodSlug}");
            }
            return Ok(result);
        }

        [Authorize(Roles ="Admin, Moderator, AlphaTester, User")]
        [HttpPost]
        public async Task<ActionResult<FormatResponse>> CreateFormat(FormatCreateRequest format)
        {
            var remoteIp = HttpContext.Connection.RemoteIpAddress;
            if (remoteIp.IsIPv4MappedToIPv6)
            {
                remoteIp = remoteIp.MapToIPv4();
            }
            string ipAddress = remoteIp.ToString();
            string userName = HttpContext.User.Identity.Name;

            var date = DateTime.UtcNow;
            if (ipAddress != null && userName != null)
                _logger.CreateFormat(date, format.ProductId, format.Type, format.Edition, ipAddress, userName);
            var response = await _formatService.CreateFormatAsync(format);
            return Ok(response);
        }
        [Authorize(Roles = "Admin, Moderator, AlphaTester, User")]
        [HttpPut("{hash}")]
        public async Task<ActionResult<FormatResponse>> UpdateFormatAsync(string hash, FormatUpdateRequest format)
        {
            var remoteIp = HttpContext.Connection.RemoteIpAddress;
            if (remoteIp.IsIPv4MappedToIPv6)
            {
                remoteIp = remoteIp.MapToIPv4();
            }
            string ipAddress = remoteIp.ToString();
            string userName = HttpContext.User.Identity.Name;

            var date = DateTime.UtcNow;
            if (ipAddress != null && userName != null)
                _logger.EditFormat(date, format.ProductId, format.Type!, format.Edition, ipAddress, userName);
            var id = DecodeHash(hash);
            var result = await _formatService.UpdateFormatAsync(id, format);
            if (result is null)
            {
                return NotFound($"Format with {hash} Not Found");
            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPatch("lock/{hash}")]
        public async Task<ActionResult<FormatResponse>> LockFormatAsync(string hash, FormatLockRequest format)
        {
            var id = DecodeHash(hash);
            var result = await _formatService.LockFormatAsync(id, format);
            if (result is null)
            {
                return NotFound($"Format with {hash} Not Found.");
            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPatch("request/{hash}")]
        public async Task<ActionResult<FormatResponse>> RequestInformationForFormatAsync(string hash, FormatInformationRequest format)
        {
            var id = DecodeHash(hash);
            var result = await _formatService.RequestInfoForFormatAsync(id, format);
            if (result is null)
            {
                return NotFound($"Format with {hash} Not Found.");
            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpDelete("{hash}")]
        public async Task<ActionResult<List<FormatResponse>>> DeleteFormatAsync(string hash)
        {
            
            var id = DecodeHash(hash);
            var result = await _formatService.DeleteFormatAsync(id);
            if (result is null)
            {
                return NotFound($"Format with {hash} was not found.");
            }
            else
            {
                var remoteIp = HttpContext.Connection.RemoteIpAddress;
                if (remoteIp.IsIPv4MappedToIPv6)
                {
                    remoteIp = remoteIp.MapToIPv4();
                }
                string ipAddress = remoteIp.ToString();
                string userName = HttpContext.User.Identity.Name;

                var date = DateTime.UtcNow;
                if (ipAddress != null && userName != null)
                    _logger.DeleteFormat(date, id, ipAddress, userName);
                return Ok(result);
            }
            
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

namespace GoMontag.OData.Controllers
{
    public class FormatController : ODataController
    {
        private readonly DataContext _dbContext;

        public FormatController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.Top | AllowedQueryOptions.Count
            | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Expand | AllowedQueryOptions.Select)]
        public IActionResult Get()
        {
            return Ok(_dbContext.Formats.Where(p => p.IsDeleted == false));
        }
    }
}