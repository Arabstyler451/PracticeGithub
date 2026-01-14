namespace Holiday.Models
{
    public class Destination
    {
        public int DestinationId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public ICollection<Travel>? Travels { get; set; }
    }
}
