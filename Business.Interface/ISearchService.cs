using Model;

namespace Business.Interface
{
    public interface ISearchService
    {
        Task<IEnumerable<Product>> GetProductsAsync(
            int minCapacity,
            string? productName,
            string? destination,
            string? supplier,
            decimal? maxPrice,
            int pageSize,
            int pageIndex);
    }
}
