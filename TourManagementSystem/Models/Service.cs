using System.ComponentModel.DataAnnotations;

namespace TourManagementSystem.Models
{
    public class Service
    {
        public int Id { get; set; }

        public string NumberOfPeople { get; set; } = string.Empty;
        public string NumberOfDays { get; set; } = string.Empty;
        public bool IsRequiredPersonalGuide { get; set; }

        public string? NoOfRoom { get; set; }

        public string TourType { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        public int DestinationId { get; set; }
        public Destination? Destination { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
