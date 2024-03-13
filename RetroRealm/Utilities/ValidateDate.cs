using System.ComponentModel.DataAnnotations;

namespace RetroRealm.Utilities
{
    public class ValidateDate : ValidationAttribute
    {
        protected override ValidationResult IsValid
                         (object obj, ValidationContext validationContext)
        {
            DateTime date = Convert.ToDateTime(obj);
            return date <= DateTime.UtcNow && date >= DateTime.UtcNow.AddYears(-1)
                ? ValidationResult.Success
                : new ValidationResult("Invalid date range");
        }
    }
}
