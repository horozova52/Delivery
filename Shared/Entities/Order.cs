using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Delivery.Shared
{
    public class Order
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Order date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public DateTime OrderDate { get; set; }
        [Required(ErrorMessage = "Status ID is required")]
        public int StatusId { get; set; }
        public Status? Status { get; set; }
        [Required(ErrorMessage = "User ID is required")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required(ErrorMessage = "Address ID is required")]
        public int AddressId { get; set; }
        public Address? Address { get; set; }
    }
}
