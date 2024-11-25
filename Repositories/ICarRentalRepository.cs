using CarRentalSystem.Models;

namespace CarRentalSystem.Repositories
{
    public interface ICarRentalRepository
    {
        public Task<List<CarRental>> GetAll();
        public Task Add(CarRental carRental);
    }
}
