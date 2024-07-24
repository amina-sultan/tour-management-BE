namespace TourManagementSystem.Models
{
    public class ServiceDTO
    {
        public int Id { get; set; }
        public string NumberOfPeople { get; set; }
        public string NumberOfDays { get; set; }
        public bool IsRequiredPersonalGuide { get; set; }
        public string NoOfRoom { get; set; }
        public string TourType { get; set; }
        public string Description { get; set; }
        public int DestinationId { get; set; }
        public DestinationDTO Destination { get; set; }
    }
}
