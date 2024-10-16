using Model;

namespace Parser.Interface
{
    public interface ILoaderService
    {
        Task<IEnumerable<Product>> LoadAllJsonAsync();
    }
}
