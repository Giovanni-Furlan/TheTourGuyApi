using Model;

namespace Parser.Interface
{
    public interface IJsonParserService
    {
        Task<IEnumerable<Product>> ParserJSONAsync(string fullFileName);

    }
}
