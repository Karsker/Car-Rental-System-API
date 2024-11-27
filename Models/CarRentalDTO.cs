using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class CarRentalDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        [DateValidate(CheckFuture = false)]
        public DateOnly RentedOn { get; set; }

        [Required]
        [Range(1, 14)]
        public int Days { get; set; }

    }
}
