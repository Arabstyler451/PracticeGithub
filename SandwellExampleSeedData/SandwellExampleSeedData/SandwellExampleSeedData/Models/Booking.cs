namespace SandwellExampleSeedData.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int HotelRoomId { get; set; }
        public string CustomerName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation property
        public HotelRoom HotelRoom { get; set; }

    }
}
