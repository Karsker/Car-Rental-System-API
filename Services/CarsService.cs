using CarRentalSystem.Models;
using CarRentalSystem.Repositories;

namespace CarRentalSystem.Services
{
    public class CarsService : ICarsService
    {
        private readonly ICarsRepository _carsRepo;

        public CarsService(ICarsRepository carsRepo)
        {
            _carsRepo = carsRepo;
        }

        public Task<List<Car>> GetAllCars()
        {
            return _carsRepo.GetAll();
        }

        public ValueTask<Car?> GetCarById(int id)
        {
            return _carsRepo.GetById(id);
        }

        public Task AddCar(Car car)
        {
            return _carsRepo.Add(car);
        }

    }
}
