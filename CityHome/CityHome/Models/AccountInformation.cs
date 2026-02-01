using System.ComponentModel.DataAnnotations.Schema;

namespace CityHome.Models
{
    public class AccountInformation
    {
        public int AccountInformationId { get; set; }

        public string UserId { get; set; } // Foreign key to ApplicationUser

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }
    }

}
