using System.ComponentModel.DataAnnotations;

namespace RetroRealm.Models
{
    public class TechnicianModel
    {
        public int TechnicianModelId { get; set; }
        [Required (ErrorMessage = "Name is required!")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "An email is required!")]
        [EmailAddress (ErrorMessage = "Not a valid email address")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "A phone number is required!")]
        [Phone (ErrorMessage = "Not a valid phone number")]
        public string? Phone { get; set; }
    }
}
