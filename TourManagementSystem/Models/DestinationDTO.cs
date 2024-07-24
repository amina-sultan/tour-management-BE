using System.ComponentModel.DataAnnotations;

namespace TourManagementSystem.Models
{
    public class DestinationDTO
    {
        public int Id { get; set; }
        public string DestinationName { get; set; }
        public string Description { get; set; }
        public decimal HotelCosrPerDay { get; set; }
        public decimal BaseCost { get; set; }
        public string ImageUrl { get; set; }
    }
}
