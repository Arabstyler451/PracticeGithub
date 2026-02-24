using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChakdeLife.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; } // Foreign key to Category

        public int Quantity { get; set; }

        public int StockQuantity { get; set; }

        public Category Category { get; set; }
    }
}
