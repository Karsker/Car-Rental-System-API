using System.Security.Claims;
using System.Text.RegularExpressions;
using CarRentalSystem.Filters;
using CarRentalSystem.Models;
using CarRentalSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CarRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        // Function to check password strength
        bool PasswordIsStrong(string passwd)
        {
            string regexPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$#!%*?&])[A-Za-z\d@$#!%*?&]{6,}$";

            if (Regex.IsMatch(passwd, regexPattern))
            {
                return true;
            }

            return false;

        }

        [TransactionLog]
        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [TransactionLog]
        [HttpPost("register")]
        public async Task<ActionResult<User>> AddUser(User user)
        {

            // Check if password is strong
            if (!PasswordIsStrong(user.Password))
            {
                return BadRequest($"Password does not meet requirements");
            }
            await _userService.AddUser(user);
            return CreatedAtAction(nameof(GetAllUsers), user);

        }


    }
}
