using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Destination { get; set; }
        public decimal Price { get; set; }
        public int MaxGuests { get; set; }
        public string Supplier { get; set; }
    }
}
