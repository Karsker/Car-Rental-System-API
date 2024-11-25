using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Models
{
    public class CarRental
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public required User User { get; set; }

        [Required]
        public required Car Car { get; set; }

        [Required]
        // Car must have been rented on or before the current date
        [DateValidate(CheckFuture = false)]
        public required DateOnly RentedOn { get; set; }

        [Required]
        // Car must be rented till a future date (not including curren date)
        [DateValidate(CheckFuture = true)]
        public required DateOnly RentedTill { get; set; }

        [Required]
        [Range(1, 14)]
        public required int Days { get; set; }

        [Required]
        [Range(500.00, 70000.00)]
        [Precision(7, 2)]
        public required decimal Amount { get; set; }

    }
}
