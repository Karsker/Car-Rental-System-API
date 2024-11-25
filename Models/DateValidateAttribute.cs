using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class DateValidateAttribute: ValidationAttribute
    {
        public bool CheckFuture { get; set; } = false;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);
            if (value is DateOnly dt)
            {
                // Check if the provided date is after the current date (should be before)
                if (!CheckFuture && dt >= currentDate) {
                    return new ValidationResult(ErrorMessage = $"Date must be before {currentDate}");
                }

                // Check if the provided date is before the current date (should be after
                if (CheckFuture && dt < currentDate)
                {
                    return new ValidationResult(ErrorMessage = $"Date must be after {DateOnly.FromDateTime(DateTime.Now)}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
