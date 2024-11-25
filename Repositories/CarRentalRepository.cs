using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Repositories
{
    public class CarRentalRepository: ICarRentalRepository
    {
        private readonly AppDbContext _dbContext;

        public CarRentalRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<CarRental>> GetAll()
        {
            return _dbContext.CarRentals.ToListAsync();
        }

        public Task Add(CarRental carRental)
        {
            _dbContext.CarRentals.Add(carRental);
            return _dbContext.SaveChangesAsync();

        }

    }
}
