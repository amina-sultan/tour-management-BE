using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TourManagementSystem.Models;
using TourManagementSystem.Repositories;
using TourManagementSystem.Services;

namespace TourManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public AuthController(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(loginDto.Email, loginDto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = await _authService.GenerateJwtToken(user);

            return Ok(new { Token = token });
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)

        {
            // Check if the email is already registered
            var existingUser = await _userRepository.GetUserByEmailAsync(signUpDto.email);
            if (existingUser != null)
            {
                return Conflict("Email already exists");
            }

            // Create a new user entity
            var newUser = new User
            {
                FirstName = signUpDto.FirstName,
                LastName = signUpDto.LastName,
                email = signUpDto.email,
                Password = signUpDto.Password  // Note: Password hashing should be implemented
                                                // Assign other properties as needed
            };

            // Add the user to the database
            var createdUser = await _userRepository.AddAsync(newUser);

            // Generate JWT token for the new user (if needed)
            var token = await _authService.GenerateJwtToken(createdUser);

            return Ok(new { Token = token });
        }
    }
}
