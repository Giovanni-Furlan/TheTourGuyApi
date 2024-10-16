using Parser.Interface;
using Microsoft.Extensions.Logging;
using Model;

namespace Parser.Implementation
{
    public class LoaderService : ILoaderService
    {
        private readonly IJsonParserService _jsonParserService;
        private readonly ILogger<LoaderService> _logger;
        private readonly string jsonPath;

        public LoaderService(ILogger<LoaderService> logger, IJsonParserService jsonParserService)
        {
            jsonPath = @$".{Path.DirectorySeparatorChar}Resources{Path.DirectorySeparatorChar}JSON";
            _logger = logger;
            _jsonParserService = jsonParserService;
        }

        public async Task<IEnumerable<Product>> LoadAllJsonAsync()
        {
            if(!Directory.Exists(jsonPath))
            {
                _logger.LogError($"Unable to find path for json files: {jsonPath}");
                return [];
            }

            List<Product> products = [];
            string[] files = Directory.GetFiles(jsonPath);

            List<Task<IEnumerable<Product>>> tasks = [];

            foreach (string file in files)
            {
                tasks.Add(_jsonParserService.ParserJSONAsync(file));
            }

            IEnumerable<Product>[] results = await Task.WhenAll(tasks);

            foreach (IEnumerable<Product> result in results)
            {
                products.AddRange(result);
            }

            return products;
        }

    }
}
