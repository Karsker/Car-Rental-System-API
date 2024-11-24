using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Models
{
    public class Car
    {
        [Required]
        public int Id {  get; set; }

        [Required]
        public required string Manufacturer { get; set; }

        [Required]
        public required string Model { get; set; }

        [Required]
        public required DateTime YearOfRTM { get; set; }

        [Required]
        [Precision(6, 2)]
        [Range(500.00, 5000.00)]
        public required decimal PricePerDay { get; set; }

        [Required]
        public required bool IsAvailable { get; set; }
    }
}
