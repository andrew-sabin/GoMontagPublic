using GoMontag_WebApp.Helpers.Logger;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sqids;

namespace GoMontag_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Moderator, AlphaTester, User")]
    public class FormatImageController : ControllerBase
    {
        private readonly IFormatImageService _formatImageService;
        private readonly ILogger<FormatImageController> _logger;

        public FormatImageController(IFormatImageService formatImagesService, ILogger<FormatImageController> logger)
        {
            _formatImageService = formatImagesService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<FormatImageResponse>>> GetAllFormatImagesAsync() 
        {
            return Ok(await _formatImageService.GetAllFormatImages());
        }

        [AllowAnonymous]
        [HttpGet("{imageHash}")]
        public async Task<ActionResult<FormatImageResponse>> GetFormatImageByIdAsync(string imageHash)
        {
            var imageId = DecodeImageHash(imageHash);
            var result = await _formatImageService.GetFormatImageById(imageId);
            if (result == null) 
            {
                return NotFound($"FormatId with {imageId} was not found");
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("url/{imageUrl}")]
        public async Task<ActionResult<FormatImageResponse>> GetFormatImageByUrlAsync(string imageUrl)
        {
            var result = await _formatImageService.GetFormatImageByImageUrl(imageUrl);
            if (result == null)
            {
                return NotFound($"FormatImage with {imageUrl} was not found");
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("format/{hashedId}")]
        public async Task<ActionResult<List<FormatImageResponse>>> GetAllFormatImagesByFormatId(string hashedId)
        {
            var formatId = DecodeHash(hashedId);
            var result = await _formatImageService.GetAllFormatImagesByFormatId(formatId);
            return result.Adapt<List<FormatImageResponse>>();
        }

        [HttpPost]
        public async Task<ActionResult<FormatImageResponse>> CreateNewFormatImage(FormatImageCreateRequest formatImage)
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
                _logger.CreateFormatImage(date, userName, ipAddress, formatImage.ImageUrl);

            var response = await _formatImageService.CreateNewFormatImage(formatImage);
            return Ok(response);

        }

        [HttpDelete("{imageHash}")]
        public async Task<ActionResult<List<FormatImageResponse>>> DeleteFormatImage(string imageHash)
        {
            var formatImageId = DecodeImageHash(imageHash);
            var result = await _formatImageService.DeleteFormatImage(formatImageId);
            if (result == null)
            {
                return NotFound($"FormatImage with {formatImageId} not found");
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
                    _logger.DeleteFormatImage(date, userName, ipAddress, formatImageId);

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

        private int DecodeImageHash(string hash)
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
