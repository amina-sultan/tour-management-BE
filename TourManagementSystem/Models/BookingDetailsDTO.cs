namespace TourManagementSystem.Models
{
    public class BookingDetailsDTO
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime TourDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        public string DestinationName { get; set; }
        public object? Service { get; internal set; }
        public object? User { get; internal set; }
    }
}
