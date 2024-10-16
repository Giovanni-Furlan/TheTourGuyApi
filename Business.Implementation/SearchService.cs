using Business.Interface;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Model;
using Parser.Interface;

namespace Business.Implementation
{
    public class SearchService : ISearchService
    {
        private readonly ILoaderService _loaderService;
        private readonly ILogger<SearchService> _logger;
        private readonly string jsonPath;
        private readonly IMemoryCache _productCache;

        public SearchService(ILogger<SearchService> logger,
            ILoaderService loaderService,
            [FromKeyedServices("products")] IMemoryCache productCache)
        {
            jsonPath = $"{Path.DirectorySeparatorChar}Resources{Path.DirectorySeparatorChar}";

            _logger = logger;
            _loaderService = loaderService;
            _productCache = productCache;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(
            int minCapacity,
            string? productName,
            string? destination,
            string? supplier,
            decimal? maxPrice,
            int pageSize,
            int pageIndex)
        {
            IEnumerable<Product>? products = await _productCache.GetOrCreateAsync("All",
                async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);
                    return await _loaderService.LoadAllJsonAsync();
                });

            IEnumerable<Product> result = [];

            if (products != null && products.Any())
            {
                result = products.WithProductName(productName)
                    .WithDestination(destination)
                    .WithSupplier(supplier)
                    .WithMaxPrice(maxPrice)
                    .WithMinCapacity(minCapacity)
                    .Paginate(pageSize, pageIndex);
            }

            return result;
        }
    }
}
