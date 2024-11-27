using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class CarDTO
    {
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }

        [Precision(6, 2)]
        [Range(500.00, 5000.00)]
        public decimal? PricePerDay { get; set; }
        public bool? IsAvailable { get; set; }
    }
}
