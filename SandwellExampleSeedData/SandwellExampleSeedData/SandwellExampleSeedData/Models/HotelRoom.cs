namespace SandwellExampleSeedData.Models
{
    public class HotelRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public double PricePerNight { get; set; }
        public int StarRating { get; set; }
        public double GuestRating { get; set; }
        public int NumberOfBedrooms { get; set; }
        public bool IsAvailable { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
