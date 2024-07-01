using Microsoft.AspNetCore.Mvc;
using TourManagementSystem.Models;
using TourManagementSystem.Repositories;


namespace TourManagementSystem.Controllers { 
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogRepository _blogRepository;

        public BlogController(BlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] CreateBlogDto createBlogDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blog = new Blog
            {
                Content = createBlogDto.Content,
                ImageUrl = createBlogDto.ImageUrl
            };

            await _blogRepository.AddAsync(blog);

            return CreatedAtAction(nameof(GetBlogById), new { id = blog.Id }, blog);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            var blogs = await _blogRepository.GetAllAsync();
            return Ok(blogs);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(int id, [FromBody] UpdateBlogDto updateBlogDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            blog.Content = updateBlogDto.Content;
            blog.ImageUrl = updateBlogDto.ImageUrl;

            await _blogRepository.UpdateAsync(blog);

            return NoContent();
        }

        // DELETE api/blog/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            await _blogRepository.DeleteAsync(blog);

            return NoContent();
        }
    }
}
