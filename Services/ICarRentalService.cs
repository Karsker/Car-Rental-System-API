using CarRentalSystem.Models;

namespace CarRentalSystem.Services
{
    public interface ICarRentalService
    {

        public Task<List<CarRental>> GetAllCarRentals();

        public Task AddCarRental(CarRental carRental);
    }
}
