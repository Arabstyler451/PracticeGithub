using System.ComponentModel.DataAnnotations;

namespace CityHome.Models
{
    public class Room
    {

        public int RoomId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Capacity { get; set; }

        public string Equipment { get; set; }

        public float PricePerHour { get; set; }

        // Navigation property
        public ICollection<Booking>? Bookings { get; set; }
    }
}
