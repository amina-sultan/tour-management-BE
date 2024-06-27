using System.ComponentModel.DataAnnotations;

namespace TourManagementSystem.Models
{
    public class User
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string? email { get; set; }

        public string? gender { get; set; }

        public string? Address { get; set; }

        public string? PhonenUmber { get; set; } = string.Empty;

        public string? Password { get; set; }

        public string? UserType { get; set; } = string.Empty;

        public int DestinationId { get; set; }
        public Destination? Destination { get; set; }

    }
}
