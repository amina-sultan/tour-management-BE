using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace TourManagementSystem.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string Content { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

    }
}
