using System.ComponentModel.DataAnnotations;

namespace RetroRealm.Models
{
    public class CustomerModel
    {
        public int CustomerModelId { get; set; }
        [Required (ErrorMessage = "First name is required")]
        public string? Firstname { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string? Lastname { get; set; }

        [Required(ErrorMessage = "An Address is required")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string? City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string? State { get; set; }

        [Required(ErrorMessage = "A postal code is required")]
        public string? PostalCode { get; set; }

        [Required(ErrorMessage = "An email is required!")]
        [EmailAddress(ErrorMessage = "Not a valid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A phone number is required!")]
        [Phone(ErrorMessage = "Not a valid phone number")]
        public string? Phone { get; set; }
    }
}
