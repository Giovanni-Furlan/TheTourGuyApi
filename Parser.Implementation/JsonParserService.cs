using Microsoft.Extensions.Logging;
using Model;
using Newtonsoft.Json;
using Parser.Interface;
using System.Text.Json.Serialization;

namespace Parser.Implementation
{
    public class JsonParserService : IJsonParserService
    {
        private readonly List<ConfigProduct> _mappings;

        private readonly ILogger<JsonParserService> _logger;
        private readonly string configFilePath;

        public JsonParserService(ILogger<JsonParserService> logger) 
        {
            configFilePath = @$".{Path.DirectorySeparatorChar}Resources{Path.DirectorySeparatorChar}mapping.json";
            _logger = logger;

            var jsonConfig = File.ReadAllText(configFilePath);
            _mappings = JsonConvert.DeserializeObject<Configuration>(jsonConfig)?.Config ?? [];
        }

        public async Task<IEnumerable<Product>> ParserJSONAsync(string fullFileName)
        {
            if(!File.Exists(fullFileName))
            {
                _logger.LogError($"Unable to find file: {fullFileName}");
                return [];
            }

            string? content = await File.ReadAllTextAsync(fullFileName);

            if(content is null)
            {
                _logger.LogError($"Unable to read file: {fullFileName}");
                return [];
            }

            return await DeserializeJsonAsync(content, fullFileName);
        }

        private async Task<IEnumerable<Product>> DeserializeJsonAsync(string content, string fullFileName)
        {
            try
            {
                dynamic? json = JsonConvert.DeserializeObject<dynamic>(content);

                if (json is null)
                {
                    _logger.LogError($"Unable to deserialize json: {fullFileName}");
                    return [];
                }

                var fileName = new FileInfo(fullFileName).Name;

                var config = _mappings.First(c => fileName.Contains(c.Supplier));

                dynamic elements = json[config.ListPath];

                List<Task<Product>> tasks = [];

                foreach (dynamic element in elements)
                {
                    Product a = CreateProductByJson(element, config);
                    tasks.Add(Task.Run(() => (Product) CreateProductByJson(element, config)));
                }

                Product[] products = await Task.WhenAll(tasks);

                return products;

            }
            catch (JsonException ex)
            {
                _logger.LogError($"Json not correctly formatted: {fullFileName}", ex);
                return [];
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to parse the file: {fullFileName}", ex);
                return [];
            }
        }

        private Product CreateProductByJson(dynamic element, ConfigProduct config)
        {
            var prod = new Product
            {
                Description = GetPropertyByPropertyPath(element, config.Description),
                MaxGuests = GetPropertyByPropertyPath(element, config.MaxGuests),
                Name = GetPropertyByPropertyPath(element, config.Name),
                Destination = GetPropertyByPropertyPath(element, config.Destination),
                Price = Math.Round((decimal)GetPropertyByPropertyPath(element, config.Price), 2),
                Supplier = config.Supplier,
            };

            if (config.Discount != null)
                prod.Price -= Math.Round((decimal)GetPropertyByPropertyPath(element, config.Discount), 2);

            if (config.DiscountPercentage != null)
                prod.Price -= Math.Round(prod.Price / (decimal)GetPropertyByPropertyPath(element, config.DiscountPercentage), 2);

            return prod;
        }

        private dynamic GetPropertyByPropertyPath(dynamic element, string propertyPath)
        {
            dynamic result = element;
            var levels = propertyPath.Split('.');

            for(int i = 0; i < levels.Length; i++)
            {
                result = result[levels[i]];
            }

            return result;
        }
    }
}
