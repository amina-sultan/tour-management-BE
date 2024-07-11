using System.ComponentModel.DataAnnotations;

namespace TourManagementSystem.Models
{
    public class BookingCalculationDto
    {
        [Required]
        public int ServiceId { get; set; }
    }
}
