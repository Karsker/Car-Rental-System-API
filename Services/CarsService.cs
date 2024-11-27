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

        public async Task UpdateCar(int id, CarDTO car)
        {
            var carFromDb = await _carsRepo.GetById(id);

            // Iterate through the properties and update
            foreach (var prop in carFromDb.GetType().GetProperties())
            {
                var updatedValue = typeof(CarDTO).GetProperty(prop.Name)?.GetValue(car);
                if (updatedValue is not null)
                {
                    prop.SetValue(carFromDb, updatedValue);
                }
            }
            await _carsRepo.Update(carFromDb);
        }

        public Task DeleteCar(int id)
        {
            return _carsRepo.Delete(id);
        }

    }
}
