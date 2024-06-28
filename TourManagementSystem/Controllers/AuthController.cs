using Microsoft.AspNetCore.Mvc;
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
            var existingUser = await _userRepository.GetUserByEmailAsync(signUpDto.email);
            if (existingUser != null)
            {
                return Conflict("Email already exists");
            }

            var newUser = new User
            {
                FirstName = signUpDto.FirstName,
                LastName = signUpDto.LastName,
                email = signUpDto.email,
                Password = signUpDto.Password,
                gender = signUpDto.gender,
                Address = signUpDto.Address,
                PhonenUmber = signUpDto.PhonenUmber,
                UserType = signUpDto.UserType
            };

            var createdUser = await _userRepository.AddAsync(newUser);

            var token = await _authService.GenerateJwtToken(createdUser);

            return Ok(new { Token = token });
        }
    }
}
