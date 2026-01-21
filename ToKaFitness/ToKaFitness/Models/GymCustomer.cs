namespace ToKaFitness.Models
{
    public class GymCustomer
    {
        public int GymCustomerId { get; set; }
        public string UserID { get; set; } //Foriegn key for linking to a logged in user
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public ICollection<GymPass>? GymPass { get; set; }
    }
}
