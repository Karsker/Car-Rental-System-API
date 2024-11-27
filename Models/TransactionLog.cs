using System.ComponentModel.DataAnnotations;

namespace CarRentalSystem.Models
{
    public class TransactionLog
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public required int UserId{ get; set; }

        [Required]
        public required string UserName { get; set; }

        [Required]
        public required string Endpoint { get; set; }

        [Required]
        [AllowedValues("GET", "POST",  "PUT", "DELETE")]
        public required string Method { get; set; }

        [Required]
        [DateValidate(CheckFuture =false)]
        public required DateTime Time { get; set; }

    }
}
