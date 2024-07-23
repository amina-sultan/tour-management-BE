using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourManagementSystem.Data;
using TourManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace TourManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly DataContext _context;

        public ServicesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            if (_context.Services == null)
            {
                return NotFound();
            }

            var services = await _context.Services
                                          .Include(s => s.Destination)
                                          .ToListAsync();

            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(int id)
        {
            if (_context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Destination)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (service == null)
            {
                return NotFound("Service not found");
            }

            return Ok(service);
        }

        [HttpPost]
        public async Task<ActionResult<Service>> AddService(Service service)
        {
            _context.Services!.Add(service);
            await _context.SaveChangesAsync();

            return Ok(await GetServices());
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(int id, [FromBody] ServiceDTO serviceDto)
        {
            if (id != serviceDto.Id)
            {
                return BadRequest();
            }

            var currentService = await _context.Services.Include(s => s.Destination).FirstOrDefaultAsync(s => s.Id == id);
            if (currentService == null)
            {
                return NotFound();
            }

            currentService.NumberOfPeople = serviceDto.NumberOfPeople;
            currentService.NumberOfDays = serviceDto.NumberOfDays;
            currentService.IsRequiredPersonalGuide = serviceDto.IsRequiredPersonalGuide;
            currentService.NoOfRoom = serviceDto.NoOfRoom;
            currentService.TourType = serviceDto.TourType;
            currentService.Description = serviceDto.Description;
            currentService.DestinationId = serviceDto.DestinationId;

            if (serviceDto.Destination != null)
            {
                currentService.Destination.DestinationName = serviceDto.Destination.DestinationName;
                currentService.Destination.Description = serviceDto.Destination.Description;
                currentService.Destination.HotelCosrPerDay = serviceDto.Destination.HotelCosrPerDay;
                currentService.Destination.BaseCost = serviceDto.Destination.BaseCost;
                currentService.Destination.ImageUrl = serviceDto.Destination.ImageUrl;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(serviceDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {

            if (_context.Services == null)
            {
                return NotFound();
            }
            var Service = await _context.Services!.FindAsync(id);
            if (Service == null)
            {
                return NotFound();
            }
            _context.Services.Remove(Service);
            await _context.SaveChangesAsync();

            return Ok(Service);
        }

        private bool ServiceExists(int id)
        {
            return (_context.Services?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpGet("unauthorized")]
        public IActionResult UnauthorizedAction()
        {
            return StatusCode(403, new { message = "You are not authorized for this action." });
        }
    }
}
