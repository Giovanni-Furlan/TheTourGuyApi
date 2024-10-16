using Model;

namespace Parser.Implementation
{
    internal class ConfigProduct
    {
        public string Supplier { get; set; }
        public string ListPath { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        public string Price { get; set; }
        public string? Discount { get; set; }
        public string? DiscountPercentage { get; set; }
        public string MaxGuests { get; set; }

    }
}
