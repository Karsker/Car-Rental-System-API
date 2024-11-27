using System.Security.Claims;
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
    public class RentalsController : ControllerBase
    {
        private readonly ICarRentalService _carRentalService;
        private readonly ICarsService _carsService;
        private readonly IUserService _userService;
        public RentalsController(ICarRentalService carRentalService, ICarsService carsService, IUserService userService)
        {
            _carRentalService = carRentalService;
            _carsService = carsService;
            _userService = userService;
        }

        [TransactionLog]
        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarRental>>> GetAllCarRentals()
        {
            var cars = await _carRentalService.GetAllCarRentals();
            return Ok(cars);
        }

        [TransactionLog]
        [Authorize(Policy = "All")]
        [HttpPost]
        public async Task<IActionResult> AddCarRental(CarRentalDTO carRental)
        {
            // Check if userId in request is same as current user Id
            var userIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var userIdFromClaim = userIdentity?.FindFirst("userId")?.Value;
            
            if (userIdFromClaim is not null && int.Parse(userIdFromClaim) != carRental.UserId)
            {
                return Unauthorized("UserId must be same as current user");
            }

            // Check if car exists in the database
            var carFromDb = await _carsService.GetCarById(carRental.CarId);

            if (carFromDb is null)
            {
                return NotFound("Car not found");
            }

            // Check if the user exists in the database
            var userFromDb = await _userService.GetUserById(carRental.UserId);

            if (userFromDb is null)
            {
                return NotFound("User not found");
            }

            // Check if the car is available to rent
            if (!carFromDb.IsAvailable)
            {
                return BadRequest("Car is not available to rent");
            }

            var newRental = await _carRentalService.AddCarRental(carRental);

            return CreatedAtAction(nameof(GetAllCarRentals), newRental);
        }
    }
}
