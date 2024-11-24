using CarRentalSystem.Models;
using Microsoft.Identity.Client;

namespace CarRentalSystem.Repositories
{
    public interface ICarsRepository
    {

        public Task<List<Car>> GetAll();

        public ValueTask<Car?> GetById(int id);
        public Task Add(Car car);

    }
}
