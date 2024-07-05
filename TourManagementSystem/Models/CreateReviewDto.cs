namespace TourManagementSystem.Models
{
    public class CreateReviewDto
    {
        public string Feedback { get; set; }
        public int UserId { get; set; }
        public string ImageUrl { get; set; }
    }
}
