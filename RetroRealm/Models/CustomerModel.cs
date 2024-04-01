using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace RetroRealm.Models
{
    public class CustomerModel
    {
        public int CustomerModelId { get; set; }
        [Required (ErrorMessage = "First name is required")]
        [StringLength (50, ErrorMessage = "First name must be less than 50 characters")]
        public string? Firstname { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name must be less than 50 characters")]
        public string? Lastname { get; set; }

        public string? Fullname => $"{Firstname} {Lastname}";

        [Required(ErrorMessage = "An Address is required")]
        [StringLength(50, ErrorMessage = "Address must be less than 50 characters")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City must be less than 50 characters")]
        public string? City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string? State { get; set; }

        [Required(ErrorMessage = "A postal code is required")]
        [StringLength(21, ErrorMessage = "Postal code must be less than 21 characters")]
        public string? PostalCode { get; set; }

        [EmailAddress(ErrorMessage = "Not a valid email address")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "Email must be less than 50 characters")]
        [Remote("CustomerEmailExists", "Validation", AdditionalFields = "CustomerModelId")]
        public string? Email { get; set; } = String.Empty;

        [Phone(ErrorMessage = "Not a valid phone number")]
        [RegularExpression("^\\(\\d{3}\\) \\d{3}-\\d{4}$", ErrorMessage = "Phone number must be in (999) 999-9999 format")]
        public string? Phone { get; set; }

        [ValidateNever]
        public CountryModel CountryModel { get; set; } = null!;

        [Required(ErrorMessage = "A country is required")]
        public int CountryModelId { get; set; }

        public ICollection<GameModel> GameModels { get; set; }

        public CustomerModel()
        {
            GameModels = new HashSet<GameModel>();
        }
    }
}
