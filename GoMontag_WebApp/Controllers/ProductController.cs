using GoMontag_WebApp.Helpers.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace GoMontag_WebApp.Controllers
{
    [Route("api/Product")]
    [ApiController]
    [Authorize (Roles ="Admin, Moderator, AlphaTester, User")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAllProducts());
        }
        [AllowAnonymous]
        [HttpGet("recent")]
        public async Task<IActionResult> GetAllRecentProducts(int pageNumber, int pageSize)
        {
            var products = await _productService.GetAllRecentProducts(pageNumber, pageSize);
            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("users/{creatorId}")]
        public async Task<IActionResult> GetProductsByCreatorID(string creatorId)
        {
            var products = await _productService.GetProductsByCreatorId(creatorId);
            if (products == null || !products.Any())
            {
                return NotFound($"Not products found with creator id: {creatorId}");
            }
            return Ok(products);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound($"Product with {id} was not found.");
            }
            return Ok(product);
        }
        [AllowAnonymous]
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<StoreResponse>?> GetProductBySlugAsync(string slug)
        {
            var result = await _productService.GetProductBySlug(slug);
            if (result == null)
            {
                return NotFound($"Store with this slug {slug} was not found");
            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin, Moderator, AlphaTester, User")]
        [HttpPost]
        public async Task<ActionResult<List<ProductResponse>>> CreateProduct(ProductCreateRequest product)
        {
            var remoteIp = HttpContext.Connection.RemoteIpAddress;
            if (remoteIp.IsIPv4MappedToIPv6)
            {
                remoteIp = remoteIp.MapToIPv4();
            }
            string ipAddress = remoteIp.ToString();
            string userName = HttpContext.User.Identity.Name;

            var date = DateTime.UtcNow;
            if (ipAddress != null && userName != null )
                _logger.CreateProduct(date, product.Name, ipAddress, userName);

            return Ok(await _productService.CreateProduct(product));
        }
        [Authorize(Roles = "Admin, Moderator, AlphaTester, User")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductUpdateRequest product)
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
                _logger.EditProduct(date, product.Name, ipAddress, userName);

            var updatedProduct = await _productService.UpdateProduct(id, product);
            if (updatedProduct == null)
            {
                return NotFound($"Product with {id} was not found.");
            }
            return Ok(updatedProduct);
        }
        [Authorize(Roles ="Admin, Moderator")]
        [HttpPatch("lock/{id}")]
        public async Task<ActionResult> LockProduct(int id, ProductLockRequest product)
        {
            var lockedProduct = await _productService.LockProduct(id, product);
            if (lockedProduct== null)
            {
                return NotFound($"Product with ID {id} was not found.");
            }
            return Ok(lockedProduct);
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
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
                _logger.DeleteProduct(date, id, ipAddress, userName);
            var deletedProduct = await _productService.DeleteProduct(id);
            if (deletedProduct == null)
            {
                return NotFound($"Product with {id} was not found.");
            }
            return Ok(deletedProduct);
        }
        [AllowAnonymous]
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryId(categoryId);
            if (products == null || !products.Any())
            {
                return NotFound($"No products found for category with ID {categoryId}.");
            }
            return Ok(products);
        }
        [AllowAnonymous]
        [HttpGet("category/recent/{categoryId}")]
        public async Task<IActionResult> SearchProductsByCategoryId(int categoryId, int pageNumber, int pageSize)
        { 
            var products = await _productService.GetProductsByCategoryIdSearch(categoryId, pageNumber, pageSize);
            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("Search/{categoryId}/{searchText}&pagenumber={pageNumber}")]
        public async Task<IActionResult> SearchProducts(string searchText, int categoryId, int pageNumber, int pageSize)
        {
            var products = await _productService.SearchProducts(searchText, categoryId, pageNumber, pageSize);
            return Ok(products);
        }
    }
}

namespace GoMontag.OData.Controllers
{
    public class ProductController : ODataController
    {
        private readonly DataContext _dbContext;

        public ProductController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.Top | AllowedQueryOptions.Count 
            | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Expand | AllowedQueryOptions.Select)]
        public IActionResult Get()
        {
            return Ok(_dbContext.Products.Where(p => p.IsDeleted == false));
        }
    }
}
