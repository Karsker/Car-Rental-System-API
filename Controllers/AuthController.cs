using CarRentalSystem.Data;
using CarRentalSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    public class LoginCredential
    {
        public required string Email { get; set; }

        public required string Password { get; set; }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JWTService _jwtService;
        private readonly IUserService _userService;

        public AuthController(JWTService jwtService, IUserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> ValidateCredentials(LoginCredential cred)
        {
            // Check if a user with the email exists
            var user = await _userService.GetUserByEmail(cred.Email);
            if (user == null)
            {
                return BadRequest("No user exists with that email address");
            }

            // Compare password hashes
            var passwordHash = UserService.ComputeHash(cred.Password);

            if (!(passwordHash == user.Password))
            {
                return Unauthorized("Invalid password");
            }

            // Generate token
            var token = _jwtService.GenerateToken(user.Name, user.Role);
            return Ok(token);
        }
    }
}
