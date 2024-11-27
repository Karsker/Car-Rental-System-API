using CarRentalSystem.Models;
using CarRentalSystem.Repositories;

namespace CarRentalSystem.Services
{
    public interface ICarsService
    {
        public Task<List<Car>> GetAllCars();

        public ValueTask<Car?> GetCarById(int id);
        public Task AddCar(Car car);

        public Task UpdateCar(int id, CarDTO car);

        public Task DeleteCar(int id);
    }
}
