using CarRentalSystem.Models;
using CarRentalSystem.Repositories;

namespace CarRentalSystem.Services
{
    public class CarRentalService: ICarRentalService
    {
        private readonly ICarRentalRepository _carRentalRepo;

        public CarRentalService(ICarRentalRepository carRentalRepo)
        {
            _carRentalRepo = carRentalRepo;
        }
        public Task<List<CarRental>> GetAllCarRentals()
        {
            return _carRentalRepo.GetAll();
        }

        public Task AddCarRental(CarRental carRental)
        {
            return _carRentalRepo.Add(carRental);
        }

    }
}
