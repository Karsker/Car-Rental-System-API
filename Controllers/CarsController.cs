using CarRentalSystem.Models;
using CarRentalSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarsService _carsService;

        public CarsController(ICarsService carsService)
        {
            _carsService = carsService;
        }

        [Authorize(Policy = "All")]
        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetAllCars()
        {
            return Ok(await _carsService.GetAllCars());
        }

        [Authorize(Policy = "All")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCarById(int id)
        {
            Car? car = await _carsService.GetCarById(id);
            if (car == null) { 
                return NotFound("Car not found");
            }

            return Ok(car);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> AddCar(Car car)
        {
            if (car.Year < 1990 || car.Year > DateTime.Now.Year)
            {
                return BadRequest($"Car model should have released between 1990 and {DateTime.Now.Year}");
            }
            await _carsService.AddCar(car);
            return CreatedAtAction(nameof(GetCarById),new { id = car.Id }, car);
        }
    }
}
