using System.ComponentModel.DataAnnotations;

namespace TourManagementSystem.Models
{
    public class Destination
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        public string DestinationName { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal? HotelCosrPerDay { get; set; }
        [Required]
        public decimal BaseCost { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

    }
}
