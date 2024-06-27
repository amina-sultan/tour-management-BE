using Microsoft.AspNetCore.Http;
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

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            if (_context.Reviews == null)
            {
                return NotFound();
            }
            return await _context.Reviews.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            if (_context.Reviews == null)
            {
                return NotFound();
            }
            var Review = await _context.Reviews!.FindAsync(id);

            if (Review == null)
            {
                return NotFound("Person not found");
            }
            return Ok(Review);
        }

        [HttpPost]
        public async Task<ActionResult<Review>> AddReview(Review review)
        {
            _context.Reviews!.Add(review);
            await _context.SaveChangesAsync();

            return Ok(await GetReviews());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(review);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {

            if (_context.Reviews == null)
            {
                return NotFound();
            }
            var Review = await _context.Reviews!.FindAsync(id);
            if (Review == null)
            {
                return NotFound();
            }
            _context.Reviews.Remove(Review);
            await _context.SaveChangesAsync();

            return Ok(Review);
        }

        private bool ReviewExists(int id)
        {
            return (_context.Reviews?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
