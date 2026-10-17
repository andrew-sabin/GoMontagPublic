using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using GoMontag_Shared.Models.AuditLog;
using GoMontag_WebApp.Helpers.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoMontag_WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private string? videoFileName;
        private readonly IWebHostEnvironment hostingEnv;
        private readonly IConfiguration _configuration;
        private readonly ILogger<VideoController> _logger;
        private readonly IAuditLogService _auditLogService;

        public VideoController(IWebHostEnvironment HostingEnv, IConfiguration configuration, ILogger<VideoController> logger, 
            IAuditLogService auditLogService)
        {
            hostingEnv = HostingEnv;
            _configuration = configuration;
            _logger = logger;
            _auditLogService = auditLogService;
        }

        [HttpPost("Save")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Save([FromForm] IList<IFormFile> UploadFiles)
        {
            if (UploadFiles == null || UploadFiles.Count == 0)
                return BadRequest("No files uploaded.");

            var storageString = _configuration.GetValue<string>("ConnectionStrings:storageConnection");
            if (storageString == null)
                return (IActionResult)Results.Problem(title: "Invalid connection to media storage",
                    statusCode: 500);

            var containerName = Request.Headers["containerName"];
            var blobDirectory = Request.Headers["blobDirectory"];

            try
            {
                var savedFiles = new List<string>();
                var filePaths = new List<string>();

                foreach (var file in UploadFiles)
                {
                    if (file == null || file.Length == 0)
                        continue;

                    string filename = file.FileName;

                    var container = new BlobContainerClient(storageString, containerName);

                    var createResponse = await container.CreateIfNotExistsAsync();

                    if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                        await container.SetAccessPolicyAsync(PublicAccessType.Blob);

                    string blobPath = string.IsNullOrEmpty(blobDirectory) ?
                        filename : $"{blobDirectory}/{filename}";

                    var blob = container.GetBlobClient(blobPath);

                    await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

                    using (var fileStream = file.OpenReadStream())
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
                        {
                            _logger.UploadFileInfo(date, userName, ipAddress, filename, blobPath);
                            var auditlog = new AuditLogCreateRequest
                            {
                                UserName = userName,
                                Action = $"Uploaded File: {filename} to {blobPath}",
                                IpAddress = ipAddress,
                                Success = true,
                                TimeStampUtc = DateTime.UtcNow
                            };
                            await _auditLogService.CreateNewAuditLog(auditlog);
                        }
                           
                        await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = file.ContentType });
                    }

                    var imagePath = "" + containerName + "/" + blobDirectory + "/" + file.FileName;

                    savedFiles.Add(file.FileName);
                    filePaths.Add(imagePath);
                }

                return Ok(new { files = savedFiles, fileLocations = filePaths });
            }
            catch (Exception ex)
            {
                // return problem details / 500
                return Problem(detail: ex.Message);
            }
        }
    }
}
