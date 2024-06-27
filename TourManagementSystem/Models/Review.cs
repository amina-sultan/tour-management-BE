using System.ComponentModel.DataAnnotations;

namespace TourManagementSystem.Models
{
    public class Review
    {


        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        public string Feedback { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
