namespace Holiday.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public string Airline { get; set; }
        public string FlightNumber { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public int TravelId { get; set; }
        public Travel Travel { get; set; }
        public ICollection<ticket>? Tickets { get; set; }

    }
}
