using System.ComponentModel.DataAnnotations;

namespace TourManagementSystem.Models
{
    public class UpdateBlogDto
    {
        public string Content { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
