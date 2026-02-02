using System.ComponentModel.DataAnnotations.Schema;

namespace CityPointRoomHire.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int RoomId { get; set; } // Foreign Key

        public string UserId { get; set; } // Foreign Key

        public DateTime BookingDate { get; set; }

        public string TimeSlot { get; set; }

        public string Status { get; set; }

        // Navigation properties
        public Room Room { get; set; }

    }
}
