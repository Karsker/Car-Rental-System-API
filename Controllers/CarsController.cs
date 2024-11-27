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
    public class CarsController : ControllerBase
    {
        private readonly ICarsService _carsService;

        public CarsController(ICarsService carsService)
        {
            _carsService = carsService;
        }

        [TransactionLog]
        [Authorize(Policy = "All")]
        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetAllCars()
        {
            var cars = await _carsService.GetAllCars();
            return Ok(cars);
        }

        [TransactionLog]
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

        [TransactionLog]
        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> AddCar(Car car)
        {
            if (car.Year < 1990 || car.Year > DateTime.Now.Year)
            {
                return BadRequest($"Car model should have released between 1990 and {DateTime.Now.Year}");
            }
            await _carsService.AddCar(car);
            return CreatedAtAction(nameof(GetCarById), new { id = car.Id }, car);
        }

        [TransactionLog]
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] CarDTO car)
        {
            // Check if car exists
            Car? carFromDb = await _carsService.GetCarById(id);
            if (carFromDb is null)
            {
                return NotFound("Car not found");
            }

            await _carsService.UpdateCar(id, car);
            return CreatedAtAction(nameof(GetCarById), new { id }, carFromDb);
            //return Ok();
        }

        [TransactionLog]
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            // Check if car exists
            Car? carFromDb = await _carsService.GetCarById(id);
            if (carFromDb is null)
            {
                return NotFound("Car not found");
            }

            await _carsService.DeleteCar(id);
            return NoContent();

        }
    }
}
