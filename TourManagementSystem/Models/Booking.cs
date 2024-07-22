using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

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

        [Required]
        public string PaymentMethod { get; set; } = "Cash";

        [Required]
        public String? Status { get; set; }

        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }

        [Required]
        public string? userName { get; set; }
        [Required]
        public string? contact { get; set; }

        [Required]
        public string? CNIC { get; set; }
    }
}
