using System.ComponentModel.DataAnnotations.Schema;

namespace ChakdeLife.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string UserId { get; set; } // Foreign key to User

        public DateTime OrderDate { get; set; }

        public float TotalAmount { get; set; }

        public string Status { get; set; } = "Pending";
    }
}
