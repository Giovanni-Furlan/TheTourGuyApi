using Business.Interface;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly ISearchService _searchService;

        public ProductController(ILogger<ProductController> logger, ISearchService searchService)
        {
            _logger = logger;
            _searchService = searchService;
        }

        [HttpGet(Name = nameof(this.GetProduct))]
        public async Task<IEnumerable<Product>> GetProduct(int minCapacity,
        string? productName,
        string? destination,
        string? supplier,
        decimal? maxPrice,
        int pageSize = 10,
        int pageIndex = 0)
        {
            return await _searchService.GetProductsAsync(minCapacity,
                productName,
                destination,
                supplier,
                maxPrice,
                pageSize,
                pageIndex);
        }
    }
}
