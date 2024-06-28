using System.ComponentModel.DataAnnotations;

namespace TourManagementSystem.Models
{
    public class SignUpDto
    {
        [Required]
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? email { get; set; }

        [Required]
        [MinLength(6)]
        public string? Password { get; set; }

        public string? UserType { get; set; }

    }
}
