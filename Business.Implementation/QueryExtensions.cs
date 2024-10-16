using Model;

namespace Business.Implementation
{
    internal static class QueryExtensions
    {
        public static IEnumerable<Product> WithProductName(this IEnumerable<Product> items, string? productName)
        {
            if (!string.IsNullOrEmpty(productName))
                return items.Where(p => p.Name.Contains(productName, StringComparison.OrdinalIgnoreCase));
            else
                return items;
        }

        public static IEnumerable<Product> WithDestination(this IEnumerable<Product> items, string? destination)
        {
            if (!string.IsNullOrEmpty(destination))
                return items.Where(p => p.Destination.Contains(destination, StringComparison.OrdinalIgnoreCase));
            else
                return items;
        }

        public static IEnumerable<Product> WithSupplier(this IEnumerable<Product> items, string? supplier)
        {
            if (!string.IsNullOrEmpty(supplier))
                return items.Where(p => p.Supplier.Contains(supplier, StringComparison.OrdinalIgnoreCase));
            else
                return items;
        }

        public static IEnumerable<Product> WithMaxPrice(this IEnumerable<Product> items, decimal? maxPrice)
        {
            if (maxPrice.HasValue)
                return items.Where(p => p.Price <= maxPrice.Value);
            else
                return items;
        }

        public static IEnumerable<Product> WithMinCapacity(this IEnumerable<Product> items, int capacity)
        {
            return items.Where(p => p.MaxGuests >= capacity);

        }

        public static IEnumerable<Product> Paginate(this IEnumerable<Product> items, int pageSize, int pageIndex)
        {
            return items.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }            
    }
}
