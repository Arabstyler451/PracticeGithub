namespace Test.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int RoomId { get; set; } // Foreign Key
        public string UserId { get; set; } // Foreign Key
        public DateTime BookingDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Status { get; set; }

        // Navigation properties
        public Room Room { get; set; }
    }
}
