using System.ComponentModel.DataAnnotations;

namespace TourManagementSystem.Models
{
    public class BookingDTO
    {
        public int ServiceId { get; set; }
        public string TotalCost { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime TourDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public string? userName { get; set; }
        public string? contact { get; set; }
        public string? CNIC { get; set; }
    }
}
