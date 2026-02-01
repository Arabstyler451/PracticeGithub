using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityHome.Models
{
    public class Booking
    {

        public int BookingId { get; set; }

        public int RoomId { get; set; } // Foreign key to Room

        public string UserId { get; set; } // Foreign key to ApplicationUser

        public DateTime BookingDate { get; set; }

        public string TimeSlot { get; set; }

        public string Status { get; set; }

        // Navigation properties
        public Room Room { get; set; }    
    }
}
