using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TourManagementSystem.Data;
using TourManagementSystem.Models;

namespace TourManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/User
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.ToListAsync();
        }


        [HttpGet("Admin")]
        [Authorize(Roles = "admin")]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = GetUserInfo();
            return Ok($"Hi {currentUser.FirstName} i am {currentUser.UserType}");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var User = await _context.Users!.FindAsync(id);

            if (User == null)
            {
                return NotFound("Person not found");
            }
            return Ok(User);
        }

        [ HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            _context.Users!.Add(user);
            await _context.SaveChangesAsync();

            return Ok(await GetUsers());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {

            if (_context.Users == null)
            {
                return NotFound();
            }
            var User = await _context.Users!.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            _context.Users.Remove(User);
            await _context.SaveChangesAsync();

            return Ok(User);
        }
        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private User GetUserInfo()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                int userId = 0;
                int.TryParse(userClaims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value, out userId);

                return new User
                {
                    email = userClaims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value,
                    Id = userId,
                    FirstName = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
                    UserType = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}
