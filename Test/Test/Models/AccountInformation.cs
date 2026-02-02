namespace Test.Models
{
    public class AccountInformation
    {
        public int AccountInformationId { get; set; }

        public string UserId { get; set; } // Foreign Key

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
