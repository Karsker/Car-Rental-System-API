using CarRentalSystem.Data;
using CarRentalSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystem.Repositories
{
    public class CarsRepository: ICarsRepository
    {
        private readonly AppDbContext _dbContext;

        public CarsRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<List<Car>> GetAll()
        {
            return _dbContext.Cars.ToListAsync();
        }

        public ValueTask<Car?> GetById(int id)
        {
            return _dbContext.Cars.FindAsync(id);
        }
        public Task Add(Car car)
        {
            _dbContext.Cars.Add(car);
            return Task.FromResult(_dbContext.SaveChangesAsync());
        }
    }
}
