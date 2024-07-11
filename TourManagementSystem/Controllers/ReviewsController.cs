using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourManagementSystem.Data;
using TourManagementSystem.Models;


namespace TourManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly DataContext _context;

        public ReviewsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetReviews()
        {
            var reviews = await _context.Reviews
                .Include(r => r.User)
                .Select(r => new ReviewDto
                {
                    Id = r.Id,
                    Feedback = r.Feedback,
                    UserId = r.UserId,
                    ImageUrl = r.ImageUrl,
                    User = new UserDto
                    {
                        Id = r.User.Id,
                        FirstName = r.User.FirstName,
                        LastName = r.User.LastName,
                        email = r.User.email,
                        gender = r.User.gender,
                        Address = r.User.Address,
                        PhonenUmber = r.User.PhonenUmber,
                        UserType = r.User.UserType
                    }
                })
                .ToListAsync();

            return Ok(reviews);
        }

        [HttpPost]
        public async Task<ActionResult<Review>> PostReview([FromBody] CreateReviewDto createReviewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FindAsync(createReviewDto.UserId);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var review = new Review
            {
                Feedback = createReviewDto.Feedback,
                UserId = createReviewDto.UserId,
                ImageUrl = createReviewDto.ImageUrl
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReviews), new { id = review.Id }, review);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound("Review not found");
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> GetReview(int id)
        {
            var review = await _context.Reviews
                .Include(r => r.User)
                .Select(r => new ReviewDto
                {
                    Id = r.Id,
                    Feedback = r.Feedback,
                    UserId = r.UserId,
                    ImageUrl = r.ImageUrl,
                    User = new UserDto
                    {
                        Id = r.User.Id,
                        FirstName = r.User.FirstName,
                        LastName = r.User.LastName,
                        email = r.User.email,
                        gender = r.User.gender,
                        Address = r.User.Address,
                        PhonenUmber = r.User.PhonenUmber,
                        UserType = r.User.UserType
                    }
                })
                .FirstOrDefaultAsync(r => r.Id == id); // Match by Review Id

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, ReviewDto reviewDto)
        {
            if (id != reviewDto.Id) // Ensure the ID matches the Review ID
            {
                return BadRequest();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            review.Feedback = reviewDto.Feedback;
            review.ImageUrl = reviewDto.ImageUrl;

            var user = await _context.Users.FindAsync(reviewDto.UserId);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            review.User = user;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Reviews.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}
