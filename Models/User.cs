using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]  
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        [AllowedValues("User", "Admin")]
        public required string Role { get; set; }
    }
}
