
using System.ComponentModel.DataAnnotations;
namespace Delivery.Shared
{
    public class Address
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "Zip Code is required")]
        public string ZipCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }

        public User? User { get; set; }
    }

}


