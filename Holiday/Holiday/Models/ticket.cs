namespace Holiday.Models
{
    public class ticket
    {
        public int ticketId {  get; set; }
        public string ticketName { get; set; }
        public string ticketType { get; set; }
        public DateOnly ticketDate { get; set; }
        public TimeOnly ticketTime { get; set; }
        public int FlightId { get; set; }
        public Flight Flight { get; set; }



    }
}
