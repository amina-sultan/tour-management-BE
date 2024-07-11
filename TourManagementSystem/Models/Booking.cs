using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourManagementSystem.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }

        public string TotalCost { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; } = DateTime.Now.Date;

        [Required]
        [DataType(DataType.Date)]
        public DateTime TourDate { get; set; }
        public int UserId { get; set; }

        [Required]
        public string PaymentMethod { get; set; } = "Cash";

        [Required]
        public String? Status { get; set; }

        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
