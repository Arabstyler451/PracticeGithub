namespace Holiday.Models
{
    public class Travel
    {
        public int TravelId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DestinationID { get; set; }
        public Destination Destination { get; set; }
        public ICollection<Flight>? Flights { get; set; }

    }
}
