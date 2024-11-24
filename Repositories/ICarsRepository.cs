using CarRentalSystem.Models;

namespace CarRentalSystem.Repositories
{
    public interface ICarsRepository
    {

        public Task<List<Car>> GetAll();
        public Task Add(Car car);
    }
}
