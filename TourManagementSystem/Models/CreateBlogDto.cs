using System.ComponentModel.DataAnnotations;

namespace TourManagementSystem.Models
{
    public class CreateBlogDto
    {
        public string Content { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
