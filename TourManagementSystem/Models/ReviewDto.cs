namespace TourManagementSystem.Models
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Feedback { get; set; }
        public int UserId { get; set; }
        public string ImageUrl { get; set; }
        public UserDto User { get; set; }
    }

}
