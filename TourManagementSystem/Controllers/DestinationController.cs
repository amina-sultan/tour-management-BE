using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourManagementSystem.Data;
using TourManagementSystem.Models;

namespace TourManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationController : ControllerBase
    {
        private readonly DataContext _context;

        public DestinationController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Destination
        [HttpGet]   
        public async Task<ActionResult<IEnumerable<Destination>>> GetDestinations()
        {
            if (_context.Destinations == null)
            {
                return NotFound();
            }
            return await _context.Destinations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Destination>> GetDestination(int id)
        {
            if (_context.Destinations == null)
            {
                return NotFound();
            }
            var Destination = await _context.Destinations!.FindAsync(id);

            if (Destination == null)
            {
                return NotFound("Person not found");
            }
            return Ok(Destination);
        }

        [HttpPost]
        public async Task<ActionResult<Destination>> AddDestination(Destination destination)
        {
            _context.Destinations!.Add(destination);
            await _context.SaveChangesAsync();

            return Ok(await GetDestinations());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDestination(int id, Destination destination)
        {
            if (id != destination.Id)
            {
                return BadRequest();
            }

            _context.Entry(destination).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestinationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(destination);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestination(int id)
        {

            if (_context.Destinations == null)
            {
                return NotFound();
            }
            var destination = await _context.Destinations!.FindAsync(id);
            if (destination == null)
            {
                return NotFound();
            }
            _context.Destinations.Remove(destination);
            await _context.SaveChangesAsync();

            return new JsonResult("Deleted Successfully");
        }

        private bool DestinationExists(int id)
        {
            return (_context.Destinations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
