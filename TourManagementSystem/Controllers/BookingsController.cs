using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourManagementSystem.Data;
using TourManagementSystem.Models;
using TourManagementSystem.Services;

namespace TourManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly BookingService _bookingService;

        public BookingsController(DataContext context, BookingService bookingService)
        {
            _context = context;
            _bookingService = bookingService;
        }

        [HttpPost("calculatecost")]
        public async Task<ActionResult<decimal>> CalculateBookingCost([FromQuery] BookingCalculationDto dto)
        {
            try
            {
                var service = await _context.Services
                    .Include(s => s.Destination)
                    .FirstOrDefaultAsync(s => s.Id == dto.ServiceId);

                if (service == null)
                {
                    return NotFound("Service not found");
                }

                var destination = service.Destination;

                decimal totalCost = _bookingService.CalculateTotalCost(service, destination);

                return Ok(totalCost);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] BookingDTO bookingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = await _context.Services.FindAsync(bookingDTO.ServiceId);
            if (service == null)
            {
                return NotFound("Service not found.");
            }

            var booking = new Booking
            {
                ServiceId = bookingDTO.ServiceId,
                TotalCost = bookingDTO.TotalCost,
                BookingDate = DateTime.Now.Date,
                TourDate = bookingDTO.TourDate,
                PaymentMethod = bookingDTO.PaymentMethod,
                Status = bookingDTO.Status,
                userName = bookingDTO.userName,
                contact = bookingDTO.contact,
                CNIC = bookingDTO.CNIC
            };

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return Ok(booking);
        }

        [HttpGet("mybookings")]
        public async Task<IActionResult> GetUserBookings()
        {
            var query = @"
        SELECT 
            b.Id, 
            b.ServiceId, 
            b.TotalCost, 
            b.BookingDate, 
            b.TourDate, 
            b.PaymentMethod, 
            b.Status, 
            b.UserName,
            b.Contact,
            b.CNIC,
            d.DestinationName AS DestinationName
        FROM 
            Bookings b
        JOIN 
            Services s ON b.ServiceId = s.Id
        JOIN 
            Destinations d ON s.DestinationId = d.Id";

            var bookingDetails = await _context.Set<BookingDetailsDTO>()
                .FromSqlRaw(query)
                .ToListAsync();

            if (!bookingDetails.Any())
            {
                return NotFound();
            }

            return Ok(bookingDetails);
        }
    }
}
